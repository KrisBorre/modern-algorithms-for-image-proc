﻿namespace WFshadBinImpulse
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
    /// 
    //public progressBar1 = new System.Windows.Forms.ProgressBar();
    private void InitializeComponent()
    {
            this.buttonOpen = new System.Windows.Forms.Button();
            this.pictureBoxOriginalImage = new System.Windows.Forms.PictureBox();
            this.pictureBoxProcessesImage = new System.Windows.Forms.PictureBox();
            this.buttonShading = new System.Windows.Forms.Button();
            this.numericUpDownWindow = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownLightness = new System.Windows.Forms.NumericUpDown();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.buttonImpulseNoise = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownDeleteDark = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownDeleteLight = new System.Windows.Forms.NumericUpDown();
            this.buttonSave = new System.Windows.Forms.Button();
            this.pictureBoxThresholding = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginalImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProcessesImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLightness)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDeleteDark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDeleteLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThresholding)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpen.Location = new System.Drawing.Point(71, 37);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Open image";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpenImage_Click);
            // 
            // pictureBoxOriginalImage
            // 
            this.pictureBoxOriginalImage.Location = new System.Drawing.Point(16, 200);
            this.pictureBoxOriginalImage.Name = "pictureBoxOriginalImage";
            this.pictureBoxOriginalImage.Size = new System.Drawing.Size(470, 470);
            this.pictureBoxOriginalImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOriginalImage.TabIndex = 1;
            this.pictureBoxOriginalImage.TabStop = false;
            this.pictureBoxOriginalImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxOriginalImage_MouseClick);
            // 
            // pictureBoxProcessesImage
            // 
            this.pictureBoxProcessesImage.Location = new System.Drawing.Point(794, 200);
            this.pictureBoxProcessesImage.Name = "pictureBoxProcessesImage";
            this.pictureBoxProcessesImage.Size = new System.Drawing.Size(470, 470);
            this.pictureBoxProcessesImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxProcessesImage.TabIndex = 2;
            this.pictureBoxProcessesImage.TabStop = false;
            // 
            // buttonShading
            // 
            this.buttonShading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShading.Location = new System.Drawing.Point(275, 37);
            this.buttonShading.Name = "buttonShading";
            this.buttonShading.Size = new System.Drawing.Size(146, 23);
            this.buttonShading.TabIndex = 3;
            this.buttonShading.Text = "Shading ";
            this.buttonShading.UseVisualStyleBackColor = true;
            this.buttonShading.Visible = false;
            this.buttonShading.Click += new System.EventHandler(this.buttonShading_Click);
            // 
            // numericUpDownWindow
            // 
            this.numericUpDownWindow.Location = new System.Drawing.Point(268, 89);
            this.numericUpDownWindow.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownWindow.Name = "numericUpDownWindow";
            this.numericUpDownWindow.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownWindow.TabIndex = 4;
            this.numericUpDownWindow.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownWindow.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Window in per mille of Width";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(379, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Lightness";
            this.label2.Visible = false;
            // 
            // numericUpDownLightness
            // 
            this.numericUpDownLightness.Location = new System.Drawing.Point(379, 89);
            this.numericUpDownLightness.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownLightness.Name = "numericUpDownLightness";
            this.numericUpDownLightness.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownLightness.TabIndex = 6;
            this.numericUpDownLightness.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownLightness.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 162);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1212, 15);
            this.progressBar1.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(574, 253);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 124);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Visible = false;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(6, 93);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(99, 17);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "no drawing Sub";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(5, 70);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(96, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "no drawing Div";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(4, 49);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(130, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "drawing with light lines";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 27);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(132, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "drawing with dark lines";
            this.radioButton1.UseVisualStyleBackColor = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // buttonImpulseNoise
            // 
            this.buttonImpulseNoise.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImpulseNoise.Location = new System.Drawing.Point(577, 59);
            this.buttonImpulseNoise.Name = "buttonImpulseNoise";
            this.buttonImpulseNoise.Size = new System.Drawing.Size(106, 29);
            this.buttonImpulseNoise.TabIndex = 12;
            this.buttonImpulseNoise.Text = "Impulse noise";
            this.buttonImpulseNoise.UseVisualStyleBackColor = true;
            this.buttonImpulseNoise.Visible = false;
            this.buttonImpulseNoise.Click += new System.EventHandler(this.buttonImpulseNoise_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(730, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Delete dark";
            this.label4.Visible = false;
            // 
            // numericUpDownDeleteDark
            // 
            this.numericUpDownDeleteDark.Location = new System.Drawing.Point(734, 68);
            this.numericUpDownDeleteDark.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDeleteDark.Name = "numericUpDownDeleteDark";
            this.numericUpDownDeleteDark.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownDeleteDark.TabIndex = 13;
            this.numericUpDownDeleteDark.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDeleteDark.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(814, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Delete light";
            this.label5.Visible = false;
            // 
            // numericUpDownDeleteLight
            // 
            this.numericUpDownDeleteLight.Location = new System.Drawing.Point(818, 68);
            this.numericUpDownDeleteLight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDeleteLight.Name = "numericUpDownDeleteLight";
            this.numericUpDownDeleteLight.Size = new System.Drawing.Size(55, 20);
            this.numericUpDownDeleteLight.TabIndex = 15;
            this.numericUpDownDeleteLight.Visible = false;
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(932, 59);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(71, 29);
            this.buttonSave.TabIndex = 17;
            this.buttonSave.Text = "Save result";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Visible = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // pictureBox3
            // 
            this.pictureBoxThresholding.Location = new System.Drawing.Point(512, 500);
            this.pictureBoxThresholding.Name = "pictureBox3";
            this.pictureBoxThresholding.Size = new System.Drawing.Size(260, 200);
            this.pictureBoxThresholding.TabIndex = 18;
            this.pictureBoxThresholding.TabStop = false;
            this.pictureBoxThresholding.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxThresholding_MouseClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(546, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(188, 20);
            this.label6.TabIndex = 20;
            this.label6.Text = "Click one of radio buttons";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(485, 463);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(317, 16);
            this.label7.TabIndex = 21;
            this.label7.Text = "If satisfied make rectangles and click \'Impulse noise\'";
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(503, 428);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(254, 18);
            this.label8.TabIndex = 22;
            this.label8.Text = "Chose optimal threshold in histogram";
            this.label8.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(568, 394);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 20);
            this.label9.TabIndex = 23;
            this.label9.Text = "Click \'Shading\'";
            this.label9.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1013, 184);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 16);
            this.label10.TabIndex = 24;
            this.label10.Text = "Processed image";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(222, 184);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 16);
            this.label11.TabIndex = 25;
            this.label11.Text = "Original image";
            this.label11.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 126);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Opened image:";
            this.label12.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1272, 710);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBoxThresholding);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownDeleteLight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownDeleteDark);
            this.Controls.Add(this.buttonImpulseNoise);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownLightness);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownWindow);
            this.Controls.Add(this.buttonShading);
            this.Controls.Add(this.pictureBoxProcessesImage);
            this.Controls.Add(this.pictureBoxOriginalImage);
            this.Controls.Add(this.buttonOpen);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginalImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProcessesImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLightness)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDeleteDark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDeleteLight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThresholding)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonOpen;
    private System.Windows.Forms.PictureBox pictureBoxOriginalImage;
    private System.Windows.Forms.PictureBox pictureBoxProcessesImage;
    private System.Windows.Forms.Button buttonShading;
    public System.Windows.Forms.NumericUpDown numericUpDownWindow;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    //public System.Windows.Forms.ProgressBar progressBar1;

    public System.Windows.Forms.NumericUpDown numericUpDownLightness;
    public System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton radioButton3;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.Button buttonImpulseNoise;
    private System.Windows.Forms.Label label4;
    public System.Windows.Forms.NumericUpDown numericUpDownDeleteDark;
    private System.Windows.Forms.Label label5;
    public System.Windows.Forms.NumericUpDown numericUpDownDeleteLight;
    private System.Windows.Forms.Button buttonSave;
    public System.Windows.Forms.PictureBox pictureBoxThresholding;
    private System.Windows.Forms.RadioButton radioButton4;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
  }
}

