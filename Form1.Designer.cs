namespace SVG2PNG
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpNaming;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label lblPadding;
        private System.Windows.Forms.NumericUpDown numPadding;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.ComboBox cmbFonts;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (installedFontCollection != null)
                {
                    installedFontCollection.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            grpInput = new GroupBox();
            txtInput = new TextBox();
            grpOutput = new GroupBox();
            btnSelectOutput = new Button();
            txtOutputPath = new TextBox();
            grpResolution = new GroupBox();
            lblWidth = new Label();
            txtWidth = new TextBox();
            lblHeight = new Label();
            txtHeight = new TextBox();
            lblInitialFontSize = new Label();
            txtInitialFontSize = new TextBox();
            lblCalculatedFontSize = new Label();
            lblFont = new Label();
            cmbFonts = new ComboBox();
            btnConvert = new Button();
            btnCancel = new Button();
            progressBar = new ProgressBar();
            lblStatus = new Label();
            lblCurrentItem = new Label();
            grpColor = new GroupBox();
            chkUseGradient = new CheckBox();
            btnColorStart = new Button();
            btnColorEnd = new Button();
            colorDialog = new ColorDialog();
            grpNaming = new GroupBox();
            lblPrefix = new Label();
            txtPrefix = new TextBox();
            lblPadding = new Label();
            numPadding = new NumericUpDown();
            grpInput.SuspendLayout();
            grpOutput.SuspendLayout();
            grpResolution.SuspendLayout();
            grpColor.SuspendLayout();
            grpNaming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPadding).BeginInit();
            SuspendLayout();
            // 
            // grpInput
            // 
            grpInput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpInput.Controls.Add(txtInput);
            grpInput.Location = new Point(12, 12);
            grpInput.Name = "grpInput";
            grpInput.Size = new Size(560, 200);
            grpInput.TabIndex = 1;
            grpInput.TabStop = false;
            grpInput.Text = "Input Text (one item per line)";
            // 
            // txtInput
            // 
            txtInput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtInput.Location = new Point(6, 22);
            txtInput.Multiline = true;
            txtInput.Name = "txtInput";
            txtInput.ScrollBars = ScrollBars.Vertical;
            txtInput.Size = new Size(548, 172);
            txtInput.TabIndex = 0;
            // 
            // grpOutput
            // 
            grpOutput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpOutput.Controls.Add(btnSelectOutput);
            grpOutput.Controls.Add(txtOutputPath);
            grpOutput.Location = new Point(12, 506);
            grpOutput.Name = "grpOutput";
            grpOutput.Size = new Size(560, 90);
            grpOutput.TabIndex = 4;
            grpOutput.TabStop = false;
            grpOutput.Text = "Output Folder";
            // 
            // btnSelectOutput
            // 
            btnSelectOutput.Location = new Point(6, 22);
            btnSelectOutput.Name = "btnSelectOutput";
            btnSelectOutput.Size = new Size(120, 30);
            btnSelectOutput.TabIndex = 0;
            btnSelectOutput.Text = "Select Output Folder";
            btnSelectOutput.UseVisualStyleBackColor = true;
            // 
            // txtOutputPath
            // 
            txtOutputPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtOutputPath.Location = new Point(132, 27);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.ReadOnly = true;
            txtOutputPath.Size = new Size(422, 23);
            txtOutputPath.TabIndex = 1;
            // 
            // grpResolution
            // 
            grpResolution.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpResolution.Controls.Add(lblWidth);
            grpResolution.Controls.Add(txtWidth);
            grpResolution.Controls.Add(lblHeight);
            grpResolution.Controls.Add(txtHeight);
            grpResolution.Controls.Add(lblInitialFontSize);
            grpResolution.Controls.Add(txtInitialFontSize);
            grpResolution.Controls.Add(lblCalculatedFontSize);
            grpResolution.Location = new Point(12, 218);
            grpResolution.Name = "grpResolution";
            grpResolution.Size = new Size(560, 90);
            grpResolution.TabIndex = 2;
            grpResolution.TabStop = false;
            grpResolution.Text = "Resolution Settings";
            // 
            // lblWidth
            // 
            lblWidth.Location = new Point(6, 25);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(45, 23);
            lblWidth.TabIndex = 0;
            lblWidth.Text = "Width:";
            // 
            // txtWidth
            // 
            txtWidth.Location = new Point(57, 22);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(60, 23);
            txtWidth.TabIndex = 1;
            txtWidth.Text = "512";
            // 
            // lblHeight
            // 
            lblHeight.Location = new Point(123, 25);
            lblHeight.Name = "lblHeight";
            lblHeight.Size = new Size(50, 23);
            lblHeight.TabIndex = 2;
            lblHeight.Text = "Height:";
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(179, 22);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(60, 23);
            txtHeight.TabIndex = 3;
            txtHeight.Text = "128";
            // 
            // lblInitialFontSize
            // 
            lblInitialFontSize.Location = new Point(245, 25);
            lblInitialFontSize.Name = "lblInitialFontSize";
            lblInitialFontSize.Size = new Size(100, 23);
            lblInitialFontSize.TabIndex = 4;
            lblInitialFontSize.Text = "Initial Font Size:";
            // 
            // txtInitialFontSize
            // 
            txtInitialFontSize.Location = new Point(351, 22);
            txtInitialFontSize.Name = "txtInitialFontSize";
            txtInitialFontSize.Size = new Size(60, 23);
            txtInitialFontSize.TabIndex = 5;
            txtInitialFontSize.Text = "48";
            // 
            // lblCalculatedFontSize
            // 
            lblCalculatedFontSize.Location = new Point(6, 55);
            lblCalculatedFontSize.Name = "lblCalculatedFontSize";
            lblCalculatedFontSize.Size = new Size(548, 23);
            lblCalculatedFontSize.TabIndex = 6;
            lblCalculatedFontSize.Text = "Calculated font size will be shown here";
            // 
            // lblFont
            // 
            lblFont.Location = new Point(351, 22);
            lblFont.Name = "lblFont";
            lblFont.Size = new Size(40, 23);
            lblFont.TabIndex = 7;
            lblFont.Text = "Font:";
            // 
            // cmbFonts
            // 
            cmbFonts.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFonts.Location = new Point(397, 22);
            cmbFonts.Name = "cmbFonts";
            cmbFonts.Size = new Size(157, 23);
            cmbFonts.TabIndex = 8;
            // 
            // btnConvert
            // 
            btnConvert.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnConvert.Location = new Point(12, 602);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(460, 40);
            btnConvert.TabIndex = 5;
            btnConvert.Text = "Convert All";
            btnConvert.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Enabled = false;
            btnCancel.Location = new Point(478, 602);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 40);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(12, 694);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(560, 23);
            progressBar.TabIndex = 9;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.Location = new Point(12, 668);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(560, 23);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Ready";
            // 
            // lblCurrentItem
            // 
            lblCurrentItem.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblCurrentItem.Location = new Point(12, 645);
            lblCurrentItem.Name = "lblCurrentItem";
            lblCurrentItem.Size = new Size(560, 23);
            lblCurrentItem.TabIndex = 7;
            lblCurrentItem.Text = "Ready";
            // 
            // grpColor
            // 
            grpColor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpColor.Controls.Add(chkUseGradient);
            grpColor.Controls.Add(btnColorStart);
            grpColor.Controls.Add(btnColorEnd);
            grpColor.Controls.Add(cmbFonts);
            grpColor.Controls.Add(lblFont);
            grpColor.Location = new Point(12, 314);
            grpColor.Name = "grpColor";
            grpColor.Size = new Size(560, 90);
            grpColor.TabIndex = 3;
            grpColor.TabStop = false;
            grpColor.Text = "Text Color Settings";
            // 
            // chkUseGradient
            // 
            chkUseGradient.Location = new Point(6, 22);
            chkUseGradient.Name = "chkUseGradient";
            chkUseGradient.Size = new Size(100, 23);
            chkUseGradient.TabIndex = 0;
            chkUseGradient.Text = "Use Gradient";
            // 
            // btnColorStart
            // 
            btnColorStart.BackColor = Color.White;
            btnColorStart.Location = new Point(112, 22);
            btnColorStart.Name = "btnColorStart";
            btnColorStart.Size = new Size(100, 23);
            btnColorStart.TabIndex = 1;
            btnColorStart.Text = "Start Color";
            btnColorStart.UseVisualStyleBackColor = false;
            // 
            // btnColorEnd
            // 
            btnColorEnd.BackColor = Color.DimGray;
            btnColorEnd.Location = new Point(218, 22);
            btnColorEnd.Name = "btnColorEnd";
            btnColorEnd.Size = new Size(100, 23);
            btnColorEnd.TabIndex = 2;
            btnColorEnd.Text = "End Color";
            btnColorEnd.UseVisualStyleBackColor = false;
            // 
            // grpNaming
            // 
            grpNaming.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpNaming.Controls.Add(lblPrefix);
            grpNaming.Controls.Add(txtPrefix);
            grpNaming.Controls.Add(lblPadding);
            grpNaming.Controls.Add(numPadding);
            grpNaming.Location = new Point(12, 410);
            grpNaming.Name = "grpNaming";
            grpNaming.Size = new Size(560, 90);
            grpNaming.TabIndex = 0;
            grpNaming.TabStop = false;
            grpNaming.Text = "File Naming Settings";
            // 
            // lblPrefix
            // 
            lblPrefix.Location = new Point(6, 25);
            lblPrefix.Name = "lblPrefix";
            lblPrefix.Size = new Size(50, 23);
            lblPrefix.TabIndex = 0;
            lblPrefix.Text = "Prefix:";
            // 
            // txtPrefix
            // 
            txtPrefix.Location = new Point(62, 22);
            txtPrefix.Name = "txtPrefix";
            txtPrefix.Size = new Size(100, 23);
            txtPrefix.TabIndex = 1;
            txtPrefix.Text = "LoadoutScreen_";
            // 
            // lblPadding
            // 
            lblPadding.Location = new Point(168, 25);
            lblPadding.Name = "lblPadding";
            lblPadding.Size = new Size(111, 23);
            lblPadding.TabIndex = 2;
            lblPadding.Text = "Number Padding:";
            // 
            // numPadding
            // 
            numPadding.Location = new Point(285, 22);
            numPadding.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numPadding.Name = "numPadding";
            numPadding.Size = new Size(60, 23);
            numPadding.TabIndex = 3;
            numPadding.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // MainForm
            // 
            ClientSize = new Size(584, 661);
            Controls.Add(grpNaming);
            Controls.Add(grpInput);
            Controls.Add(grpResolution);
            Controls.Add(grpColor);
            Controls.Add(grpOutput);
            Controls.Add(btnConvert);
            Controls.Add(btnCancel);
            Controls.Add(lblCurrentItem);
            Controls.Add(lblStatus);
            Controls.Add(progressBar);
            MinimumSize = new Size(600, 700);
            Name = "MainForm";
            Text = "Text to SVG to PNG Converter";
            FormClosing += MainForm_FormClosing;
            grpInput.ResumeLayout(false);
            grpInput.PerformLayout();
            grpOutput.ResumeLayout(false);
            grpOutput.PerformLayout();
            grpResolution.ResumeLayout(false);
            grpResolution.PerformLayout();
            grpColor.ResumeLayout(false);
            grpNaming.ResumeLayout(false);
            grpNaming.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPadding).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.GroupBox grpOutput;
        private System.Windows.Forms.Button btnSelectOutput;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.GroupBox grpResolution;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblInitialFontSize;
        private System.Windows.Forms.TextBox txtInitialFontSize;
        private System.Windows.Forms.Label lblCalculatedFontSize;
        private System.Windows.Forms.GroupBox grpColor;
        private System.Windows.Forms.Button btnColorStart;
        private System.Windows.Forms.Button btnColorEnd;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.CheckBox chkUseGradient;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCurrentItem;
    }
}