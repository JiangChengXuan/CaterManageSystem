using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterBLL.ManagerInfoPackage;
using CaterModel;
using CaterCommon;
using CaterModel.Enum;
namespace CaterUI
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        
        }

        ManagerInfoBLL managerInfoBLL = new ManagerInfoBLL();

        public string Code { get; set; }
        /// <summary>
        /// 系统登录
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            ManagerInfo userInfo;
            string uname = txtName.Text.Trim();
            string pwd = txtPwd.Text.Trim();

            //判断验证码
            if (txtCode.Text.Trim().ToLower()!=this.Code.ToLower())
            {
                 MessageBox.Show("验证码错误", "提示");
                 return;
            }


            LoginState loginState = managerInfoBLL.Login(uname, pwd, out userInfo);

       
            switch (loginState)
            {
                case LoginState.Success: 
                   
                    FormMain main = new FormMain(userInfo);
                    main.Show();
                    this.Hide();
                    break;
                case LoginState.PwdError:  
                    MessageBox.Show("密码错误", "提示");
                    break;
                case LoginState.UnameError: 
                    MessageBox.Show("用户名错误", "提示");
                    break;
            }

           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }// END btnLogin_Click（）

        private void FormLogin_Load(object sender, EventArgs e)
        {
            LoadAuthCode();
        }


        private void LoadAuthCode()
        {
            AuthCode authCode = new AuthCode();
            Image image = authCode.DrawAuthCodeImg();
            picCode.Image = image;
            Code=authCode.Code;
         
           
        }

        private void linkResetCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadAuthCode();
        }  // END LoadAuthCode（）


    }
}
