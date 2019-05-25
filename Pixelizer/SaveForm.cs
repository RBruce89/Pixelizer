using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixelizer
{
    public partial class SaveForm : Form
    {
    
        public SaveForm()
        {
            InitializeComponent();
        }

        private void SaveForm_Load(object sender, EventArgs e)
        {
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveImageDialog = new SaveFileDialog
            {
                Filter = ".PNG file (*.png)|*.png",
                FilterIndex = 1,
                RestoreDirectory = true,
                ValidateNames = true
            };

            if (saveImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {   
                savePictureBox.Image.Save(saveImageDialog.FileName);
                savePictureBox.Image.Dispose();
            }
            this.Close();
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Retry;
        }
    }
}
