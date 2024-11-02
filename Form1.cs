using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Svg;
using System.Text;
using System.Linq;
using System.Drawing.Text;
using LabelGenerator.Properties;
using System.Drawing.Drawing2D;


namespace SVG2PNG
{
    public partial class MainForm : Form
    {
        private string selectedOutputPath = "";
        private CancellationTokenSource cancellationTokenSource;
        private bool isProcessing = false;
        private float calculatedFontSize;
        private InstalledFontCollection installedFontCollection;

        public MainForm()
        {
            InitializeComponent();
            LoadFontsList();
            LoadSettings();
            SetupEventHandlers();
            SetupNumericValidation();
        }

        private void LoadSettings()
        {
            // Load output path
            if (!string.IsNullOrEmpty(Settings.Default.LastOutputPath))
            {
                selectedOutputPath = Settings.Default.LastOutputPath;
                txtOutputPath.Text = selectedOutputPath;
            }

            // Load resolution settings
            txtWidth.Text = Settings.Default.LastWidth;
            txtHeight.Text = Settings.Default.LastHeight;
            txtInitialFontSize.Text = Settings.Default.LastFontSize;

            // Load font
            if (!string.IsNullOrEmpty(Settings.Default.LastFont))
            {
                int fontIndex = cmbFonts.Items.IndexOf(Settings.Default.LastFont);
                if (fontIndex >= 0)
                {
                    cmbFonts.SelectedIndex = fontIndex;
                }
            }

            // Load color settings
            btnColorStart.BackColor = Settings.Default.LastStartColor;
            btnColorEnd.BackColor = Settings.Default.LastEndColor;
            chkUseGradient.Checked = Settings.Default.LastUseGradient;
            btnColorEnd.Enabled = chkUseGradient.Checked;
        }

        private void SaveSettings()
        {
            // Save output path
            Settings.Default.LastOutputPath = selectedOutputPath;

            // Save resolution settings
            Settings.Default.LastWidth = txtWidth.Text;
            Settings.Default.LastHeight = txtHeight.Text;
            Settings.Default.LastFontSize = txtInitialFontSize.Text;

            // Save font
            if (cmbFonts.SelectedItem != null)
            {
                Settings.Default.LastFont = cmbFonts.SelectedItem.ToString();
            }

            // Save color settings
            Settings.Default.LastStartColor = btnColorStart.BackColor;
            Settings.Default.LastEndColor = btnColorEnd.BackColor;
            Settings.Default.LastUseGradient = chkUseGradient.Checked;

            // Save all settings
            Settings.Default.Save();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isProcessing)
            {
                cancellationTokenSource?.Cancel();
                while (isProcessing)
                {
                    Application.DoEvents();
                    Thread.Sleep(100);
                }
            }
            SaveSettings();  // Add this line
        }

        private void SetupNumericValidation()
        {
            txtWidth.KeyPress += NumericOnly_KeyPress;
            txtHeight.KeyPress += NumericOnly_KeyPress;
            txtInitialFontSize.KeyPress += NumericOnly_KeyPress;
        }

        private void BtnSelectOutput_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // Set initial directory if one was previously selected
                if (!string.IsNullOrEmpty(selectedOutputPath) && Directory.Exists(selectedOutputPath))
                {
                    folderDialog.SelectedPath = selectedOutputPath;
                }

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Create directory if it doesn't exist
                        if (!Directory.Exists(folderDialog.SelectedPath))
                        {
                            Directory.CreateDirectory(folderDialog.SelectedPath);
                        }

                        // Test write permissions
                        string testFile = Path.Combine(folderDialog.SelectedPath, "test.tmp");
                        File.WriteAllText(testFile, "test");
                        File.Delete(testFile);

