namespace WFpiecewiseLinear
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
            this.buttonSaveResult = new System.Windows.Forms.Button();
            this.pictureBoxOriginalImage = new System.Windows.Forms.PictureBox();
            this.pictureBoxContrastEnhancedImage = new System.Windows.Forms.PictureBox();
            this.pictureBoxHistogram = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginalImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxContrastEnhancedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHistogram)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpenImage
            // 
            this.buttonOpenImage.Location = new System.Drawing.Point(255, 12);
            this.buttonOpenImage.Name = "buttonOpenImage";
            this.buttonOpenImage.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenImage.TabIndex = 0;
            this.buttonOpenImage.Text = "Open image";
            this.buttonOpenImage.UseVisualStyleBackColor = true;
            this.buttonOpenImage.Click += new System.EventHandler(this.buttonOpenImage_Click);
            // 
            // buttonSaveResult
            // 
            this.buttonSaveResult.Location = new System.Drawing.Point(846, 12);
            this.buttonSaveResult.Name = "buttonSaveResult";
            this.buttonSaveResult.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveResult.TabIndex = 1;
            this.buttonSaveResult.Text = "Save result";
            this.buttonSaveResult.UseVisualStyleBackColor = true;
            this.buttonSaveResult.Click += new System.EventHandler(this.buttonSaveResult_Click);
            // 
            // pictureBoxOriginalImage
            // 
            this.pictureBoxOriginalImage.Location = new System.Drawing.Point(10, 120);
            this.pictureBoxOriginalImage.Name = "pictureBoxOriginalImage";
            this.pictureBoxOriginalImage.Size = new System.Drawing.Size(600, 600);
            this.pictureBoxOriginalImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOriginalImage.TabIndex = 2;
            this.pictureBoxOriginalImage.TabStop = false;
            // 
            // pictureBoxContrastEnhancedImage
            // 
            this.pictureBoxContrastEnhancedImage.Location = new System.Drawing.Point(620, 120);
            this.pictureBoxContrastEnhancedImage.Name = "pictureBoxContrastEnhancedImage";
            this.pictureBoxContrastEnhancedImage.Size = new System.Drawing.Size(600, 600);
            this.pictureBoxContrastEnhancedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxContrastEnhancedImage.TabIndex = 3;
            this.pictureBoxContrastEnhancedImage.TabStop = false;
            // 
            // pictureBoxHistogram
            // 
            this.pictureBoxHistogram.Location = new System.Drawing.Point(10, 464);
            this.pictureBoxHistogram.Name = "pictureBoxHistogram";
            this.pictureBoxHistogram.Size = new System.Drawing.Size(256, 256);
            this.pictureBoxHistogram.TabIndex = 5;
            this.pictureBoxHistogram.TabStop = false;
            this.pictureBoxHistogram.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox3_MouseDown);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(22, 76);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1150, 17);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(207, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Click at two points in the histogram";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(858, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Result image";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Opened image:";
            this.label3.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1272, 733);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBoxHistogram);
            this.Controls.Add(this.pictureBoxContrastEnhancedImage);
            this.Controls.Add(this.pictureBoxOriginalImage);
            this.Controls.Add(this.buttonSaveResult);
            this.Controls.Add(this.buttonOpenImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginalImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxContrastEnhancedImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHistogram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonOpenImage;
    private System.Windows.Forms.Button buttonSaveResult;
    private System.Windows.Forms.PictureBox pictureBoxOriginalImage;
    private System.Windows.Forms.PictureBox pictureBoxContrastEnhancedImage;
    private System.Windows.Forms.PictureBox pictureBoxHistogram;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
  }
}

