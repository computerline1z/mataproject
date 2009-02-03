using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TypingBC.Presentation.View
{
    public partial class ViewDlg : Form
    {
        #region Properties
        private int iCurrLayer = 0;
        private int iCurrMode = 0;
        private string strCurrentUser;
        private bool isBlindView = true;
        private string strHelpString;
        #endregion
        
        public ViewDlg()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            ShowLayer0();
            /************************************************************************/
            /* Get and process user name here                                       */
            /************************************************************************/



        }

        private void ShowLayer0()
        {
            UserNameDlg dlg = new UserNameDlg();
            dlg.StartPosition = FormStartPosition.CenterScreen;

            /************************************************************************/
            /* Show dialog                                                          */
            /************************************************************************/
            dlg.ShowDialog();
            strCurrentUser = dlg.strUserName;
        }

        private void ShowLayer1()
        {
            this.lbMainMenu.Items.Clear();

            // Đưa các option vào
            this.lbMainMenu.Items.Add("Chế độ gõ bình thường");
            this.lbMainMenu.Items.Add("Chế độ gõ 6 phím Braille");

            /************************************************************************/
            /* Hướng dẫn cho người dùng về 2 chế độ gõ là như thế nào               */
            /************************************************************************/
                    
            if (isBlindView)
            {
                /************************************************************************/
                /* Đối với người mù thì đọc tất cả thông tin hướng dẫn ở đây            */
                /************************************************************************/




            }
            else
            {
                /************************************************************************/
                /* Đối với người người thấy đường thì hiển thị thông tin hướng dẫn      */
                /************************************************************************/



            }
        }

        private void ShowLayer2()
        {
            this.lbMainMenu.Items.Clear();

            // Đưa các option vào
        }

        private void ShowLayer3()
        {
            this.lbMainMenu.Items.Clear();

            // Đưa các option vào
        }

        private void ViewDlg_Load(object sender, EventArgs e)
        {
            iCurrLayer = 1;
            ShowLayer1();
        }

        private void lbMainMenu_DoubleClick(object sender, EventArgs e)
        {
            /************************************************************************/
            /* Double Click vô Main Menu                                            */
            /************************************************************************/



        }

        private void lbMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            /************************************************************************/
            /* Nhấn phím Enter vô Main Menu                                         */
            /************************************************************************/



        }
    }
}