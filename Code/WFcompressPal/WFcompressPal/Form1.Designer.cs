namespace WFcompressPal
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
            this.buttonOpenImage = new System.Windows.Forms.Button();
            this.buttonImpulseNoise = new System.Windows.Forms.Button();
            this.buttonSegment = new System.Windows.Forms.Button();
            this.buttonDetectEdges = new System.Windows.Forms.Button();
            this.buttonEncode = new System.Windows.Forms.Button();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.numericUpDownDark = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownThreshold = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpenImage
            // 
            this.buttonOpenImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenImage.Location = new System.Drawing.Point(64, 72);
            this.buttonOpenImage.Name = "buttonOpenImage";
            this.buttonOpenImage.Size = new System.Drawing.Size(108, 42);
            this.buttonOpenImage.TabIndex = 0;
            this.buttonOpenImage.Text = "Open image";
            this.buttonOpenImage.UseVisualStyleBackColor = true;
            this.buttonOpenImage.Click += new System.EventHandler(this.buttonOpenImage_Click);
            // 
            // buttonImpulseNoise
            // 
            this.buttonImpulseNoise.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImpulseNoise.Location = new System.Drawing.Point(243, 29);
            this.buttonImpulseNoise.Name = "buttonImpulseNoise";
            this.buttonImpulseNoise.Size = new System.Drawing.Size(151, 42);
            this.buttonImpulseNoise.TabIndex = 1;
            this.buttonImpulseNoise.Text = "Impulse noise";
            this.buttonImpulseNoise.UseVisualStyleBackColor = true;
            this.buttonImpulseNoise.Visible = false;
            this.buttonImpulseNoise.Click += new System.EventHandler(this.buttonImpulseNoise_Click);
            // 
            // buttonSegment
            // 
            this.buttonSegment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSegment.Location = new System.Drawing.Point(443, 72);
            this.buttonSegment.Name = "buttonSegment";
            this.buttonSegment.Size = new System.Drawing.Size(108, 42);
            this.buttonSegment.TabIndex = 2;
            this.buttonSegment.Text = "Segment";
            this.buttonSegment.UseVisualStyleBackColor = true;
            this.buttonSegment.Visible = false;
            this.buttonSegment.Click += new System.EventHandler(this.buttonSegment_Click);
            // 
            // buttonDetectEdges
            // 
            this.buttonDetectEdges.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDetectEdges.Location = new System.Drawing.Point(589, 29);
            this.buttonDetectEdges.Name = "buttonDetectEdges";
            this.buttonDetectEdges.Size = new System.Drawing.Size(139, 42);
            this.buttonDetectEdges.TabIndex = 3;
            this.buttonDetectEdges.Text = "Detect edges";
            this.buttonDetectEdges.UseVisualStyleBackColor = true;
            this.buttonDetectEdges.Visible = false;
            this.buttonDetectEdges.Click += new System.EventHandler(this.buttonDetectEdges_Click);
            // 
            // buttonEncode
            // 
            this.buttonEncode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEncode.Location = new System.Drawing.Point(761, 29);
            this.buttonEncode.Name = "buttonEncode";
            this.buttonEncode.Size = new System.Drawing.Size(108, 42);
            this.buttonEncode.TabIndex = 4;
            this.buttonEncode.Text = "Encode";
            this.buttonEncode.UseVisualStyleBackColor = true;
            this.buttonEncode.Visible = false;
            this.buttonEncode.Click += new System.EventHandler(this.buttonEncode_Click);
            // 
            // buttonRestore
            // 
            this.buttonRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRestore.Location = new System.Drawing.Point(910, 29);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(108, 42);
            this.buttonRestore.TabIndex = 5;
            this.buttonRestore.Text = "Restore";
            this.buttonRestore.UseVisualStyleBackColor = true;
            this.buttonRestore.Visible = false;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(1066, 29);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(143, 42);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save code \\DAT";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Visible = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // numericUpDownDark
            // 
            this.numericUpDownDark.Location = new System.Drawing.Point(233, 110);
            this.numericUpDownDark.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDark.Name = "numericUpDownDark";
            this.numericUpDownDark.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownDark.TabIndex = 7;
            this.numericUpDownDark.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownDark.Visible = false;
            // 
            // numericUpDownLight
            // 
            this.numericUpDownLight.Location = new System.Drawing.Point(339, 110);
            this.numericUpDownLight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownLight.Name = "numericUpDownLight";
            this.numericUpDownLight.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownLight.TabIndex = 8;
            this.numericUpDownLight.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownLight.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(230, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Delete dark";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(334, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Delete light";
            this.label2.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(64, 170);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1119, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(26, 230);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(653, 230);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(600, 600);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(879, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 18);
            this.label3.TabIndex = 14;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(630, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Threshold";
            this.label4.Visible = false;
            // 
            // numericUpDownThreshold
            // 
            this.numericUpDownThreshold.Location = new System.Drawing.Point(637, 110);
            this.numericUpDownThreshold.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownThreshold.Name = "numericUpDownThreshold";
            this.numericUpDownThreshold.Size = new System.Drawing.Size(62, 20);
            this.numericUpDownThreshold.TabIndex = 16;
            this.numericUpDownThreshold.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownThreshold.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(311, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 18);
            this.label5.TabIndex = 17;
            this.label5.Text = "label5";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(61, 142);
            this.label6.MaximumSize = new System.Drawing.Size(600, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(466, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Opened image: aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(580, 205);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 18);
            this.label7.TabIndex = 19;
            this.label7.Text = "label7";
            this.label7.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1272, 973);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownThreshold);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownLight);
            this.Controls.Add(this.numericUpDownDark);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonRestore);
            this.Controls.Add(this.buttonEncode);
            this.Controls.Add(this.buttonDetectEdges);
            this.Controls.Add(this.buttonSegment);
            this.Controls.Add(this.buttonImpulseNoise);
            this.Controls.Add(this.buttonOpenImage);
            this.Name = "Form1";
            this.Text = "Image Compression";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonOpenImage;
    private System.Windows.Forms.Button buttonImpulseNoise;
    private System.Windows.Forms.Button buttonSegment;
    private System.Windows.Forms.Button buttonDetectEdges;
    private System.Windows.Forms.Button buttonEncode;
    private System.Windows.Forms.Button buttonRestore;
    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.NumericUpDown numericUpDownDark;
    private System.Windows.Forms.NumericUpDown numericUpDownLight;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    public System.Windows.Forms.ProgressBar progressBar1;
    public System.Windows.Forms.PictureBox pictureBox1;
    public System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown numericUpDownThreshold;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
  }
}

