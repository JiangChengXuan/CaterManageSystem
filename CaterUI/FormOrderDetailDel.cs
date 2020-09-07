using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaterUI
{
    public partial class FormOrderDetailDel : Form
    {
        public FormOrderDetailDel()
        {
            InitializeComponent();
        }

        public event Action<int> DeleteDishHandle;
        public int delMaxNum = 100;

        /// <summary>
        /// 确认数量，进行数据删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int quantity = Convert.ToInt32(numericUpDown1.Value);

            DeleteDishHandle(quantity);

            this.Close();
        }

        private void FormOrderDetailDel_Load(object sender, EventArgs e)
        {
            numericUpDown1.Maximum = delMaxNum;
        }

        
    }
}
