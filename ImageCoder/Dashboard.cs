using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageCoder
{
    public partial class Dashboard : Form
    {


        public Dashboard()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            open.ShowDialog();
            var raw = new Bitmap(open.FileName);
            Bitmap conv = Decode(raw);

            imgBox.BackgroundImage = conv;
        }

        private static Bitmap Decode(Bitmap raw)
        {
            var conv = new Bitmap(raw.Width / 2, raw.Height / 2);

            for (int x = 0; x < conv.Width; x++)
            {
                for (int y = 0; y < conv.Height; y++)
                {
                    conv.SetPixel(x, y, raw.GetPixel(x * 2 + 1, y * 2 + 1));
                }
            }

            return conv;
        }

        private static Bitmap Encode(Bitmap original, Bitmap secret)
        {
            var raw = new Bitmap(original);
            for (int x = 0; x < secret.Width; x++)
            {
                for (int y = 0; y < secret.Height; y++)
                {
                    raw.SetPixel(x * 2 +1, y * 2+1, secret.GetPixel(x, y));
                }
            }
            return raw;
        }

        private void ButtonEncode_Click(object sender, EventArgs e)
        {
            OpenFileDialog openOrig = new OpenFileDialog();
            OpenFileDialog openSecr = new OpenFileDialog();

            openOrig.ShowDialog();
            openSecr.ShowDialog();

            var enc = Encode(new Bitmap(openOrig.FileName), new Bitmap(openSecr.FileName));
            imgBox.BackgroundImage = enc;
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            var save = new SaveFileDialog();
            save.ShowDialog();
            new Bitmap(imgBox.BackgroundImage).Save(save.FileName);
        }
    }
}
