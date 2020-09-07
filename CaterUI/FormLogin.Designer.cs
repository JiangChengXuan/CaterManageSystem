namespace CaterUI
{
    partial class FormLogin
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
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.picCode = new System.Windows.Forms.PictureBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.linkResetCode = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picCode)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPwd
            // 
            this.txtPwd.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPwd.Location = new System.Drawing.Point(355, 274);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(409, 65);
            this.txtPwd.TabIndex = 7;
            this.txtPwd.Text = "123";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(355, 176);
            this.txtName.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(409, 65);
            this.txtName.TabIndex = 6;
            this.txtName.Text = "jcx";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(596, 521);
            this.btnClose.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(172, 82);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(345, 521);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(174, 82);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(211, 415);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "验证码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 30);
            this.label2.TabIndex = 10;
            this.label2.Text = "用户名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 309);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 30);
            this.label3.TabIndex = 11;
            this.label3.Text = "密码：";
            // 
            // picCode
            // 
            this.picCode.Location = new System.Drawing.Point(807, 370);
            this.picCode.Name = "picCode";
            this.picCode.Size = new System.Drawing.Size(279, 75);
            this.picCode.TabIndex = 12;
            this.picCode.TabStop = false;
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCode.Location = new System.Drawing.Point(355, 380);
            this.txtCode.Margin = new System.Windows.Forms.Padding(8);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(409, 65);
            this.txtCode.TabIndex = 13;
            // 
            // linkResetCode
            // 
            this.linkResetCode.AutoSize = true;
            this.linkResetCode.Location = new System.Drawing.Point(1124, 414);
            this.linkResetCode.Name = "linkResetCode";
            this.linkResetCode.Size = new System.Drawing.Size(103, 30);
            this.linkResetCode.TabIndex = 14;
            this.linkResetCode.TabStop = true;
            this.linkResetCode.Text = "看不清";
            this.linkResetCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkResetCode_LinkClicked);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1345, 859);
            this.Controls.Add(this.linkResetCode);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.picCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLogin);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormLogin";
            this.Text = "登录";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.LinkLabel linkResetCode;
    }
}