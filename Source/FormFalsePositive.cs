using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using woanware;
using System;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormFalsePositive : Form
    {
        #region Member Variables
        private Event _event;
        private FalsePositives _falsePositives;
        private FalsePositive _falsePositive;
        private List<FilterDefinition> _filterDefinitions;
        private List<NameValue> _protocols;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="falsePositives"></param>
        public FormFalsePositive(FalsePositives falsePositives)
        {
            InitializeComponent();

            _falsePositives = falsePositives;

            GenerateFilterDefinitions();

            cboField.Items.Clear();
            cboField.DisplayMember = "Field";
            cboField.ValueMember = "FieldName";
            cboField.Items.AddRange(_filterDefinitions.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="falsePositives"></param>
        /// <param name="?"></param>
        public FormFalsePositive(FalsePositives falsePositives, 
                                 Event data) : this(falsePositives)
        {
            _event = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="falsePositives"></param>
        /// <param name="falsePositive"></param>
        public FormFalsePositive(FalsePositives falsePositives,
                                 FalsePositive falsePositive): this(falsePositives)
        {
            _falsePositive = falsePositive;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void LoadProtocols()
        {
            using (new HourGlass(this))
            {
                _protocols = new List<NameValue>();
                
                NameValue nameValue = new NameValue();
                nameValue.Name = Global.Protocols.Tcp.GetEnumDescription();
                nameValue.Value = ((int)Global.Protocols.Tcp).ToString();
                _protocols.Add(nameValue);

                nameValue = new NameValue();
                nameValue.Name = Global.Protocols.Udp.GetEnumDescription();
                nameValue.Value = ((int)Global.Protocols.Udp).ToString();
                _protocols.Add(nameValue);

                nameValue = new NameValue();
                nameValue.Name = Global.Protocols.Icmp.GetEnumDescription();
                nameValue.Value = ((int)Global.Protocols.Icmp).ToString();
                _protocols.Add(nameValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void GenerateFilterDefinitions()
        {
            _filterDefinitions = new List<FilterDefinition>();
            AddFilterDefinition("Source IP", "iphdr.ip_src", Global.FilterType.Ip);
            AddFilterDefinition("TCP Source Port", "tcphdr.tcp_sport", Global.FilterType.Numeric);
            AddFilterDefinition("UDP Source Port", "udphdr.udp_sport", Global.FilterType.Numeric);
            AddFilterDefinition("Destination IP", "iphdr.ip_dst", Global.FilterType.Ip);
            AddFilterDefinition("TCP Destination Port", "tcphdr.tcp_dport", Global.FilterType.Numeric);
            AddFilterDefinition("UDP Destination Port", "udphdr.udp_dport", Global.FilterType.Numeric);
            AddFilterDefinition("Protocol", "iphdr.ip_proto", Global.FilterType.Protocol);
            AddFilterDefinition("Payload (ASCII)", "data.data_payload", Global.FilterType.PayloadAscii);
            AddFilterDefinition("Payload (HEX)", "data.data_payload", Global.FilterType.PayloadHex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="columnName"></param>
        /// <param name="type"></param>
        private void AddFilterDefinition(string field, 
                                         string columnName, 
                                         snorbert.Global.FilterType type)
        {
            FilterDefinition fd = new FilterDefinition();
            fd.Field = field;
            fd.ColumnName = columnName;
            fd.Type = type;
            _filterDefinitions.Add(fd);
        }

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            if (cboField.SelectedIndex == -1)
            {
                UserInterface.DisplayMessageBox(this, "The field must be selected", MessageBoxIcon.Exclamation);
                cboField.Select();
                return;
            }

            if (cboCondition.SelectedIndex == -1)
            {
                UserInterface.DisplayMessageBox(this, "The condition must be selected", MessageBoxIcon.Exclamation);
                cboCondition.Select();
                return;
            }

            FilterDefinition fd = (FilterDefinition)cboField.Items[cboField.SelectedIndex];
            if (fd == null)
            {
                return;
            }

            FalsePositive tempFp = new FalsePositive();
            tempFp.Definition = fd;
            tempFp.Condition = cboCondition.Items[cboCondition.SelectedIndex].ToString();
            tempFp.Value = GetFilterValue(fd);
            tempFp.Display = GetFilterDisplayValue(fd);

            if (_event == null)
            {
                tempFp.Sid = _falsePositive.Sid;
            }
            else
            {
                tempFp.Sid = _event.Sid.ToString();
            }

            // Make sure the FP does not already exist
            var temp = from f in _falsePositives.Data
                       where f.Condition == tempFp.Condition &
                             f.Definition.ColumnName == tempFp.Definition.ColumnName &
                             f.Value == tempFp.Value &
                             f.Sid == tempFp.Sid
                       select f;

            if (temp.Any() == true)
            {
                UserInterface.DisplayMessageBox(this, "The false positive already exists", MessageBoxIcon.Exclamation);
                return;
            }

            // New false positive
            if (_falsePositive == null)
            {
                _falsePositive = new FalsePositive();
            }

            _falsePositive.Sid = tempFp.Sid;
            _falsePositive.Condition = tempFp.Condition;
            _falsePositive.Definition = tempFp.Definition;
            _falsePositive.Value = tempFp.Value;
            _falsePositive.Display = tempFp.Display;

            _falsePositives.Data.Add(_falsePositive);

            string ret = _falsePositives.Save();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox("An error occurred whilst saving the false positives: " + ret);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboField_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cboField.SelectedIndex == -1)
            {
                return;
            }

            LoadConditions();
            LoadDefaultValue();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadDefaultValue()
        {
            if (_event == null)
            {
                return;
            }

            FilterDefinition fd = (FilterDefinition)cboField.Items[cboField.SelectedIndex];
            if (fd == null)
            {
                return;
            }

            switch (fd.Field.ToLower())
            {
                case "source ip":
                    ipValue.Text = _event.IpSrc.ToString();
                    break;
                case "tcp source port":
                    txtValue.Text = _event.TcpSrcPort.ToString();
                    break;
                case "udp source port":
                    txtValue.Text = _event.UdpSrcPort.ToString();
                    break;
                case "destination ip":
                    ipValue.Text = _event.IpDst.ToString();
                    break;
                case "tcp destination port":
                    txtValue.Text = _event.TcpDstPort.ToString();
                    break;
                case "udp destination port":
                    txtValue.Text = _event.UdpDstPort.ToString();
                    break;
                case "protocol":
                    UserInterface.LocateAndSelectNameValueCombo(cboValue, _event.Protocol, false);
                    break;
                case "payload (ascii)":
                    txtValue.Text = _event.PayloadAscii;
                    break;
                case "payload (hex)":
                    txtValue.Text = woanware.Text.ConvertByteArrayToHexString(_event.PayloadHex);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadConditions()
        {
            FilterDefinition fd = (FilterDefinition)cboField.Items[cboField.SelectedIndex];
            if (fd == null)
            {
                return;
            }

            switch (fd.Type)
            {
                case Global.FilterType.Ip:
                    cboCondition.Items.Clear();
                    cboCondition.Items.Add("=");
                    cboCondition.Items.Add("!=");

                    cboValue.Visible = false;
                    txtValue.Visible = false;
                    ipValue.Visible = true;
                    break;
                case Global.FilterType.Numeric:
                    cboCondition.Items.Clear();
                    cboCondition.Items.Add("=");
                    cboCondition.Items.Add("!=");

                    cboValue.Visible = false;
                    txtValue.Visible = true;
                    ipValue.Visible = false;
                    break;
               
                case Global.FilterType.Protocol:
                    cboCondition.Items.Clear();
                    cboCondition.Items.Add("=");
                    cboCondition.Items.Add("!=");

                    cboValue.Items.Clear();
                    cboValue.DisplayMember = "Name";
                    cboValue.ValueMember = "Value";
                    cboValue.Items.AddRange(_protocols.ToArray());
                    UserInterface.SetDropDownWidth(cboValue);

                    cboValue.Visible = true;
                    txtValue.Visible = false;
                    ipValue.Visible = false;
                    break;
                case Global.FilterType.Text:
                    cboCondition.Items.Clear();
                    cboCondition.Items.Add("=");
                    cboCondition.Items.Add("!=");
                    cboCondition.Items.Add("LIKE");
                    cboCondition.Items.Add("NOT LIKE");

                    cboValue.Visible = false;
                    txtValue.Visible = true;
                    ipValue.Visible = false;
                    break;
                case Global.FilterType.PayloadAscii:
                    cboCondition.Items.Clear();
                    cboCondition.Items.Add("=");
                    cboCondition.Items.Add("!=");
                    cboCondition.Items.Add("LIKE");
                    cboCondition.Items.Add("NOT LIKE");

                    cboValue.Visible = false;
                    txtValue.Visible = true;
                    ipValue.Visible = false;
                    break;
                case Global.FilterType.PayloadHex:
                    cboCondition.Items.Clear();
                    cboCondition.Items.Add("=");
                    cboCondition.Items.Add("!=");
                    cboCondition.Items.Add("LIKE");
                    cboCondition.Items.Add("NOT LIKE");

                    cboValue.Visible = false;
                    txtValue.Visible = true;
                    ipValue.Visible = false;
                    break;
            }

            cboCondition.Select();
            cboCondition.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fd"></param>
        /// <returns></returns>
        private string GetFilterValue(FilterDefinition fd)
        {
            switch (fd.Type)
            {
                case Global.FilterType.Protocol:
                    if (cboValue.SelectedIndex == -1)
                    {
                        return string.Empty;
                    }

                    NameValue nameValueClass = (NameValue)cboValue.Items[cboValue.SelectedIndex];
                    return nameValueClass.Value;
                case Global.FilterType.Ip:
                    long ip = Misc.ConvertIpAddress(ipValue.Text);
                    if (ip == 0)
                    {
                        throw new Exception("Invalid IP address");
                    }
                    return ip.ToString();
                case Global.FilterType.Numeric:
                case Global.FilterType.Text:
                case Global.FilterType.Timestamp:
                    return txtValue.Text;
                case Global.FilterType.PayloadAscii:
                    // Convert to HEX
                    string payloadAscii = txtValue.Text;
                    bool wildcardStart = payloadAscii.StartsWith("%");
                    bool wildcardEnd = payloadAscii.EndsWith("%");
                    if (wildcardStart == true)
                    {
                        payloadAscii = payloadAscii.Substring(1);
                    }

                    if (wildcardEnd == true)
                    {
                        payloadAscii = payloadAscii.Remove(payloadAscii.Length - 1);
                    }

                    payloadAscii = woanware.Text.ConvertTextToHex(payloadAscii);
                    if (wildcardStart == true)
                    {
                        payloadAscii = "%" + payloadAscii;
                    }

                    if (wildcardEnd == true)
                    {
                        payloadAscii =  payloadAscii + "%";
                    }

                    return payloadAscii;
                case Global.FilterType.PayloadHex:
                    string payloadHex = txtValue.Text;
                    bool wildcardStartHex = payloadHex.StartsWith("%");
                    bool wildcardEndHex = payloadHex.EndsWith("%");
                    if (wildcardStartHex == true)
                    {
                        payloadHex = payloadHex.Substring(1);
                    }

                    if (wildcardEndHex == true)
                    {
                        payloadHex = payloadHex.Remove(payloadHex.Length - 1);
                    }

                    if (woanware.Text.IsHex(payloadHex) == false)
                    {
                        throw new Exception("Invalid HEX value");
                    }

                    if (wildcardStartHex == true)
                    {
                        payloadHex = "%" + payloadHex;
                    }

                    if (wildcardEndHex == true)
                    {
                        payloadHex = payloadHex + "%";
                    }

                    return payloadHex; // Test if hex?
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fd"></param>
        /// <returns></returns>
        private string GetFilterDisplayValue(FilterDefinition fd)
        {
            switch (fd.Type)
            {
                case Global.FilterType.Ip:
                    return ipValue.Text;
                case Global.FilterType.Numeric:
                    return txtValue.Text;
                case Global.FilterType.Protocol:

                    if (cboValue.SelectedIndex == -1)
                    {
                        return string.Empty;
                    }

                    NameValue nameValueSeverity = (NameValue)cboValue.Items[cboValue.SelectedIndex];
                    return nameValueSeverity.Name;
                case Global.FilterType.SignatureId:
                    if (cboValue.SelectedIndex == -1)
                    {
                        return string.Empty;
                    }

                    NameValue nameValueSigId = (NameValue)cboValue.Items[cboValue.SelectedIndex];
                    return nameValueSigId.Value;
                case Global.FilterType.Text:
                    return txtValue.Text;
                case Global.FilterType.Timestamp:
                    return txtValue.Text;
                case Global.FilterType.PayloadAscii:
                    return txtValue.Text; 
                case Global.FilterType.PayloadHex:
                    return txtValue.Text; 
                default:
                    return string.Empty;
            }
        }

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public FalsePositive FalsePositive
        {
            get
            {
                return _falsePositive;
            }
            set
            {
                _falsePositive = value;

                //foreach (FilterDefinition fd in cboField.Items)
                //{
                //    if (fd.Field != _filter.Field)
                //    {
                //        continue;
                //    }

                //    cboField.SelectedItem = fd;
                //    break;
                //}

                //UserInterface.LocateAndSelectComboBoxValue(_filter.Condition, cboCondition);

                //switch (_filter.Definition.Type)
                //{
                //    case Global.FilterType.PayloadAscii:
                //        txtValue.Text = _filter.Display;
                //        break;
                //    case Global.FilterType.Ip:
                //        ipValue.Text = _filter.Display;
                //        break;
                //    case Global.FilterType.Numeric:
                //    case Global.FilterType.Text:
                //    case Global.FilterType.Timestamp:
                //    case Global.FilterType.PayloadHex:
                //        txtValue.Text = _filter.Value;
                //        break;
                //    case Global.FilterType.Severity:
                //        UserInterface.LocateAndSelectNameValueCombo(cboValue, _filter.Value, true);
                //        break;
                //    case Global.FilterType.SignatureName:
                //    case Global.FilterType.SignatureId:
                //    case Global.FilterType.Classification:
                //    case Global.FilterType.Sensor:
                //    case Global.FilterType.Protocol:
                //        UserInterface.LocateAndSelectNameValueCombo(cboValue, _filter.Value, false);
                //        break;
                //    default:
                //        break;
                //}
            }
        }
        #endregion

        #region Form Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormFilter_Load(object sender, EventArgs e)
        {
            this.Show();
            LoadProtocols();
        }
        #endregion
    }
}
