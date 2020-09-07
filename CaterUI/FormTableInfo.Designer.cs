namespace CaterUI
{
    partial class FormTableInfo
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
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearchAll = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rbUnFree = new System.Windows.Forms.RadioButton();
            this.rbFree = new System.Windows.Forms.RadioButton();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAddHall = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.ddlHallAdd = new System.Windows.Forms.ComboBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ddlFreeSearch = new System.Windows.Forms.ComboBox();
            this.ddlHallSearch = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvList);
            this.groupBox1.Location = new System.Drawing.Point(24, 22);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(1135, 1120);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "列表";
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5});
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(8, 43);
            this.dgvList.Margin = new System.Windows.Forms.Padding(8);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1119, 1069);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            this.dgvList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvList_CellFormatting);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "TId";
            this.Column1.HeaderText = "编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "TTitle";
            this.Column2.HeaderText = "名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "HTitle";
            this.Column3.HeaderText = "厅包";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "TIsFree";
            this.Column5.HeaderText = "是否空闲";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // btnSearchAll
            // 
            this.btnSearchAll.Location = new System.Drawing.Point(22, 232);
            this.btnSearchAll.Margin = new System.Windows.Forms.Padding(8);
            this.btnSearchAll.Name = "btnSearchAll";
            this.btnSearchAll.Size = new System.Drawing.Size(398, 62);
            this.btnSearchAll.TabIndex = 5;
            this.btnSearchAll.Text = "显示全部";
            this.btnSearchAll.UseVisualStyleBackColor = true;
            this.btnSearchAll.Click += new System.EventHandler(this.btnSearchAll_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 152);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "空  闲：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "厅  包：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.rbUnFree);
            this.groupBox3.Controls.Add(this.rbFree);
            this.groupBox3.Controls.Add(this.btnRemove);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.btnAddHall);
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.ddlHallAdd);
            this.groupBox3.Controls.Add(this.txtTitle);
            this.groupBox3.Controls.Add(this.txtId);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(1174, 350);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox3.Size = new System.Drawing.Size(442, 792);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "添加\\修改";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label7.Location = new System.Drawing.Point(100, 218);
            this.label7.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(313, 60);
            this.label7.TabIndex = 22;
            this.label7.Text = "如果是包间则填写名称\r\n如果是厅堂则填写号码";
            // 
            // rbUnFree
            // 
            this.rbUnFree.AutoSize = true;
            this.rbUnFree.Location = new System.Drawing.Point(272, 472);
            this.rbUnFree.Margin = new System.Windows.Forms.Padding(8);
            this.rbUnFree.Name = "rbUnFree";
            this.rbUnFree.Size = new System.Drawing.Size(140, 34);
            this.rbUnFree.TabIndex = 21;
            this.rbUnFree.Text = "使用中";
            this.rbUnFree.UseVisualStyleBackColor = true;
            // 
            // rbFree
            // 
            this.rbFree.AutoSize = true;
            this.rbFree.Checked = true;
            this.rbFree.Location = new System.Drawing.Point(140, 472);
            this.rbFree.Margin = new System.Windows.Forms.Padding(8);
            this.rbFree.Name = "rbFree";
            this.rbFree.Size = new System.Drawing.Size(110, 34);
            this.rbFree.TabIndex = 20;
            this.rbFree.TabStop = true;
            this.rbFree.Tag = "FormInput";
            this.rbFree.Text = "空闲";
            this.rbFree.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(28, 700);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(8);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(392, 62);
            this.btnRemove.TabIndex = 19;
            this.btnRemove.Text = "删除选中的行数据";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(22, 645);
            this.label9.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(373, 30);
            this.label9.TabIndex = 15;
            this.label9.Text = "提示：双击表格项进行修改";
            // 
            // btnAddHall
            // 
            this.btnAddHall.Location = new System.Drawing.Point(140, 372);
            this.btnAddHall.Margin = new System.Windows.Forms.Padding(8);
            this.btnAddHall.Name = "btnAddHall";
            this.btnAddHall.Size = new System.Drawing.Size(280, 62);
            this.btnAddHall.TabIndex = 13;
            this.btnAddHall.Text = "厅包管理";
            this.btnAddHall.UseVisualStyleBackColor = true;
            this.btnAddHall.Click += new System.EventHandler(this.btnAddHall_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(245, 550);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(175, 62);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(28, 550);
            this.btnSave.Margin = new System.Windows.Forms.Padding(8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(175, 62);
            this.btnSave.TabIndex = 11;
            this.btnSave.Tag = "FormInput@添加";
            this.btnSave.Text = "添加";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ddlHallAdd
            // 
            this.ddlHallAdd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlHallAdd.FormattingEnabled = true;
            this.ddlHallAdd.Location = new System.Drawing.Point(140, 305);
            this.ddlHallAdd.Margin = new System.Windows.Forms.Padding(8);
            this.ddlHallAdd.Name = "ddlHallAdd";
            this.ddlHallAdd.Size = new System.Drawing.Size(274, 38);
            this.ddlHallAdd.TabIndex = 10;
            this.ddlHallAdd.Tag = "FormInput";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(140, 148);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(8);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(274, 42);
            this.txtTitle.TabIndex = 6;
            this.txtTitle.Tag = "FormInput";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(140, 65);
            this.txtId.Margin = new System.Windows.Forms.Padding(8);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(274, 42);
            this.txtId.TabIndex = 5;
            this.txtId.Tag = "FormInput@添加时无编号";
            this.txtId.Text = "添加时无编号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 472);
            this.label6.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 30);
            this.label6.TabIndex = 3;
            this.label6.Text = "空  闲：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 312);
            this.label5.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 30);
            this.label5.TabIndex = 2;
            this.label5.Text = "厅  包：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 158);
            this.label4.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 30);
            this.label4.TabIndex = 1;
            this.label4.Text = "名  称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "编  号：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ddlFreeSearch);
            this.groupBox2.Controls.Add(this.ddlHallSearch);
            this.groupBox2.Controls.Add(this.btnSearchAll);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(1174, 22);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox2.Size = new System.Drawing.Size(442, 312);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "搜索";
            // 
            // ddlFreeSearch
            // 
            this.ddlFreeSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlFreeSearch.FormattingEnabled = true;
            this.ddlFreeSearch.Location = new System.Drawing.Point(140, 145);
            this.ddlFreeSearch.Margin = new System.Windows.Forms.Padding(8);
            this.ddlFreeSearch.Name = "ddlFreeSearch";
            this.ddlFreeSearch.Size = new System.Drawing.Size(274, 38);
            this.ddlFreeSearch.TabIndex = 9;
            this.ddlFreeSearch.SelectedIndexChanged += new System.EventHandler(this.ddlSearch_SelectedIndexChanged);
            // 
            // ddlHallSearch
            // 
            this.ddlHallSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlHallSearch.FormattingEnabled = true;
            this.ddlHallSearch.Location = new System.Drawing.Point(140, 48);
            this.ddlHallSearch.Margin = new System.Windows.Forms.Padding(8);
            this.ddlHallSearch.Name = "ddlHallSearch";
            this.ddlHallSearch.Size = new System.Drawing.Size(274, 38);
            this.ddlHallSearch.TabIndex = 8;
            this.ddlHallSearch.SelectedIndexChanged += new System.EventHandler(this.ddlSearch_SelectedIndexChanged);
            // 
            // FormTableInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1640, 1165);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "FormTableInfo";
            this.Text = "餐桌管理";
            this.Load += new System.EventHandler(this.FormTableInfo_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnSearchAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbUnFree;
        private System.Windows.Forms.RadioButton rbFree;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnAddHall;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox ddlHallAdd;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox ddlFreeSearch;
        private System.Windows.Forms.ComboBox ddlHallSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}