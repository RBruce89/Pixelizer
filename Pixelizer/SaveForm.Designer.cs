namespace Pixelizer
{
    partial class SaveForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveForm));
            this.saveButton = new System.Windows.Forms.Button();
            this.redoButton = new System.Windows.Forms.Button();
            this.savePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.savePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(12, 19);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(115, 50);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.Location = new System.Drawing.Point(153, 19);
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(115, 50);
            this.redoButton.TabIndex = 2;
            this.redoButton.Text = "Redo";
            this.redoButton.UseVisualStyleBackColor = true;
            this.redoButton.Click += new System.EventHandler(this.RedoButton_Click);
            // 
            // savePictureBox
            // 
            this.savePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.savePictureBox.Location = new System.Drawing.Point(12, 12);
            this.savePictureBox.Name = "savePictureBox";
            this.savePictureBox.Size = new System.Drawing.Size(256, 1);
            this.savePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.savePictureBox.TabIndex = 0;
            this.savePictureBox.TabStop = false;
            // 
            // SaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 79);
            this.Controls.Add(this.redoButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.savePictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pixel Image";
            this.Load += new System.EventHandler(this.SaveForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.savePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox savePictureBox;
        public System.Windows.Forms.Button saveButton;
        public System.Windows.Forms.Button redoButton;
    }
}