using Be.Windows.Forms;
using System.Windows.Forms;

namespace snorbert
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormPayload : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="temp"></param>
        public FormPayload(Event temp)
        {
            InitializeComponent();

            // Payload Tab (HEX)
            if (temp.PayloadHex != null)
            {
                DynamicByteProvider dynamicByteProvider = new DynamicByteProvider(temp.PayloadHex);
                hexEvent.ByteProvider = dynamicByteProvider;
            }
            else
            {
                DynamicByteProvider dynamicByteProvider = new DynamicByteProvider(new byte[] { });
                hexEvent.ByteProvider = dynamicByteProvider;
            }

            // Payload Tab (ASCII)
            txtPayloadAscii.Text = temp.PayloadAscii;
        }
    }
}
