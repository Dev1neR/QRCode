using System;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRCode
{
    public partial class QRCode : Form
    {
        public QRCode()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Image Files(*.BMP; *.JPG; *.PNG)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
            saveFileDialog1.Filter = "Image Files(*.BMP; *.JPG; *.PNG)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            string encoding = tbEnc.Text;
            QRCodeEncoder enc = new QRCodeEncoder();
            Bitmap qrcode = enc.Encode(encoding);
            pbQR.Image = qrcode;
            pbQR.SizeMode = PictureBoxSizeMode.CenterImage;

            if (MessageBox.Show("Чи бажаєте ви зберегти отриманий QRCode?", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    string filename = saveFileDialog1.FileName;
                    qrcode.Save(filename);
                }
            }         
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                string filename = openFileDialog1.FileName;
                pbQRDec.Image = Image.FromFile(filename);
                pbQRDec.SizeMode = PictureBoxSizeMode.CenterImage;
                QRCodeDecoder dec = new QRCodeDecoder();
                tbDec.Text = (dec.decode(new QRCodeBitmapImage(pbQRDec.Image as Bitmap)));
            }
        }

        private void btnEncode_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.btnEncode, "Зробити QRCode з вказаного тексту у рядку");
        }

        private void btnDecode_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip2 = new System.Windows.Forms.ToolTip();
            ToolTip2.SetToolTip(this.btnDecode, "Розкодувати завантажений QRCode та вивести результат у рядок");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About abt = new About();
            {
                abt.ShowDialog();
            }
        }
    }
}
