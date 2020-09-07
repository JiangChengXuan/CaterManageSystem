namespace CaterUI
{
    partial class FormOrderDish
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
            this.ddlType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAllDish = new System.Windows.Forms.DataGridView();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblMoney = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOrder = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.dgvOrderDetail = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsConfirm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllDish)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetail)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ddlType
            // 
            this.ddlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlType.FormattingEnabled = true;
            this.ddlType.Location = new System.Drawing.Point(858, 40);
            this.ddlType.Margin = new System.Windows.Forms.Padding(8);
            this.ddlType.Name = "ddlType";
            this.ddlType.Size = new System.Drawing.Size(367, 38);
            this.ddlType.TabIndex = 3;
            this.ddlType.SelectedIndexChanged += new System.EventHandler(this.ddlType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(693, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "菜品分类：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "菜名首字母：";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "DChar";
            this.Column4.HeaderText = "首字母";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "DTypeTitle";
            this.Column3.HeaderText = "分类";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "DPrice";
            this.Column8.HeaderText = "价格";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "DTitle";
            this.Column2.HeaderText = "名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "DId";
            this.Column1.HeaderText = "编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // dgvAllDish
            // 
            this.dgvAllDish.AllowUserToAddRows = false;
            this.dgvAllDish.AllowUserToDeleteRows = false;
            this.dgvAllDish.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllDish.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column8,
            this.Column3,
            this.Column4});
            this.dgvAllDish.Location = new System.Drawing.Point(15, 102);
            this.dgvAllDish.Margin = new System.Windows.Forms.Padding(8);
            this.dgvAllDish.MultiSelect = false;
            this.dgvAllDish.Name = "dgvAllDish";
            this.dgvAllDish.ReadOnly = true;
            this.dgvAllDish.RowTemplate.Height = 23;
            this.dgvAllDish.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllDish.Size = new System.Drawing.Size(1448, 1088);
            this.dgvAllDish.TabIndex = 4;
            this.dgvAllDish.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllDish_CellDoubleClick);
            this.dgvAllDish.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAllDish_CellFormatting);
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(202, 35);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(8);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(387, 42);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.lblMoney);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnOrder);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.dgvOrderDetail);
            this.groupBox2.Location = new System.Drawing.Point(1635, 39);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox2.Size = new System.Drawing.Size(1523, 1215);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "购物车-已点菜品";
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMoney.Location = new System.Drawing.Point(222, 52);
            this.lblMoney.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(28, 30);
            this.lblMoney.TabIndex = 7;
            this.lblMoney.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 30);
            this.label3.TabIndex = 6;
            this.label3.Text = "消费总计：￥";
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(920, 32);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(8);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(212, 58);
            this.btnOrder.TabIndex = 5;
            this.btnOrder.Text = "下单";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(635, 32);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(8);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(270, 58);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "删除选中项";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // dgvOrderDetail
            // 
            this.dgvOrderDetail.AllowUserToAddRows = false;
            this.dgvOrderDetail.AllowUserToDeleteRows = false;
            this.dgvOrderDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Count,
            this.Column9,
            this.IsConfirm});
            this.dgvOrderDetail.Location = new System.Drawing.Point(15, 102);
            this.dgvOrderDetail.Margin = new System.Windows.Forms.Padding(8);
            this.dgvOrderDetail.Name = "dgvOrderDetail";
            this.dgvOrderDetail.RowTemplate.Height = 23;
            this.dgvOrderDetail.Size = new System.Drawing.Size(1492, 1088);
            this.dgvOrderDetail.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvAllDish);
            this.groupBox1.Controls.Add(this.ddlType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTitle);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(48, 39);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(1496, 1215);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "菜单-双击菜品可加入购物车";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "SCId";
            this.Column5.HeaderText = "编号";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "DTitle";
            this.Column6.HeaderText = "名称";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Count
            // 
            this.Count.DataPropertyName = "Count";
            this.Count.HeaderText = "数量";
            this.Count.Name = "Count";
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "DPrice";
            this.Column9.HeaderText = "价格";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // IsConfirm
            // 
            this.IsConfirm.DataPropertyName = "IsConfirm";
            this.IsConfirm.HeaderText = "是否下单";
            this.IsConfirm.Name = "IsConfirm";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1165, 32);
            this.button1.Margin = new System.Windows.Forms.Padding(8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(212, 58);
            this.button1.TabIndex = 8;
            this.button1.Text = "退菜";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // FormOrderDish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(3536, 1432);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormOrderDish";
            this.Text = "点菜";
            this.Load += new System.EventHandler(this.FormOrderDish_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllDish)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetail)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridView dgvAllDish;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.DataGridView dgvOrderDetail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsConfirm;
        private System.Windows.Forms.Button button1;
    }
}