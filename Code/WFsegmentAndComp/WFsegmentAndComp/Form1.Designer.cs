namespace WFsegmentAndComp
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
            this.buttonBreadthFirst = new System.Windows.Forms.Button();
            this.buttonSegment = new System.Windows.Forms.Button();
            this.buttonRootMethod = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.numericUpDownDark = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSaveImage6 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSaveImage4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpenImage
            // 
            this.buttonOpenImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenImage.Location = new System.Drawing.Point(63, 27);
            this.buttonOpenImage.Name = "buttonOpenImage";
            this.buttonOpenImage.Size = new System.Drawing.Size(75, 25);
            this.buttonOpenImage.TabIndex = 0;
            this.buttonOpenImage.Text = "Open image";
            this.buttonOpenImage.UseVisualStyleBackColor = true;
            this.buttonOpenImage.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonImpulseNoise
            // 
            this.buttonImpulseNoise.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImpulseNoise.Location = new System.Drawing.Point(380, 27);
            this.buttonImpulseNoise.Name = "buttonImpulseNoise";
            this.buttonImpulseNoise.Size = new System.Drawing.Size(120, 25);
            this.buttonImpulseNoise.TabIndex = 1;
            this.buttonImpulseNoise.Text = "Impulse noise";
            this.buttonImpulseNoise.UseVisualStyleBackColor = true;
            this.buttonImpulseNoise.Visible = false;
            this.buttonImpulseNoise.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonBreadthFirst
            // 
            this.buttonBreadthFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBreadthFirst.Location = new System.Drawing.Point(600, 27);
            this.buttonBreadthFirst.Name = "buttonBreadthFirst";
            this.buttonBreadthFirst.Size = new System.Drawing.Size(120, 25);
            this.buttonBreadthFirst.TabIndex = 3;
            this.buttonBreadthFirst.Text = "Breadth First Search";
            this.buttonBreadthFirst.UseVisualStyleBackColor = true;
            this.buttonBreadthFirst.Visible = false;
            this.buttonBreadthFirst.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonSegment
            // 
            this.buttonSegment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSegment.Location = new System.Drawing.Point(228, 29);
            this.buttonSegment.Name = "buttonSegment";
            this.buttonSegment.Size = new System.Drawing.Size(75, 23);
            this.buttonSegment.TabIndex = 2;
            this.buttonSegment.Text = "Segment";
            this.buttonSegment.UseVisualStyleBackColor = true;
            this.buttonSegment.Visible = false;
            this.buttonSegment.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonRootMethod
            // 
            this.buttonRootMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRootMethod.Location = new System.Drawing.Point(954, 27);
            this.buttonRootMethod.Name = "buttonRootMethod";
            this.buttonRootMethod.Size = new System.Drawing.Size(114, 25);
            this.buttonRootMethod.TabIndex = 4;
            this.buttonRootMethod.Text = "Root method";
            this.buttonRootMethod.UseVisualStyleBackColor = true;
            this.buttonRootMethod.Visible = false;
            this.buttonRootMethod.Click += new System.EventHandler(this.button5_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(31, 167);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1127, 15);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(10, 250);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(148, 217);
            this.label1.MaximumSize = new System.Drawing.Size(400, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(333, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "                                                                                 " +
    "";
            this.label1.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1227, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // numericUpDownDark
            // 
            this.numericUpDownDark.Location = new System.Drawing.Point(362, 86);
            this.numericUpDownDark.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDark.Name = "numericUpDownDark";
            this.numericUpDownDark.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownDark.TabIndex = 13;
            this.numericUpDownDark.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDark.Visible = false;
            // 
            // numericUpDownLight
            // 
            this.numericUpDownLight.Location = new System.Drawing.Point(459, 86);
            this.numericUpDownLight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownLight.Name = "numericUpDownLight";
            this.numericUpDownLight.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownLight.TabIndex = 14;
            this.numericUpDownLight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownLight.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(367, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Delete. dark";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(463, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Delete light";
            this.label5.Visible = false;
            // 
            // buttonSaveImage6
            // 
            this.buttonSaveImage6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveImage6.Location = new System.Drawing.Point(1086, 27);
            this.buttonSaveImage6.Name = "buttonSaveImage6";
            this.buttonSaveImage6.Size = new System.Drawing.Size(113, 25);
            this.buttonSaveImage6.TabIndex = 17;
            this.buttonSaveImage6.Text = "Save image of \'Root\'";
            this.buttonSaveImage6.UseVisualStyleBackColor = true;
            this.buttonSaveImage6.Visible = false;
            this.buttonSaveImage6.Click += new System.EventHandler(this.button6_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(615, 250);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(600, 600);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(829, 217);
            this.label2.MaximumSize = new System.Drawing.Size(400, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(333, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "                                                                                 " +
    "";
            this.label2.Visible = false;
            // 
            // buttonSaveImage4
            // 
            this.buttonSaveImage4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveImage4.Location = new System.Drawing.Point(743, 27);
            this.buttonSaveImage4.Name = "buttonSaveImage4";
            this.buttonSaveImage4.Size = new System.Drawing.Size(128, 25);
            this.buttonSaveImage4.TabIndex = 22;
            this.buttonSaveImage4.Text = "Save image of \'Breadth\'";
            this.buttonSaveImage4.UseVisualStyleBackColor = true;
            this.buttonSaveImage4.Visible = false;
            this.buttonSaveImage4.Click += new System.EventHandler(this.button8_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "Opened image:";
            this.label3.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(474, 217);
            this.label6.MaximumSize = new System.Drawing.Size(400, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(333, 20);
            this.label6.TabIndex = 24;
            this.label6.Text = "                                                                                 " +
    "";
            this.label6.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1227, 912);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSaveImage4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.buttonSaveImage6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownLight);
            this.Controls.Add(this.numericUpDownDark);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonRootMethod);
            this.Controls.Add(this.buttonBreadthFirst);
            this.Controls.Add(this.buttonSegment);
            this.Controls.Add(this.buttonImpulseNoise);
            this.Controls.Add(this.buttonOpenImage);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Image Segmentation and Connected Components";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonOpenImage;
    private System.Windows.Forms.Button buttonImpulseNoise;
    private System.Windows.Forms.Button buttonBreadthFirst;
    private System.Windows.Forms.Button buttonSegment;
    private System.Windows.Forms.Button buttonRootMethod;
    public System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.NumericUpDown numericUpDownDark;
    private System.Windows.Forms.NumericUpDown numericUpDownLight;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button buttonSaveImage6;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button buttonSaveImage4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label6;
  }
}

