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
using  System.Text.RegularExpressions;
namespace CaterUI
{
    public partial class FormManagerInfo : Form
    {
        //实现单例模式-构造函数私有化
        private FormManagerInfo()
        {
            InitializeComponent();
        }

        private static object obj = new object(); //单例同步辅助对象
        private static FormManagerInfo formManagerInfo = null;  //存储单例对象

        /// <summary>
        /// 全局单例对象获取-双重判断
        /// </summary>
        public static FormManagerInfo CreateInstance()
        {
            if (formManagerInfo == null || formManagerInfo.IsDisposed)
            {
                lock (obj)
                {
                    if (formManagerInfo == null || formManagerInfo.IsDisposed)
                    {
                        formManagerInfo = new FormManagerInfo();
                    }
                }
            }
            return formManagerInfo;
        }  // END CreateInstance（）


        ManagerInfoBLL managerInfoBLL = new ManagerInfoBLL();

        private List<ManagerInfo> managerInfoList = new List<ManagerInfo>();

        private void FormManagerInfo_Load(object sender, EventArgs e)
        {
             
            LoadManagerInfo();
            comMType.DataSource = managerInfoBLL.GetManagerTypeList();
            comMType.DisplayMember = "Value";
            comMType.ValueMember = "ID";
        }


        private void LoadManagerInfo()
        {
             managerInfoList = managerInfoBLL.GetManagerInfoList();

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = managerInfoList;
            if (dgvList.Rows.Count > 0)
            {
                dgvList.Rows[0].Selected = false;
            }
        }

        //添加管理员
        private void btnSave_Click(object sender, EventArgs e)
        {
             bool result =false;
             string resultMsg = "处理失败";

             #region 封装数据对象

             ManagerInfo data = new ManagerInfo();
             if (Regex.IsMatch(txtId.Text,"\\d+"))
             {
                 data.MId = Convert.ToInt32(txtId.Text);
             }
              
             data.MName = txtName.Text;
             data.MType = comMType.SelectedValue.ToString();
             data.MPwd = txtPwd.Text; 
             #endregion
 
             
             ManagerInfo managerInfo = managerInfoList.SingleOrDefault(s => s.MId.ToString() == txtId.Text);
            if (managerInfo != null)  //修改
            {
                //未修改密码             
                if (managerInfo.MPwd == data.MPwd)
                {
                    data.MPwd = null;
                }
                result = managerInfoBLL.EditManagerInfo(data);
            }
            else  //新增
            {
                result = managerInfoBLL.AddManagerInfo(data, out resultMsg);
            } 
         



            #region Message
            if (result)
            {
                MessageBox.Show("处理成功");             
            }
            else
            {
                MessageBox.Show(resultMsg);
            } 
            #endregion

            //重置表单
            ResetForm();

            LoadManagerInfo();
            
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //将类型列格式化转换成对应的含义
            if (e.ColumnIndex==2)
            {
                int num = Convert.ToInt32(e.Value);
                ManagerInfo.ManagerType type=(ManagerInfo.ManagerType)num;
                e.Value = type.ToString();
            }
        }

       
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
                    //获取选中行
            ManagerInfo managerInfo = managerInfoList[e.RowIndex];

            //数据绑定到控件
            txtId.Text = managerInfo.MId.ToString();
            txtName.Text = managerInfo.MName;           
            txtPwd.Text = managerInfo.MPwd;

            //类型
            for (int i = 0; i < comMType.Items.Count; i++)
            {
                KeyValue obj = comMType.Items[i] as KeyValue;
                if (obj.ID==Convert.ToInt32(managerInfo.MType))
                {
                    comMType.SelectedIndex = i;
                }
            }

            btnSave.Text = "修改";
        
        }

        /// <summary>
        /// 重置表单
        /// </summary>
      private void ResetForm()
        {
            btnSave.Text = "添加";
            txtId.Text = "添加时无编号";
            txtName.Text = string.Empty;
            txtPwd.Text = string.Empty;
            comMType.SelectedIndex = 0;

        }

      private void btnCancel_Click(object sender, EventArgs e)
      {
          ResetForm();
      }

       //删除
      private void btnRemove_Click(object sender, EventArgs e)
      {
          if (dgvList.SelectedRows.Count <= 0)
          {
              MessageBox.Show("请选中您要删除的行", "提示");
              return;
          }

        DialogResult dr=  MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.YesNo);

        if (dr != DialogResult.Yes) return;
 
        int mid = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);

        managerInfoBLL.DeleteManagerInfo(mid); //执行删除

        LoadManagerInfo(); //刷新表格数据

      }

        
    }
}
