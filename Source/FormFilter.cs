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
    public partial class FormFilter : Form
    {
        #region Member Variables
        private Sql _sql;
        private List<Filter> _filters;
        private Filter _filter;
        private List<FilterDefinition> _filterDefinitions;
        private List<NameValue> _signatures;
        private List<NameValue> _priorities;
        private List<NameValue> _classifications;
        private List<NameValue> _sensors;
        private List<NameValue> _protocols;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="filters"></param>
        public FormFilter(Sql sql, List<Filter> filters)
        {
            InitializeComponent();

            _filters = filters;
            _sql = sql;

            GenerateFilterDefinitions();

            cboField.Items.Clear();
            cboField.DisplayMember = "Field";
            cboField.ValueMember = "FieldName";
            cboField.Items.AddRange(_filterDefinitions.ToArray());
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void LoadSignatures()
        {
            using (new HourGlass(this))
            {
                _signatures = new List<NameValue>();
                var dbSignatures = new DbSignature();
                var results = dbSignatures.Query((_sql.GetQuery(Sql.Query.SQL_SIG_NAMES)));
                foreach (var result in results)
                {
                    NameValue nameValue = new NameValue();
                    nameValue.Name = result.sig_name;
                    nameValue.Value = result.sig_sid.ToString();
                    _signatures.Add(nameValue);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadPriorities()
        {
            using (new HourGlass(this))
            {
                _priorities = new List<NameValue>();
                var dbSignatures = new DbSignature();
                var results = dbSignatures.Query(_sql.GetQuery(Sql.Query.SQL_SIG_PRIORITIES));
                foreach (var result in results)
                {
                    NameValue nameValue = new NameValue();
                    if (result.sig_priority == null)
                    {
                        nameValue.Name = "IS NULL";
                        nameValue.Value = "IS NULL";
                    }
                    else
                    {
                        nameValue.Name = result.sig_priority.ToString();
                        nameValue.Value = result.sig_priority.ToString();
                    }
                    
                    _priorities.Add(nameValue);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadClassifications()
        {
            using (new HourGlass(this))
            {
                _classifications = new List<NameValue>();
                var dbClassification = new DbClassification();
                var results = dbClassification.Query(_sql.GetQuery(Sql.Query.SQL_SIG_CLASS));
                foreach (var result in results)
                {
                    NameValue nameValue = new NameValue();
                    nameValue.Name = result.sig_class_name;
                    nameValue.Value = result.sig_class_id.ToString();
                    _classifications.Add(nameValue);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadSensors()
        {
            using (new HourGlass(this))
            {
                _sensors = new List<NameValue>();
                var dbSensor = new DbSensor();
                var results = dbSensor.Query(_sql.GetQuery(Sql.Query.SQL_SENSORS));
                foreach (var result in results)
                {
                    NameValue nameValue = new NameValue();
                    nameValue.Name = result.hostname;
                    nameValue.Value = result.sid.ToString();
                    _sensors.Add(nameValue);
                }
            }
        }

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
            AddFilterDefinition("Classification", "signature.sig_class_id", Global.FilterType.Classification);
            AddFilterDefinition("Signature", "signature.sig_sid", Global.FilterType.SignatureId);
            AddFilterDefinition("Signature Name", "signature.sig_sid", Global.FilterType.SignatureName);
            AddFilterDefinition("Start Time", "event.timestamp", Global.FilterType.Timestamp);
            AddFilterDefinition("End Time", "event.timestamp", Global.FilterType.Timestamp);
            AddFilterDefinition("Payload (ASCII)", "data.data_payload", Global.FilterType.PayloadAscii);
            AddFilterDefinition("Payload (HEX)", "data.data_payload", Global.FilterType.PayloadHex);
            AddFilterDefinition("Severity", "signature.sig_priority", Global.FilterType.Severity);
            AddFilterDefinition("Sensor", "event.sid", Global.FilterType.Sensor);
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

            Filter tempFilter = new Filter();
            tempFilter.Definition = fd;
            tempFilter.Condition = cboCondition.Items[cboCondition.SelectedIndex].ToString();
            tempFilter.Value = GetFilterValue(fd);
            tempFilter.Display = GetFilterDisplayValue(fd);

            // Make sure the filter does not already exist
            var temp = from f in _filters
                      where f.Condition == tempFilter.Condition &
                            f.Definition == tempFilter.Definition &
                            f.Value == tempFilter.Value &
                            f.Id != tempFilter.Id
                     select f;

            if (temp.Any() == true)
            {
                UserInterface.DisplayMessageBox(this, "The filter already exists", MessageBoxIcon.Exclamation);
                return;
            }

            // New filter
            if (_filter == null)
            {
                _filter = new Filter();
            }

            _filter.Condition = tempFilter.Condition;
            _filter.Definition = tempFilter.Definition;
            _filter.Value = tempFilter.Value;
            _filter.Display = tempFilter.Display;

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
                case Global.FilterType.Classification:
                    cboCondition.Items.Clear();
                    cboCondition.Items.Add("=");
                    cboCondition.Items.Add("!=");

                    cboValue.Items.Clear();
                    cboValue.DisplayMember = "Name";
                    cboValue.ValueMember = "Value";
                    cboValue.Items.AddRange(_classifications.ToArray());
                    UserInterface.SetDropDownWidth(cboValue);
                    
                    cboValue.Visible = true;
                    txtValue.Visible = false;
                    ipValue.Visible = false;
                    break;
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
                case Global.FilterType.Sensor:
                    cboCondition.Items.Clear();
                    cboCondition.Items.Add("=");
                    cboCondition.Items.Add("!=");

                    cboValue.Items.Clear();
                    cboValue.DisplayMember = "Name";
                    cboValue.ValueMember = "Value";
                    cboValue.Items.AddRange(_sensors.ToArray());
                    UserInterface.SetDropDownWidth(cboValue);

                    cboValue.Visible = true;
                    txtValue.Visible = false;
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
                case Global.FilterType.Severity:
                    cboCondition.Items.Clear();
                    cboCondition.Items.Add("=");
                    cboCondition.Items.Add("!=");

                    cboValue.Items.Clear();
                    cboValue.DisplayMember = "Name";
                    cboValue.ValueMember = "Value";
                    cboValue.Items.AddRange(_priorities.ToArray());
                    UserInterface.SetDropDownWidth(cboValue);
                    
                    cboValue.Visible = true;
                    txtValue.Visible = false;
                    ipValue.Visible = false;
                    break;
                case Global.FilterType.SignatureName :
                    cboCondition.Items.Clear();
                    cboCondition.Items.Add("=");
                    cboCondition.Items.Add("!=");
                    cboCondition.Items.Add("LIKE");
                    cboCondition.Items.Add("NOT LIKE");

                    cboValue.Items.Clear();
                    cboValue.DisplayMember = "Name";
                    cboValue.ValueMember = "Value";
                    cboValue.Items.AddRange(_signatures.ToArray());
                    UserInterface.SetDropDownWidth(cboValue);

                    cboValue.Visible = true;
                    txtValue.Visible = false;
                    ipValue.Visible = false;
                    break;
                case Global.FilterType.SignatureId:
                    cboCondition.Items.Clear();
                    cboCondition.Items.Add("=");
                    cboCondition.Items.Add("!=");
                    cboCondition.Items.Add("LIKE");
                    cboCondition.Items.Add("NOT LIKE");

                    cboValue.Items.Clear();
                    cboValue.DisplayMember = "Value";
                    cboValue.ValueMember = "Name";
                    cboValue.Items.AddRange(_signatures.ToArray());
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
                case Global.FilterType.Timestamp:
                    cboCondition.Items.Clear();
                    cboCondition.Items.Add("=");
                    cboCondition.Items.Add("!=");
                    cboCondition.Items.Add(">");
                    cboCondition.Items.Add(">=");
                    cboCondition.Items.Add("<");
                    cboCondition.Items.Add("<=");

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
                case Global.FilterType.Classification:
                case Global.FilterType.Severity:
                case Global.FilterType.Sensor:
                case Global.FilterType.SignatureName:
                case Global.FilterType.SignatureId:
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
                case Global.FilterType.Severity:
                case Global.FilterType.SignatureName:
                case Global.FilterType.Sensor:
                case Global.FilterType.Classification:
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
        public Filter Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;

                foreach (FilterDefinition fd in cboField.Items)
                {
                    if (fd.Field != _filter.Field)
                    {
                        continue;
                    }

                    cboField.SelectedItem = fd;
                    break;
                }

                UserInterface.LocateAndSelectComboBoxValue(_filter.Condition, cboCondition);

                switch (_filter.Definition.Type)
                {
                    case Global.FilterType.PayloadAscii:
                        txtValue.Text = _filter.Display;
                        break;
                    case Global.FilterType.Ip:
                        ipValue.Text = _filter.Display;
                        break;
                    case Global.FilterType.Numeric:
                    case Global.FilterType.Text:
                    case Global.FilterType.Timestamp:
                    case Global.FilterType.PayloadHex:
                        txtValue.Text = _filter.Value;
                        break;
                    case Global.FilterType.Severity:
                        UserInterface.LocateAndSelectNameValueCombo(cboValue, _filter.Value, true);
                        break;
                    case Global.FilterType.SignatureName:
                    case Global.FilterType.SignatureId:
                    case Global.FilterType.Classification:
                    case Global.FilterType.Sensor:
                    case Global.FilterType.Protocol:
                        UserInterface.LocateAndSelectNameValueCombo(cboValue, _filter.Value, false);
                        break;
                    default:
                        break;
                }
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
            LoadSignatures();
            LoadPriorities();
            LoadClassifications();
            LoadSensors();
            LoadProtocols();
        }
        #endregion
    }
}
