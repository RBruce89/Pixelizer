using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixelizer
{
    public partial class Form1 : Form
    {
        // Declare variables to scramble information and format resulting image.
        byte[] keyCoordinates = new byte[2];
        byte unusedCharacters = 0;
        byte unusedPixels = 0;
        byte[] shiftKey = new byte[3];
        Random randomValue = new Random();

        Bitmap pixelImage = new Bitmap(1, 1);
        int imageWidth;

        int textLength;
        int[] CharacterUCode;

        public Form1()
        {
            InitializeComponent();
            this.inputRichTextBox.AllowDrop = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Control_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Control_DragDrop(object sender, DragEventArgs e)
        {
            // Accept file and convert accordingly.
            string[] droppedFile = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (Path.GetExtension(droppedFile[0].ToLower()) == ".txt")
            {
                inputRichTextBox.Text = File.ReadAllText(droppedFile[0]);
                TextToImageVariables();
            }
            else if (Path.GetExtension(droppedFile[0].ToLower()) == ".png")
            {
                pixelImage = (Bitmap)Image.FromFile(droppedFile[0]);
                ImageToText();
                if (this.Height == 210)
                {
                    SaveTxt();
                }
            }
            else
            {
                MessageBox.Show("The selected file is unsupported. \n Please use a .txt or .png", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            // Select file and convert accordingly.
            OpenFileDialog selectFileDialog = new OpenFileDialog
            {
                Filter = ".PNG or .TXT file (*.png, *.txt)|*.png;*.txt",
                FilterIndex = 1,
                RestoreDirectory = true,
                Title = "Choose plain text or image",
            };
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(selectFileDialog.FileName.ToLower()) == ".txt")
                {
                    inputRichTextBox.Text = File.ReadAllText(selectFileDialog.FileName);
                    TextToImageVariables();
                }
                else if (Path.GetExtension(selectFileDialog.FileName.ToLower()) == ".png")
                {
                    pixelImage = (Bitmap)Image.FromFile(selectFileDialog.FileName);
                    ImageToText();
                    if (this.Height == 210)
                    {
                        SaveTxt();
                    }
                }
                else
                {
                    MessageBox.Show("The selected file is unsupported. \n Please select a .txt or .png", "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void EnterTextButton_Click(object sender, EventArgs e)
        {
            // Change UI setup around showing inputRichTextBox or not.
            if (this.Height == 210)
            {
                enterTextButton.Text = "Hide Text ↑";
                this.Height = 675;
                this.MinimumSize = new Size(463, 675);
                this.MaximumSize = new Size(463, 675);
                convertTextButton.TabStop = true;
                saveTextButton.TabStop = true;
                inputRichTextBox.TabStop = true;
                dividerLabel.Visible = true;
                inputRichTextBox.Focus();
                if ((this.DesktopLocation.Y + 708 > Screen.FromControl(this).Bounds.Height) && (Screen.FromControl(this).Bounds.Height >= 708))
                {
                    this.DesktopLocation = new Point(this.DesktopLocation.X , (Screen.FromControl(this).Bounds.Height - 708));
                }
            }
            else
            {
                enterTextButton.Text = "Enter Text ↓";
                this.Height = 210;
                this.MinimumSize = new Size(463, 210);
                this.MaximumSize = new Size(463, 210);
                convertTextButton.TabStop = false;
                saveTextButton.TabStop = false;
                inputRichTextBox.TabStop = false;
                dividerLabel.Visible = false;
                selectFileButton.Focus();
            }
        }

        private void ConvertTextButton_Click(object sender, EventArgs e)
        {
            TextToImageVariables();
        }

        private void SaveTextButton_Click(object sender, EventArgs e)
        {
            SaveTxt();
        }

        private void SaveTxt()
        {
            // Save text in inputRichTextBox to .txt file.
            SaveFileDialog saveTextFileDialog = new SaveFileDialog
            {
                Filter = ".TXT file (*.txt)|*.txt",
                FilterIndex = 1,
                RestoreDirectory = true,
                ValidateNames = true
            };

            if (saveTextFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StringBuilder displayText = new StringBuilder(inputRichTextBox.Text);
                displayText.Replace("\r\n", "\r");
                displayText.Replace("\n", "\r");
                displayText.Replace("\r", "\r\n");
                File.WriteAllText(saveTextFileDialog.FileName, displayText.ToString());
            }
        }

        private void InputRichTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            // Setup and display right click menu for inputRichTextBox.
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenu inputMenu = new System.Windows.Forms.ContextMenu();
                MenuItem inputMenuItem = new MenuItem("Cut");
                inputMenuItem.Click += new EventHandler(InputCut);
                inputMenu.MenuItems.Add(inputMenuItem);
                inputMenuItem = new MenuItem("Copy");
                inputMenuItem.Click += new EventHandler(InputCopy);
                inputMenu.MenuItems.Add(inputMenuItem);
                inputMenuItem = new MenuItem("Paste");
                inputMenuItem.Click += new EventHandler(InputPaste);
                inputMenu.MenuItems.Add(inputMenuItem);
                inputRichTextBox.ContextMenu = inputMenu;
            }
        }
        void InputCut(object sender, EventArgs e)
        {
            inputRichTextBox.Cut();
        }
        void InputCopy(object sender, EventArgs e)
        {
            if (inputRichTextBox.SelectedText != null && inputRichTextBox.SelectedText != "")
            {
                Clipboard.SetText(inputRichTextBox.SelectedText);
            }
        }
        void InputPaste(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                int insertionPoint = inputRichTextBox.SelectionStart;
                if (inputRichTextBox.SelectedText != null && inputRichTextBox.SelectedText != "")
                {
                    inputRichTextBox.Text = inputRichTextBox.Text.Remove(inputRichTextBox.SelectionStart, inputRichTextBox.SelectionLength);
                }
                inputRichTextBox.Text = inputRichTextBox.Text.Insert(inputRichTextBox.SelectionStart, Clipboard.GetText(TextDataFormat.Text).ToString());
                inputRichTextBox.SelectionStart = insertionPoint + (Clipboard.GetText(TextDataFormat.Text).ToString().Length);
            }
        }

        private byte SetRandom(byte min, byte max)
        {
            // Use ticks to obtain random numbers.
            System.Threading.Thread.Sleep(2);
            String randomTimeString = DateTime.Now.Ticks.ToString();
            int randomTime = int.Parse(randomTimeString.Remove(0, (randomTimeString.Length - 3)));
            int randomKey = randomTime + randomValue.Next(0, 100);
            while (randomKey < min || randomKey > max)
            {
                randomTimeString = DateTime.Now.Ticks.ToString();
                randomTime = int.Parse(randomTimeString.Remove(0, (randomTimeString.Length - 2)));
                randomKey = randomTime + randomValue.Next(0, 255);
            }
            return Convert.ToByte(randomValue.Next(0, 255));
        }

        private void TextToImageVariables()
        {
            // Set variables to scramble text in inputRichTextBox and format resulting picture.
            textLength = inputRichTextBox.Text.Length;

            keyCoordinates[0] = SetRandom(4, 255);
            keyCoordinates[1] = SetRandom(0, 255);
            shiftKey[0] = SetRandom(0, 254);
            shiftKey[1] = SetRandom(0, 254);
            shiftKey[2] = SetRandom(0, 254);

            if (textLength < 16376)
            {
                imageWidth = 64;
            }
            else if (textLength < 65528)
            {
                imageWidth = 128;
            }
            else
            {
                imageWidth = 256;
            }

            if (textLength % 4 > 0)
            {
                unusedCharacters = Convert.ToByte(4 - (textLength % 4));
            }
            else
            {
                unusedCharacters = 0;
            }
            if ((((textLength + unusedCharacters) / 4) + 2) % 256 > 0)
            {
                unusedPixels = Convert.ToByte(256 - ((((textLength + unusedCharacters) / 4) + 2) % 256));
            }
            else
            {
                unusedPixels = 0;
            }       

            CharacterUCode = new int[(textLength + unusedCharacters + 8) + (unusedPixels * 4)];
            String textInBox = inputRichTextBox.Text;

            for (var i = 0; i < textLength; i++)
            {
                char currentChar = Convert.ToChar(textInBox.Substring(i, 1));
                CharacterUCode[i] = currentChar;
            }

            TextToImageSafety();
        }

        private void TextToImageSafety()
        {
            // Determine if there are any characters with a u-code above 255.
            String invalidChars = null;
            for (var i = 0; i < textLength; i++)
            {
                if (CharacterUCode[i] > 255)
                {
                    // Swaps characters with similar characters that have U-Codes under 255.
                    if (CharacterUCode[i] == 8211 || CharacterUCode[i] == 8212 || CharacterUCode[i] == 8213)
                    {
                        CharacterUCode[i] = 45; // -
                    }
                    else if (CharacterUCode[i] == 8220 || CharacterUCode[i] == 8221)
                    {
                        CharacterUCode[i] = 34; // "
                    }
                    else if (CharacterUCode[i] == 8216 || CharacterUCode[i] == 8217)
                    {
                        CharacterUCode[i] = 39; // '
                    }
                    else
                    {
                        // Highlights characters with U-Codes above 255, and adds them to a list.
                        inputRichTextBox.SelectionStart = i;
                        inputRichTextBox.SelectionLength = 1;
                        inputRichTextBox.SelectionBackColor = Color.Red;
                        Boolean redundantChar = false;
                        if (invalidChars != null)
                        {
                            for (var c = 0; c < invalidChars.Length; c++)
                            {
                                if (inputRichTextBox.Text.Substring(i, 1) == invalidChars.Substring(c, 1))
                                {
                                    redundantChar = true;
                                }
                            }
                        }
                        if (redundantChar == false)
                        {
                            if (invalidChars == null)
                            {
                                invalidChars = inputRichTextBox.Text.Substring(i, 1);
                            }
                            else if (invalidChars.Length < 28)
                            {
                                invalidChars += ", " + inputRichTextBox.Text.Substring(i, 1);
                            }
                            else if (invalidChars.Length == 28)
                            {
                                invalidChars += ", and others";
                            }
                        }
                    }
                }
            }

            if (invalidChars != null)
            {
                // Gives user a list of out of range characters, and gives them the option to halt processing or remove them.
                DialogResult safetyDialogResult;
                if (invalidChars.Length == 1)
                {
                    safetyDialogResult = MessageBox.Show(invalidChars + " is unsupported. Would you like to remove it?", "Invalid character", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
                else if (invalidChars.Length == 40)
                {
                    safetyDialogResult = MessageBox.Show(invalidChars + " are unsupported. Would you like to remove them?", "Invalid characters", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
                else
                {
                    safetyDialogResult = MessageBox.Show(invalidChars.Insert(invalidChars.Length - 1, "and, ") + " are unsupported. Would you like to remove them?", "Invalid characters",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }

                if (safetyDialogResult == DialogResult.Yes)
                {
                    for (var i = textLength; i >= 0; i--)
                    {
                        if (CharacterUCode[i] > 255)
                        {
                            inputRichTextBox.SelectionStart = i;
                            inputRichTextBox.SelectionLength = 1;
                            inputRichTextBox.SelectedText = "";
                        }
                    }
                    TextToImageVariables();
                }
            }
            else
            {
                TextToImageConversion();
            }
        }

        private void TextToImageConversion()
        {
            // Scramble character values, put them in pixelMap array, and save it as a .png.

            // Puts values needed to dicypher picture in pixelMap array.
            byte[] pixelMap = new byte[(textLength + unusedCharacters + 8) + (unusedPixels * 4)];
            pixelMap[0] = keyCoordinates[0];
            pixelMap[1] = keyCoordinates[1];
            pixelMap[2] = unusedCharacters;
            pixelMap[3] = unusedPixels;
            pixelMap[keyCoordinates[0] + keyCoordinates[1]] = shiftKey[0];
            pixelMap[keyCoordinates[0] + keyCoordinates[1] + 1] = shiftKey[1];
            pixelMap[keyCoordinates[0] + keyCoordinates[1] + 2] = shiftKey[2];
            pixelMap[keyCoordinates[0] + keyCoordinates[1] + 3] = Convert.ToByte(randomValue.Next(256));

            // Scrambles values from plain text and stores them in pixelMap array.
            int characterCounter = 1;
            for (var i = 4; i < ((textLength + unusedCharacters + 8) + (unusedPixels * 4)); i++)
            {
                if (i < keyCoordinates[0] + keyCoordinates[1] + 4 && i >= keyCoordinates[0] + keyCoordinates[1])
                {
                    continue;
                }
                if (characterCounter > textLength)
                {
                    CharacterUCode[characterCounter - 1] = Convert.ToByte(randomValue.Next(32, 126));
                }
                if (characterCounter % 2 == 0)
                {
                    if (CharacterUCode[characterCounter - 1] + shiftKey[characterCounter % 3] < 256)
                    {
                        pixelMap[i] = Convert.ToByte(CharacterUCode[characterCounter - 1] + shiftKey[characterCounter % 3]);
                    }
                    else
                    {
                        pixelMap[i] = Convert.ToByte(CharacterUCode[characterCounter - 1] + shiftKey[characterCounter % 3] - 256);
                    }
                }
                else
                {
                    if (CharacterUCode[characterCounter - 1] - shiftKey[characterCounter % 3] > -1)
                    {
                        pixelMap[i] = Convert.ToByte(CharacterUCode[characterCounter - 1] - shiftKey[characterCounter % 3]);
                    }
                    else
                    {
                        pixelMap[i] = Convert.ToByte(CharacterUCode[characterCounter - 1] - shiftKey[characterCounter % 3] + 256);
                    }
                }
                characterCounter += 1;
            }

            // Creates image.
            unsafe
            {
                fixed (byte* ptr = pixelMap)
                {
                    pixelImage = new Bitmap(imageWidth, pixelMap.Length / (imageWidth * 4), imageWidth * 4, PixelFormat.Format32bppArgb, new IntPtr(ptr));
                }
            }

            // Formats and displays save form.
            DialogResult saveFormDialog = DialogResult.OK;
            SaveForm saveForm = new SaveForm();
            if (((pixelImage.Height * (256 / imageWidth)) + 118) < Screen.FromControl(this).Bounds.Height)
            {
                saveForm.savePictureBox.Height = pixelImage.Height * (256 / imageWidth);
                saveForm.Height = (pixelImage.Height * (256 / imageWidth)) + 118;
                saveForm.MinimumSize = new Size(296, (pixelImage.Height * (256 / imageWidth)) + 118);
            }
            else
            {
                saveForm.savePictureBox.Height = Screen.FromControl(this).Bounds.Height - 160;
                saveForm.Height = Screen.FromControl(this).Bounds.Height;
                saveForm.MinimumSize = new Size(296, Screen.FromControl(this).Bounds.Height);
            }
            saveForm.MaximumSize = saveForm.MinimumSize;
            saveForm.saveButton.Location = new Point(12, saveForm.Height - 100);
            saveForm.redoButton.Location = new Point(153, saveForm.Height - 100);
            saveForm.savePictureBox.Image = GetCopyOf(pixelImage);
            pixelImage.Dispose();
            saveFormDialog = saveForm.ShowDialog();

            if (saveFormDialog == DialogResult.Retry)
            {
                TextToImageVariables();
            }
        }

        private static Bitmap GetCopyOf(Bitmap originalImage)
        {
            // Copy bitmap and perserve original format.
            Bitmap returnImage = new Bitmap(originalImage.Width, originalImage.Height, originalImage.PixelFormat);
            Rectangle rect = new Rectangle(0, 0, originalImage.Width, originalImage.Height);
            BitmapData originalData = originalImage.LockBits(rect, ImageLockMode.ReadOnly, originalImage.PixelFormat);
            BitmapData returnData = returnImage.LockBits(rect, ImageLockMode.WriteOnly, originalImage.PixelFormat);

            int imageDataLength = Math.Abs(originalData.Stride) * originalImage.Height;
            byte[] imageData = new byte[imageDataLength];
            System.Runtime.InteropServices.Marshal.Copy(originalData.Scan0, imageData, 0, imageData.Length);
            System.Runtime.InteropServices.Marshal.Copy(imageData, 0, returnData.Scan0, imageData.Length);
            
            originalImage.UnlockBits(originalData);
            returnImage.UnlockBits(returnData);

            return returnImage;
        }

        private void ImageToText()
        {
            // Decypher picture and display original message. 
            if (pixelImage.Width == 64 || pixelImage.Width == 128 || pixelImage.Width == 256)
            {
                StringBuilder decipheredText = new StringBuilder();
                byte[] pixelMap = new byte[pixelImage.Width * pixelImage.Height * 4];
                imageWidth = pixelImage.Width;

                // Converts pixel colors back to values in pixelMap array.
                for (var x = 0; x < pixelImage.Width; x++)
                {
                    for (var y = 0; y < pixelImage.Height; y++)
                    {
                        int offset = ((imageWidth * 4) * y) + (x * 4);
                        Color pixelColor = pixelImage.GetPixel(x, y);
                        pixelMap[offset] = pixelColor.B;
                        pixelMap[offset + 1] = pixelColor.G;
                        pixelMap[offset + 2] = pixelColor.R;
                        pixelMap[offset + 3] = pixelColor.A;
                    }
                }
                
                keyCoordinates[0] = pixelMap[0];
                keyCoordinates[1] = pixelMap[1];
                shiftKey[0] = pixelMap[pixelMap[0] + pixelMap[1]];
                shiftKey[1] = pixelMap[pixelMap[0] + pixelMap[1] + 1];
                shiftKey[2] = pixelMap[pixelMap[0] + pixelMap[1] + 2];
                unusedCharacters = pixelMap[2];
                unusedPixels = pixelMap[3];
                
                // Decyphers scrambled values, and displays original message in inputRichTextBox.
                int characterCounter = 1;
                byte Remainingkeys = 4;
                for (var i = 4; i < (pixelMap.Length - unusedCharacters - Remainingkeys - (unusedPixels * 4)); i++)
                {
                    if (i < keyCoordinates[0] + keyCoordinates[1] + 4 && i >= keyCoordinates[0] + keyCoordinates[1])
                    {
                        Remainingkeys -= 1;
                        continue;
                    }
                    if (characterCounter % 2 == 0)
                    {
                        if (pixelMap[i] - shiftKey[characterCounter % 3] > -1)
                        {
                            pixelMap[i] = Convert.ToByte(pixelMap[i] - shiftKey[characterCounter % 3]);
                            decipheredText.Append(Convert.ToChar(pixelMap[i]));
                        }
                        else
                        {
                            pixelMap[i] = Convert.ToByte(pixelMap[i] - shiftKey[characterCounter % 3] + 256);
                            decipheredText.Append(Convert.ToChar(pixelMap[i]));
                        }
                    }
                    else
                    {
                        if (pixelMap[i] + shiftKey[characterCounter % 3] < 256)
                        {
                            pixelMap[i] = Convert.ToByte(pixelMap[i] + shiftKey[characterCounter % 3]);
                            decipheredText.Append(Convert.ToChar(pixelMap[i]));
                        }
                        else
                        {
                            pixelMap[i] = Convert.ToByte(pixelMap[i] + shiftKey[characterCounter % 3] - 256);
                            decipheredText.Append(Convert.ToChar(pixelMap[i]));
                        }
                    }
                    characterCounter += 1;
                }

                inputRichTextBox.Text = decipheredText.ToString();
            }

            else
            {
                MessageBox.Show("The Selected image is unsupported", "Invalid image");
            }
        }      
    }
}
