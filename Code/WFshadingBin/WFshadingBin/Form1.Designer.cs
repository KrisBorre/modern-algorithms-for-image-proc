namespace WFshadingBin
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.buttonOpenImage = new System.Windows.Forms.Button();
            this.buttonShadingCorrection = new System.Windows.Forms.Button();
            this.buttonSaveDivision = new System.Windows.Forms.Button();
            this.numericUpDownWindow = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownLightness = new System.Windows.Forms.NumericUpDown();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.buttonSaveSubtraction = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(8, 195);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 500);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(810, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(450, 450);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(810, 460);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(450, 450);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // buttonOpenImage
            // 
            this.buttonOpenImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenImage.Location = new System.Drawing.Point(54, 8);
            this.buttonOpenImage.Name = "buttonOpenImage";
            this.buttonOpenImage.Size = new System.Drawing.Size(112, 37);
            this.buttonOpenImage.TabIndex = 3;
            this.buttonOpenImage.Text = "Open image";
            this.buttonOpenImage.UseVisualStyleBackColor = true;
            this.buttonOpenImage.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonShadingCorrection
            // 
            this.buttonShadingCorrection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShadingCorrection.Location = new System.Drawing.Point(391, 12);
            this.buttonShadingCorrection.Name = "buttonShadingCorrection";
            this.buttonShadingCorrection.Size = new System.Drawing.Size(170, 33);
            this.buttonShadingCorrection.TabIndex = 4;
            this.buttonShadingCorrection.Text = "Shading correction";
            this.buttonShadingCorrection.UseVisualStyleBackColor = true;
            this.buttonShadingCorrection.Visible = false;
            this.buttonShadingCorrection.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonSaveDivision
            // 
            this.buttonSaveDivision.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveDivision.Location = new System.Drawing.Point(638, 562);
            this.buttonSaveDivision.Name = "buttonSaveDivision";
            this.buttonSaveDivision.Size = new System.Drawing.Size(126, 31);
            this.buttonSaveDivision.TabIndex = 7;
            this.buttonSaveDivision.Text = "Save division";
            this.buttonSaveDivision.UseVisualStyleBackColor = true;
            this.buttonSaveDivision.Visible = false;
            this.buttonSaveDivision.Click += new System.EventHandler(this.button4_Click);
            // 
            // numericUpDownWindow
            // 
            this.numericUpDownWindow.Location = new System.Drawing.Point(366, 78);
            this.numericUpDownWindow.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownWindow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWindow.Name = "numericUpDownWindow";
            this.numericUpDownWindow.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownWindow.TabIndex = 8;
            this.numericUpDownWindow.Value = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.numericUpDownWindow.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(292, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Window in per mille of Width";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(547, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Lightness";
            this.label2.Visible = false;
            // 
            // numericUpDownLightness
            // 
            this.numericUpDownLightness.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownLightness.Location = new System.Drawing.Point(551, 77);
            this.numericUpDownLightness.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownLightness.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownLightness.Name = "numericUpDownLightness";
            this.numericUpDownLightness.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownLightness.TabIndex = 10;
            this.numericUpDownLightness.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownLightness.Visible = false;
            this.numericUpDownLightness.ValueChanged += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(22, 166);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(742, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 16;
            this.progressBar1.Visible = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(530, 263);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(256, 200);
            this.pictureBox4.TabIndex = 17;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox4_MouseClick);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(533, 619);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(256, 200);
            this.pictureBox5.TabIndex = 18;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox5_MouseClick);
            // 
            // buttonSaveSubtraction
            // 
            this.buttonSaveSubtraction.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveSubtraction.Location = new System.Drawing.Point(609, 194);
            this.buttonSaveSubtraction.Name = "buttonSaveSubtraction";
            this.buttonSaveSubtraction.Size = new System.Drawing.Size(155, 31);
            this.buttonSaveSubtraction.TabIndex = 19;
            this.buttonSaveSubtraction.Text = "Save subtraction";
            this.buttonSaveSubtraction.UseVisualStyleBackColor = true;
            this.buttonSaveSubtraction.Visible = false;
            this.buttonSaveSubtraction.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(515, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(289, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "If shading OK click threshold for \'SubIm\'";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(521, 596);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(282, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "If shading OK click threshold for \'DivIm\'";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(61, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "label5";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(556, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 20);
            this.label6.TabIndex = 23;
            this.label6.Text = "Click \'Shading correction\'";
            this.label6.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1272, 1003);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSaveSubtraction);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownLightness);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownWindow);
            this.Controls.Add(this.buttonSaveDivision);
            this.Controls.Add(this.buttonShadingCorrection);
            this.Controls.Add(this.buttonOpenImage);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.Button buttonOpenImage;
    private System.Windows.Forms.Button buttonShadingCorrection;
    private System.Windows.Forms.Button buttonSaveDivision;
    public System.Windows.Forms.NumericUpDown numericUpDownWindow;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    public System.Windows.Forms.NumericUpDown numericUpDownLightness;
    public System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.PictureBox pictureBox4;
    private System.Windows.Forms.PictureBox pictureBox5;
    private System.Windows.Forms.Button buttonSaveSubtraction;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
  }
}

