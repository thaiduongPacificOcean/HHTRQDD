
namespace HHTRQD
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
            this.label1 = new System.Windows.Forms.Label();
            this.gvTrongSo = new System.Windows.Forms.DataGridView();
            this.btnTinhAHP = new System.Windows.Forms.Button();
            this.gvKetqua = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveDatabase = new System.Windows.Forms.Button();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
            this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
            this.gvNangLuc = new System.Windows.Forms.DataGridView();
            this.gvLVNhom = new System.Windows.Forms.DataGridView();
            this.gvLogic = new System.Windows.Forms.DataGridView();
            this.gvGiaoTiep = new System.Windows.Forms.DataGridView();
            this.gvThaiDo = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gvTrongSo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKetqua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNangLuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLVNhom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLogic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGiaoTiep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvThaiDo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 20F);
            this.label1.Location = new System.Drawing.Point(533, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trọng số tiêu chí";
            // 
            // gvTrongSo
            // 
            this.gvTrongSo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTrongSo.Location = new System.Drawing.Point(28, 78);
            this.gvTrongSo.Name = "gvTrongSo";
            this.gvTrongSo.RowHeadersWidth = 51;
            this.gvTrongSo.RowTemplate.Height = 24;
            this.gvTrongSo.Size = new System.Drawing.Size(1258, 251);
            this.gvTrongSo.TabIndex = 1;
            this.gvTrongSo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvTrongSo_CellFormatting);
            this.gvTrongSo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvTrongSo_CellValueChanged);
            // 
            // btnTinhAHP
            // 
            this.btnTinhAHP.Location = new System.Drawing.Point(28, 347);
            this.btnTinhAHP.Name = "btnTinhAHP";
            this.btnTinhAHP.Size = new System.Drawing.Size(101, 26);
            this.btnTinhAHP.TabIndex = 2;
            this.btnTinhAHP.Text = "Tính AHP";
            this.btnTinhAHP.UseVisualStyleBackColor = true;
            this.btnTinhAHP.Click += new System.EventHandler(this.btnTinhAHP_Click);
            // 
            // gvKetqua
            // 
            this.gvKetqua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvKetqua.Location = new System.Drawing.Point(28, 427);
            this.gvKetqua.Name = "gvKetqua";
            this.gvKetqua.RowHeadersWidth = 51;
            this.gvKetqua.RowTemplate.Height = 24;
            this.gvKetqua.Size = new System.Drawing.Size(1258, 251);
            this.gvKetqua.TabIndex = 3;
            this.gvKetqua.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvKetqua_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 392);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Kết quả:";
            // 
            // btnSaveDatabase
            // 
            this.btnSaveDatabase.Location = new System.Drawing.Point(1185, 383);
            this.btnSaveDatabase.Name = "btnSaveDatabase";
            this.btnSaveDatabase.Size = new System.Drawing.Size(101, 30);
            this.btnSaveDatabase.TabIndex = 5;
            this.btnSaveDatabase.Text = "Lưu kết quả";
            this.btnSaveDatabase.UseVisualStyleBackColor = true;
            this.btnSaveDatabase.Click += new System.EventHandler(this.btnSaveDatabase_Click);
            // 
            // btnGoBack
            // 
            this.btnGoBack.Location = new System.Drawing.Point(1185, 684);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(101, 30);
            this.btnGoBack.TabIndex = 6;
            this.btnGoBack.Text = "Trở về";
            this.btnGoBack.UseVisualStyleBackColor = true;
            // 
            // sqlDataAdapter1
            // 
            this.sqlDataAdapter1.DeleteCommand = this.sqlDeleteCommand1;
            this.sqlDataAdapter1.InsertCommand = this.sqlInsertCommand1;
            this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
            this.sqlDataAdapter1.UpdateCommand = this.sqlUpdateCommand1;
            // 
            // gvNangLuc
            // 
            this.gvNangLuc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvNangLuc.Location = new System.Drawing.Point(164, 360);
            this.gvNangLuc.Name = "gvNangLuc";
            this.gvNangLuc.RowHeadersWidth = 51;
            this.gvNangLuc.RowTemplate.Height = 24;
            this.gvNangLuc.Size = new System.Drawing.Size(38, 36);
            this.gvNangLuc.TabIndex = 7;
            this.gvNangLuc.Visible = false;
            // 
            // gvLVNhom
            // 
            this.gvLVNhom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvLVNhom.Location = new System.Drawing.Point(244, 360);
            this.gvLVNhom.Name = "gvLVNhom";
            this.gvLVNhom.RowHeadersWidth = 51;
            this.gvLVNhom.RowTemplate.Height = 24;
            this.gvLVNhom.Size = new System.Drawing.Size(52, 36);
            this.gvLVNhom.TabIndex = 8;
            this.gvLVNhom.Visible = false;
            // 
            // gvLogic
            // 
            this.gvLogic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvLogic.Location = new System.Drawing.Point(324, 360);
            this.gvLogic.Name = "gvLogic";
            this.gvLogic.RowHeadersWidth = 51;
            this.gvLogic.RowTemplate.Height = 24;
            this.gvLogic.Size = new System.Drawing.Size(68, 36);
            this.gvLogic.TabIndex = 9;
            this.gvLogic.Visible = false;
            // 
            // gvGiaoTiep
            // 
            this.gvGiaoTiep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvGiaoTiep.Location = new System.Drawing.Point(425, 360);
            this.gvGiaoTiep.Name = "gvGiaoTiep";
            this.gvGiaoTiep.RowHeadersWidth = 51;
            this.gvGiaoTiep.RowTemplate.Height = 24;
            this.gvGiaoTiep.Size = new System.Drawing.Size(60, 36);
            this.gvGiaoTiep.TabIndex = 10;
            this.gvGiaoTiep.Visible = false;
            // 
            // gvThaiDo
            // 
            this.gvThaiDo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvThaiDo.Location = new System.Drawing.Point(514, 360);
            this.gvThaiDo.Name = "gvThaiDo";
            this.gvThaiDo.RowHeadersWidth = 51;
            this.gvThaiDo.RowTemplate.Height = 24;
            this.gvThaiDo.Size = new System.Drawing.Size(58, 36);
            this.gvThaiDo.TabIndex = 11;
            this.gvThaiDo.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1318, 735);
            this.Controls.Add(this.gvThaiDo);
            this.Controls.Add(this.gvGiaoTiep);
            this.Controls.Add(this.gvLogic);
            this.Controls.Add(this.gvLVNhom);
            this.Controls.Add(this.gvNangLuc);
            this.Controls.Add(this.btnGoBack);
            this.Controls.Add(this.btnSaveDatabase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gvKetqua);
            this.Controls.Add(this.btnTinhAHP);
            this.Controls.Add(this.gvTrongSo);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvTrongSo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKetqua)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNangLuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLVNhom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLogic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGiaoTiep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvThaiDo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DataGridView gvTrongSo;
        private System.Windows.Forms.Button btnTinhAHP;
        private System.Windows.Forms.DataGridView gvKetqua;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveDatabase;
        private System.Windows.Forms.Button btnGoBack;
        private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
        private System.Data.SqlClient.SqlCommand sqlInsertCommand1;
        private System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
        private System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        private System.Windows.Forms.DataGridView gvNangLuc;
        private System.Windows.Forms.DataGridView gvLVNhom;
        private System.Windows.Forms.DataGridView gvLogic;
        private System.Windows.Forms.DataGridView gvGiaoTiep;
        private System.Windows.Forms.DataGridView gvThaiDo;
    }
}