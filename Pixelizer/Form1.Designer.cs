namespace Pixelizer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.selectFileButton = new System.Windows.Forms.Button();
            this.enterTextButton = new System.Windows.Forms.Button();
            this.inputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.dragDropLabel = new System.Windows.Forms.Label();
            this.convertTextButton = new System.Windows.Forms.Button();
            this.saveTextButton = new System.Windows.Forms.Button();
            this.dividerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectFileButton
            // 
            this.selectFileButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.selectFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectFileButton.Location = new System.Drawing.Point(12, 12);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(169, 64);
            this.selectFileButton.TabIndex = 0;
            this.selectFileButton.Text = "Select File";
            this.selectFileButton.UseVisualStyleBackColor = false;
            this.selectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // enterTextButton
            // 
            this.enterTextButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.enterTextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enterTextButton.Location = new System.Drawing.Point(12, 94);
            this.enterTextButton.Name = "enterTextButton";
            this.enterTextButton.Size = new System.Drawing.Size(169, 64);
            this.enterTextButton.TabIndex = 1;
            this.enterTextButton.Text = "Enter Text ↓";
            this.enterTextButton.UseVisualStyleBackColor = false;
            this.enterTextButton.Click += new System.EventHandler(this.EnterTextButton_Click);
            // 
            // inputRichTextBox
            // 
            this.inputRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputRichTextBox.Location = new System.Drawing.Point(12, 202);
            this.inputRichTextBox.Name = "inputRichTextBox";
            this.inputRichTextBox.Size = new System.Drawing.Size(423, 423);
            this.inputRichTextBox.TabIndex = 4;
            this.inputRichTextBox.TabStop = false;
            this.inputRichTextBox.Text = "";
            this.inputRichTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.Control_DragDrop);
            this.inputRichTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.Control_DragEnter);
            this.inputRichTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InputRichTextBox_MouseDown);
            // 
            // dragDropLabel
            // 
            this.dragDropLabel.AllowDrop = true;
            this.dragDropLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dragDropLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dragDropLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dragDropLabel.Location = new System.Drawing.Point(187, 12);
            this.dragDropLabel.Name = "dragDropLabel";
            this.dragDropLabel.Size = new System.Drawing.Size(248, 146);
            this.dragDropLabel.TabIndex = 0;
            this.dragDropLabel.Text = "Drag and Drop";
            this.dragDropLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dragDropLabel.DragDrop += new System.Windows.Forms.DragEventHandler(this.Control_DragDrop);
            this.dragDropLabel.DragEnter += new System.Windows.Forms.DragEventHandler(this.Control_DragEnter);
            // 
            // convertTextButton
            // 
            this.convertTextButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.convertTextButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.convertTextButton.Location = new System.Drawing.Point(79, 173);
            this.convertTextButton.Name = "convertTextButton";
            this.convertTextButton.Size = new System.Drawing.Size(84, 23);
            this.convertTextButton.TabIndex = 2;
            this.convertTextButton.TabStop = false;
            this.convertTextButton.Text = "Convert Text";
            this.convertTextButton.UseVisualStyleBackColor = false;
            this.convertTextButton.Click += new System.EventHandler(this.ConvertTextButton_Click);
            // 
            // saveTextButton
            // 
            this.saveTextButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.saveTextButton.Location = new System.Drawing.Point(284, 173);
            this.saveTextButton.Name = "saveTextButton";
            this.saveTextButton.Size = new System.Drawing.Size(84, 23);
            this.saveTextButton.TabIndex = 3;
            this.saveTextButton.TabStop = false;
            this.saveTextButton.Text = "Save Text";
            this.saveTextButton.UseVisualStyleBackColor = false;
            this.saveTextButton.Click += new System.EventHandler(this.SaveTextButton_Click);
            // 
            // dividerLabel
            // 
            this.dividerLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dividerLabel.Location = new System.Drawing.Point(4, 164);
            this.dividerLabel.Name = "dividerLabel";
            this.dividerLabel.Size = new System.Drawing.Size(440, 3);
            this.dividerLabel.TabIndex = 0;
            this.dividerLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Pixelizer.Properties.Resources.BWTexture;
            this.ClientSize = new System.Drawing.Size(447, 171);
            this.Controls.Add(this.dividerLabel);
            this.Controls.Add(this.saveTextButton);
            this.Controls.Add(this.convertTextButton);
            this.Controls.Add(this.dragDropLabel);
            this.Controls.Add(this.inputRichTextBox);
            this.Controls.Add(this.enterTextButton);
            this.Controls.Add(this.selectFileButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(463, 210);
            this.MinimumSize = new System.Drawing.Size(463, 210);
            this.Name = "Form1";
            this.Text = "Pixelizer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button selectFileButton;
        private System.Windows.Forms.Button enterTextButton;
        private System.Windows.Forms.RichTextBox inputRichTextBox;
        private System.Windows.Forms.Label dragDropLabel;
        private System.Windows.Forms.Button convertTextButton;
        private System.Windows.Forms.Button saveTextButton;
        private System.Windows.Forms.Label dividerLabel;
    }
}

