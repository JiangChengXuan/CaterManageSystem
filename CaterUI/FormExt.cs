using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaterUI
{
  public static  class FormExt
    {

      /// <summary>
      /// 将新增或修改表单中的控件重置为默认值
      /// </summary>
      public static void FormReset(this Control Control)
      {
        
          foreach (Control item in Control.Controls)
          {
              //标识为需要重置的控件
              if (item.Tag != null && item.Tag.ToString().Contains("FormInput")) 
              {
                  if (item is ComboBox)
                  {
                      #region ComboBox
                      ComboBox cb = item as ComboBox;
                      cb.SelectedIndex = 0; 
                      #endregion
                  }
                  else if (item is TextBox||item is Button)
                  {
                      if (item.Tag.ToString().Contains("@"))
                      {
                          item.Text = item.Tag.ToString().Split('@')[1];
                      }
                      else
                      {
                          TextBox txt = item as TextBox;
                          txt.Text = string.Empty;
                      }

                  }
                  else if (item is RadioButton)
                  {
                      RadioButton rb = item as RadioButton;
                      rb.Checked = true;
                  }
                  else if (item is Label)
                  {
                      if (item.Tag.ToString().Contains("@"))
                      {
                          item.Text = item.Tag.ToString().Split('@')[1];
                      }
                      else
                      {                     
                          item.Text = string.Empty;
                      }

                  }
              }
              FormReset(item);
          }  // END FormReset（）
 

      } // END FormReset（）

   
    }
}
