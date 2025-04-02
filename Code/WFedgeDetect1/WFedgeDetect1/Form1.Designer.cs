namespace WFedgeDetect
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
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonDetectEdges = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.pictureBoxOriginalImage = new System.Windows.Forms.PictureBox();
            this.pictureBoxDetectedEdges = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.radioButton1Comb = new System.Windows.Forms.RadioButton();
            this.radioButton2Image = new System.Windows.Forms.RadioButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginalImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDetectedEdges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(176, 13);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(92, 42);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Open image";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpenImage_Click);
            // 
            // buttonDetectEdges
            // 
            this.buttonDetectEdges.Location = new System.Drawing.Point(637, 13);
            this.buttonDetectEdges.Name = "buttonDetectEdges";
            this.buttonDetectEdges.Size = new System.Drawing.Size(92, 42);
            this.buttonDetectEdges.TabIndex = 1;
            this.buttonDetectEdges.Text = "Detect edges";
            this.buttonDetectEdges.UseVisualStyleBackColor = true;
            this.buttonDetectEdges.Click += new System.EventHandler(this.buttonDetectEdges_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(405, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Edges threshold";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(408, 35);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(83, 20);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // pictureBoxOriginalImage
            // 
            this.pictureBoxOriginalImage.Location = new System.Drawing.Point(20, 150);
            this.pictureBoxOriginalImage.Name = "pictureBoxOriginalImage";
            this.pictureBoxOriginalImage.Size = new System.Drawing.Size(600, 600);
            this.pictureBoxOriginalImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOriginalImage.TabIndex = 4;
            this.pictureBoxOriginalImage.TabStop = false;
            // 
            // pictureBoxDetectedEdges
            // 
            this.pictureBoxDetectedEdges.Location = new System.Drawing.Point(660, 150);
            this.pictureBoxDetectedEdges.Name = "pictureBoxDetectedEdges";
            this.pictureBoxDetectedEdges.Size = new System.Drawing.Size(600, 600);
            this.pictureBoxDetectedEdges.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDetectedEdges.TabIndex = 5;
            this.pictureBoxDetectedEdges.TabStop = false;
            this.pictureBoxDetectedEdges.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxDetectedEdges_MouseClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(52, 100);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1160, 15);
            this.progressBar1.TabIndex = 6;
            // 
            // radioButton1Comb
            // 
            this.radioButton1Comb.AutoSize = true;
            this.radioButton1Comb.Location = new System.Drawing.Point(823, 8);
            this.radioButton1Comb.Name = "radioButton1Comb";
            this.radioButton1Comb.Size = new System.Drawing.Size(52, 17);
            this.radioButton1Comb.TabIndex = 7;
            this.radioButton1Comb.Text = "Comb";
            this.radioButton1Comb.UseVisualStyleBackColor = true;
            // 
            // radioButton2Image
            // 
            this.radioButton2Image.AutoSize = true;
            this.radioButton2Image.Location = new System.Drawing.Point(825, 38);
            this.radioButton2Image.Name = "radioButton2Image";
            this.radioButton2Image.Size = new System.Drawing.Size(54, 17);
            this.radioButton2Image.TabIndex = 8;
            this.radioButton2Image.Text = "Image";
            this.radioButton2Image.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(20, 706);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(1200, 279);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(408, 711);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Color transitions in \'EdgeIm\'";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(408, 828);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Lightness in \'SigmaIm\'";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(306, 123);
            this.label4.MaximumSize = new System.Drawing.Size(300, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "Original image";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(860, 123);
            this.label5.MaximumSize = new System.Drawing.Size(300, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 18);
            this.label5.TabIndex = 13;
            this.label5.Text = "Detected edges";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(49, 67);
            this.label6.MaximumSize = new System.Drawing.Size(500, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(263, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Opened image: aaaaaaaaaaaaaaaaaaaaaaaa";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(572, 123);
            this.label7.MaximumSize = new System.Drawing.Size(300, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 18);
            this.label7.TabIndex = 15;
            this.label7.Text = "Click \'Detect edges\'";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1272, 997);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.radioButton2Image);
            this.Controls.Add(this.radioButton1Comb);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBoxDetectedEdges);
            this.Controls.Add(this.pictureBoxOriginalImage);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDetectEdges);
            this.Controls.Add(this.buttonOpen);
            this.Name = "Form1";
            this.Text = "WFdetectEdges";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginalImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDetectedEdges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonOpen;
    private System.Windows.Forms.Button buttonDetectEdges;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.NumericUpDown numericUpDown1;
    public System.Windows.Forms.PictureBox pictureBoxOriginalImage;
    public System.Windows.Forms.PictureBox pictureBoxDetectedEdges;
    public System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.RadioButton radioButton1Comb;
    private System.Windows.Forms.RadioButton radioButton2Image;
    public System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
  }
}

