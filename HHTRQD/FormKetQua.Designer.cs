
namespace HHTRQD
{
  partial class FormKetQua
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
      this.label1 = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Arial", 26F, System.Drawing.FontStyle.Bold);
      this.label1.Location = new System.Drawing.Point(217, 42);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(353, 51);
      this.label1.TabIndex = 0;
      this.label1.Text = "Kết Quả Tư Vấn";
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("Arial", 14F);
      this.button1.Location = new System.Drawing.Point(510, 476);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(187, 63);
      this.button1.TabIndex = 3;
      this.button1.Text = "Tư vấn mới";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // button2
      // 
      this.button2.Font = new System.Drawing.Font("Arial", 14F);
      this.button2.Location = new System.Drawing.Point(98, 476);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(187, 63);
      this.button2.TabIndex = 4;
      this.button2.Text = "Quay lại";
      this.button2.UseVisualStyleBackColor = true;
      // 
      // dataGridView1
      // 
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Location = new System.Drawing.Point(245, 120);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.RowHeadersWidth = 51;
      this.dataGridView1.RowTemplate.Height = 24;
      this.dataGridView1.Size = new System.Drawing.Size(300, 300);
      this.dataGridView1.TabIndex = 5;
      // 
      // FormKetQua
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(792, 572);
      this.Controls.Add(this.dataGridView1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.label1);
      this.Name = "FormKetQua";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "FormKetQua";
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.DataGridView dataGridView1;
  }
}