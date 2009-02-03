using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TypingBC.Presentation.View
{
    public partial class UserNameDlg : Form
    {
        public string strUserName;

        public UserNameDlg()
        {
            InitializeComponent();
        }

        private void UserNameDlg_Load(object sender, EventArgs e)
        {
            this.tbUserName.Text = "Không tên";
            
            /************************************************************************/
            /* Speak every thing about UserNameDialog here                          */
            /************************************************************************/

            

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            strUserName = this.tbUserName.Text;
            this.Close();
        }

        private void tbUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }
    }
}