namespace BFontCore.Forms
{
	partial class MainForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.buttonFile = new System.Windows.Forms.Button();
			this.textFile = new System.Windows.Forms.TextBox();
			this.radioFile = new System.Windows.Forms.RadioButton();
			this.radioSystem = new System.Windows.Forms.RadioButton();
			this.listSystem = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.panelRender = new System.Windows.Forms.Panel();
			this.textRenderText = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.numericPadding = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.numericDistanceField = new System.Windows.Forms.NumericUpDown();
			this.comboPageSize = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numericSize = new System.Windows.Forms.NumericUpDown();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericPadding)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericDistanceField)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericSize)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.buttonFile);
			this.groupBox1.Controls.Add(this.textFile);
			this.groupBox1.Controls.Add(this.radioFile);
			this.groupBox1.Controls.Add(this.radioSystem);
			this.groupBox1.Controls.Add(this.listSystem);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(352, 159);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Font";
			// 
			// buttonFile
			// 
			this.buttonFile.Location = new System.Drawing.Point(314, 133);
			this.buttonFile.Name = "buttonFile";
			this.buttonFile.Size = new System.Drawing.Size(32, 20);
			this.buttonFile.TabIndex = 4;
			this.buttonFile.Text = "...";
			this.buttonFile.UseVisualStyleBackColor = true;
			this.buttonFile.Click += new System.EventHandler(this.buttonFile_Click);
			// 
			// textFile
			// 
			this.textFile.BackColor = System.Drawing.SystemColors.Window;
			this.textFile.Location = new System.Drawing.Point(71, 133);
			this.textFile.Name = "textFile";
			this.textFile.ReadOnly = true;
			this.textFile.Size = new System.Drawing.Size(237, 20);
			this.textFile.TabIndex = 3;
			// 
			// radioFile
			// 
			this.radioFile.AutoSize = true;
			this.radioFile.Location = new System.Drawing.Point(6, 134);
			this.radioFile.Name = "radioFile";
			this.radioFile.Size = new System.Drawing.Size(41, 17);
			this.radioFile.TabIndex = 2;
			this.radioFile.Text = "File";
			this.radioFile.UseVisualStyleBackColor = true;
			this.radioFile.CheckedChanged += new System.EventHandler(this.radioFile_CheckedChanged);
			// 
			// radioSystem
			// 
			this.radioSystem.AutoSize = true;
			this.radioSystem.Checked = true;
			this.radioSystem.Location = new System.Drawing.Point(6, 19);
			this.radioSystem.Name = "radioSystem";
			this.radioSystem.Size = new System.Drawing.Size(59, 17);
			this.radioSystem.TabIndex = 1;
			this.radioSystem.TabStop = true;
			this.radioSystem.Text = "System";
			this.radioSystem.UseVisualStyleBackColor = true;
			this.radioSystem.CheckedChanged += new System.EventHandler(this.radioSystem_CheckedChanged);
			// 
			// listSystem
			// 
			this.listSystem.FormattingEnabled = true;
			this.listSystem.Location = new System.Drawing.Point(71, 19);
			this.listSystem.Name = "listSystem";
			this.listSystem.Size = new System.Drawing.Size(275, 108);
			this.listSystem.TabIndex = 0;
			this.listSystem.SelectedIndexChanged += new System.EventHandler(this.listSystem_SelectedIndexChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.panelRender);
			this.groupBox2.Location = new System.Drawing.Point(12, 177);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(909, 594);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Rendering";
			// 
			// panelRender
			// 
			this.panelRender.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelRender.Location = new System.Drawing.Point(6, 19);
			this.panelRender.Name = "panelRender";
			this.panelRender.Size = new System.Drawing.Size(897, 569);
			this.panelRender.TabIndex = 0;
			this.panelRender.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRender_Paint);
			// 
			// textRenderText
			// 
			this.textRenderText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textRenderText.Location = new System.Drawing.Point(6, 19);
			this.textRenderText.Multiline = true;
			this.textRenderText.Name = "textRenderText";
			this.textRenderText.Size = new System.Drawing.Size(362, 132);
			this.textRenderText.TabIndex = 2;
			this.textRenderText.Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ\r\nabcdefghijklmnopqrstuvwxyz\r\n1234567890 \r\n\"!`?\'.,;:()[" +
    "]{}<>|/@\\^$-%+=#_&~*";
			this.textRenderText.TextChanged += new System.EventHandler(this.textRenderText_TextChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.textRenderText);
			this.groupBox3.Location = new System.Drawing.Point(370, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(374, 159);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Render Text";
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Controls.Add(this.numericPadding);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Controls.Add(this.numericDistanceField);
			this.groupBox4.Controls.Add(this.comboPageSize);
			this.groupBox4.Controls.Add(this.label2);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.numericSize);
			this.groupBox4.Location = new System.Drawing.Point(750, 12);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(171, 159);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Settings";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(46, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Padding";
			// 
			// numericPadding
			// 
			this.numericPadding.Location = new System.Drawing.Point(86, 72);
			this.numericPadding.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.numericPadding.Name = "numericPadding";
			this.numericPadding.Size = new System.Drawing.Size(79, 20);
			this.numericPadding.TabIndex = 8;
			this.numericPadding.ValueChanged += new System.EventHandler(this.numericPadding_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 101);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Distance Field";
			// 
			// numericDistanceField
			// 
			this.numericDistanceField.Location = new System.Drawing.Point(86, 99);
			this.numericDistanceField.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.numericDistanceField.Name = "numericDistanceField";
			this.numericDistanceField.Size = new System.Drawing.Size(79, 20);
			this.numericDistanceField.TabIndex = 5;
			this.numericDistanceField.ValueChanged += new System.EventHandler(this.numericDistanceField_ValueChanged);
			// 
			// comboPageSize
			// 
			this.comboPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPageSize.Items.AddRange(new object[] {
            "256",
            "512",
            "1024",
            "2048",
            "4096"});
			this.comboPageSize.Location = new System.Drawing.Point(86, 45);
			this.comboPageSize.Name = "comboPageSize";
			this.comboPageSize.Size = new System.Drawing.Size(79, 21);
			this.comboPageSize.TabIndex = 4;
			this.comboPageSize.SelectedIndexChanged += new System.EventHandler(this.comboPageSize_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Page Size";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(27, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Size";
			// 
			// numericSize
			// 
			this.numericSize.Location = new System.Drawing.Point(86, 19);
			this.numericSize.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
			this.numericSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericSize.Name = "numericSize";
			this.numericSize.Size = new System.Drawing.Size(79, 20);
			this.numericSize.TabIndex = 0;
			this.numericSize.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.numericSize.ValueChanged += new System.EventHandler(this.numericSize_ValueChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(933, 783);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.MinimumSize = new System.Drawing.Size(683, 440);
			this.Name = "MainForm";
			this.Text = "Font Generator";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericPadding)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericDistanceField)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericSize)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioFile;
		private System.Windows.Forms.RadioButton radioSystem;
		private System.Windows.Forms.ListBox listSystem;
		private System.Windows.Forms.TextBox textFile;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonFile;
		private System.Windows.Forms.Panel panelRender;
		private System.Windows.Forms.TextBox textRenderText;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericSize;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboPageSize;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericDistanceField;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numericPadding;
	}
}