                        selectedOutputPath = folderDialog.SelectedPath;
                        txtOutputPath.Text = selectedOutputPath;
                        SaveSettings();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Cannot use the selected folder: {ex.Message}\nPlease choose a different folder.",
                            "Access Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
            btnCancel.Enabled = false;
            lblStatus.Text = "Cancelling...";
        }

        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private string SanitizeFileName(string fileName)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            return string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
        }

        private async Task ConvertSvgToPng(string svgPath, string pngPath)
        {
            await Task.Run(() =>
            {
                try
                {
                    // If the file exists, delete it first
                    if (File.Exists(pngPath))
                    {
                        File.Delete(pngPath);
                    }

                    var svgDocument = SvgDocument.Open(svgPath);
                    using (var bitmap = svgDocument.Draw())
                    {
                        bitmap.Save(pngPath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error converting SVG to PNG: {ex.Message}");
                }
            });
        }

        private void LoadFontsList()
        {
            // Store current selection if any
            string currentFont = cmbFonts.SelectedItem?.ToString() ?? "Arial";

            // Initialize the font collection
            installedFontCollection = new InstalledFontCollection();

            // Clear and reload the list
            cmbFonts.Items.Clear();

            foreach (FontFamily family in installedFontCollection.Families)
            {
                cmbFonts.Items.Add(family.Name);
            }

            // Set default to Arial or first available font
            int arialIndex = cmbFonts.Items.IndexOf("Arial");
            if (arialIndex >= 0)
            {
                cmbFonts.SelectedIndex = arialIndex;
            }
            else if (cmbFonts.Items.Count > 0)
            {
                cmbFonts.SelectedIndex = 0;
            }

            // Try to restore previous selection
            int previousIndex = cmbFonts.Items.IndexOf(currentFont);
            if (previousIndex >= 0)
            {
                cmbFonts.SelectedIndex = previousIndex;
            }
        }

        private float CalculateOptimalFontSize(string[] lines, int width, int height, float initialFontSize)
        {
            if (lines.Length == 0) return initialFontSize;

            string longestLine = lines.OrderByDescending(s => s.Length).First();
            float fontSize = initialFontSize;
            string selectedFontName = cmbFonts.SelectedItem?.ToString() ?? "Arial";

            using (var bmp = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(bmp))
            {
                // Start with initial font size and scale down if needed
                while (fontSize > 1)
                {
                    using (var font = new Font(selectedFontName, fontSize, FontStyle.Bold))
                    {
                        SizeF textSize = g.MeasureString(longestLine, font);

                        // Add padding (10% of width/height)
                        float paddedWidth = width * 0.9f;
                        float paddedHeight = height * 0.9f;

                        if (textSize.Width <= paddedWidth && textSize.Height <= paddedHeight)
                        {
                            break;
                        }
                    }
                    fontSize -= 0.5f;
                }
            }

            return fontSize;
        }

        private async void BtnConvert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedOutputPath))
            {
                MessageBox.Show("Please select an output folder first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verify the directory exists
            try
            {
                if (!Directory.Exists(selectedOutputPath))
                {
                    var result = MessageBox.Show(
                        "The selected output directory doesn't exist. Would you like to create it?",
                        "Directory Not Found",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Directory.CreateDirectory(selectedOutputPath);
                    }
                    else
                    {
                        return;
                    }
                }

                // Test write permissions
                string testFile = Path.Combine(selectedOutputPath, "test.tmp");
                try
                {
                    File.WriteAllText(testFile, "test");
                    File.Delete(testFile);
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "Cannot write to the selected output folder. Please check your permissions or choose a different folder.",
                        "Access Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error accessing output folder: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Validate numeric inputs
            if (!int.TryParse(txtWidth.Text, out int width) ||
                !int.TryParse(txtHeight.Text, out int height) ||
                !float.TryParse(txtInitialFontSize.Text, out float initialFontSize))
            {
                MessageBox.Show("Please enter valid numbers for width, height, and font size.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate text input
            var lines = txtInput.Lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();
            if (lines.Count == 0)
            {
                MessageBox.Show("Please enter some text to convert.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Calculate optimal font size before processing
            calculatedFontSize = CalculateOptimalFontSize(lines.ToArray(), width, height, initialFontSize);
            lblCalculatedFontSize.Text = $"Calculated font size: {calculatedFontSize:F1}";

            // Setup for processing
            btnConvert.Enabled = false;
            btnCancel.Enabled = true;
            isProcessing = true;
            cancellationTokenSource = new CancellationTokenSource();
            progressBar.Maximum = lines.Count;
            progressBar.Value = 0;

            try
            {
                await ProcessLines(lines, width, height, cancellationTokenSource.Token);
                lblStatus.Text = "All items processed successfully!";
                MessageBox.Show("Conversion completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                lblStatus.Text = "Operation cancelled by user.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during conversion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error during conversion. See error message.";
            }
            finally
            {
                btnConvert.Enabled = true;
                btnCancel.Enabled = false;
                isProcessing = false;
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
        }

        private string GenerateFileName(string text, int index)
        {
            // Remove all non-alphanumeric characters and spaces, keeping only letters and numbers
            string cleanName = new string(text.Where(c => char.IsLetterOrDigit(c)).ToArray());

            // Get the prefix (or empty string if none)
            string prefix = txtPrefix.Text.Trim();
            if (!string.IsNullOrEmpty(prefix) && !prefix.EndsWith("_"))
            {
                prefix += "_";
            }

            // Generate the number suffix only if padding is greater than 0
            string numberSuffix = "";
            int paddingLength = (int)numPadding.Value;
            if (paddingLength > 0)
            {
                numberSuffix = $"_{(index + 1).ToString().PadLeft(paddingLength, '0')}";
            }

            // Combine all parts
            return $"{prefix}{cleanName}{numberSuffix}.png";
        }

        private async Task ProcessLines(List<string> lines, int width, int height, CancellationToken cancellationToken)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string text = lines[i].Trim();
                lblCurrentItem.Text = $"Processing: {text}";

                try
                {
                    string outputFileName = Path.Combine(selectedOutputPath, GenerateFileName(text, i));
                    await CreatePngDirectly(text, outputFileName, width, height);

                    progressBar.Value = i + 1;
                    lblStatus.Text = $"Processed {i + 1} of {lines.Count}";
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error processing '{text}': {ex.Message}");
                }
            }
        }
        private async Task ProcessLines(List<string> lines, int width, int height, CancellationToken cancellationToken)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string text = lines[i].Trim();
                lblCurrentItem.Text = $"Processing: {text}";

                try
                {
                    string svgContent = GenerateSvgWithText(text, width, height);
                    string tempSvgPath = Path.Combine(Path.GetTempPath(), $"temp_{i}.svg");
                    File.WriteAllText(tempSvgPath, svgContent);

                    // Use the new file naming method
                    string outputFileName = Path.Combine(selectedOutputPath, GenerateFileName(text, i));
                    await ConvertSvgToPng(tempSvgPath, outputFileName);

                    if (File.Exists(tempSvgPath))
                    {
                        File.Delete(tempSvgPath);
                    }

                    progressBar.Value = i + 1;
                    lblStatus.Text = $"Processed {i + 1} of {lines.Count}";
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error processing '{text}': {ex.Message}");
                }
            }
        }

        // Add these methods to your MainForm.cs class:

        private void SetupEventHandlers()
        {
            // Existing handlers
            btnSelectOutput.Click += BtnSelectOutput_Click;
            btnConvert.Click += BtnConvert_Click;
            btnCancel.Click += BtnCancel_Click;
            this.FormClosing += MainForm_FormClosing;

            txtWidth.TextChanged += ResolutionSettings_Changed;
            txtHeight.TextChanged += ResolutionSettings_Changed;
            txtInitialFontSize.TextChanged += ResolutionSettings_Changed;
            cmbFonts.SelectedIndexChanged += ResolutionSettings_Changed;

            // New color handlers
            btnColorStart.Click += BtnColorStart_Click;
            btnColorEnd.Click += BtnColorEnd_Click;
            chkUseGradient.CheckedChanged += ChkUseGradient_CheckedChanged;
        }

        private void BtnColorStart_Click(object sender, EventArgs e)
        {
            colorDialog.Color = btnColorStart.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnColorStart.BackColor = colorDialog.Color;
                SaveSettings();
            }
        }

        private void BtnColorEnd_Click(object sender, EventArgs e)
        {
            colorDialog.Color = btnColorEnd.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnColorEnd.BackColor = colorDialog.Color;
                SaveSettings();
            }
        }

        private void ChkUseGradient_CheckedChanged(object sender, EventArgs e)
        {
            btnColorEnd.Enabled = chkUseGradient.Checked;
            SaveSettings();
        }

        private string GenerateSvgWithText(string text, int width, int height)
        {
            float x = width / 2;
            float y = height / 2;
            string selectedFontName = cmbFonts.SelectedItem?.ToString() ?? "Arial";

            string gradientDef = "";
            string fillAttribute = "";

            if (chkUseGradient.Checked)
            {
                string startColor = ColorToHex(btnColorStart.BackColor);
                string endColor = ColorToHex(btnColorEnd.BackColor);
                gradientDef = $@"<defs>
                    <linearGradient id='textGradient' x1='0%' y1='0%' x2='0%' y2='100%'>
                        <stop offset='0%' style='stop-color:{startColor}'/>
                        <stop offset='100%' style='stop-color:{endColor}'/>
                    </linearGradient>
                </defs>";
                fillAttribute = "fill='url(#textGradient)'";
            }
            else
            {
                string singleColor = ColorToHex(btnColorStart.BackColor);
                fillAttribute = $"fill='{singleColor}'";
            }

            // Clean the text to prevent XML issues
            text = System.Security.SecurityElement.Escape(text);

            return $@"<?xml version='1.0' encoding='UTF-8'?>
            <svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 {width} {height}'>
            {gradientDef}
                <text 
                    x='{x}' 
                    y='{y}' 
                    {fillAttribute}
                    font-family='{selectedFontName}, Arial, sans-serif'
                    font-size='{calculatedFontSize}'
                    font-weight='bold'
                    text-anchor='middle'
                    dominant-baseline='middle'>{text}</text>
            </svg>".Trim();
        }

        private async Task CreatePngDirectly(string text, string outputPath, int width, int height)
        {
            await Task.Run(() =>
            {
                try
                {
                    using (var bitmap = new Bitmap(width, height))
                    {
                        using (var graphics = Graphics.FromImage(bitmap))
                        {
                            // Set up high quality rendering
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                            // Clear background (optional - make transparent)
                            graphics.Clear(Color.Transparent);

                            // Create brush for text
                            Brush textBrush;
                            if (chkUseGradient.Checked)
                            {
                                textBrush = new LinearGradientBrush(
                                    new Point(0, 0),
                                    new Point(0, height),
                                    btnColorStart.BackColor,
                                    btnColorEnd.BackColor);
                            }
                            else
                            {
                                textBrush = new SolidBrush(btnColorStart.BackColor);
                            }

                            // Use selected font
                            using (var font = new Font(cmbFonts.SelectedItem?.ToString() ?? "Arial", calculatedFontSize, FontStyle.Bold))
                            {
                                // Measure string to center it
                                var textSize = graphics.MeasureString(text, font);
                                float x = (width - textSize.Width) / 2;
                                float y = (height - textSize.Height) / 2;

                                // Draw the text
                                graphics.DrawString(text, font, textBrush, x, y);
                            }

                            // Clean up brush
                            textBrush.Dispose();

                            // Save with transparent background
                            bitmap.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error creating PNG: {ex.Message}");
                }
            });
        }

        private string ColorToHex(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        private void ResolutionSettings_Changed(object sender, EventArgs e)
        {
            SaveSettings();
        }

    }
}