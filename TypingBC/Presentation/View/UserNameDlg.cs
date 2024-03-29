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
            this.cbUserName.Text = "Không tên";
            this.cbUserName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.cbUserName.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.cbUserName.Focus();

            /************************************************************************/
            /* Load danh sách tất cả các UserName và đưa vào ComboBox*/
            /************************************************************************/
            for (char c = 'a'; c < 'z'; c++ )
            {
                this.cbUserName.Items.Add(c.ToString() + c.ToString());
            }

            
            /************************************************************************/
            /* Speak every thing about UserNameDialog here                          */
            /************************************************************************/

            

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            strUserName = this.cbUserName.Text;
            this.Dispose();
        }

        private void cbUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

        private void UserNameDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}