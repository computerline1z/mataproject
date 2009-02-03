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
        private TypingMode tmCurrMode = TypingMode.NORMAL;
        private ExerciseSetType estCurrExerciseSet = ExerciseSetType.RECORNITION;
        private int iCurrExercise = 0;
        private string strCurrentUser;
        private bool isBlindView = true;
        private string strHelpString = "Help me!";

        #endregion
        
        public ViewDlg()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            ShowLayer0();
        }

        /// <summary>
        /// Dialog lấy tên User
        /// </summary>
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

        /// <summary>
        /// Chọn chế độ gõ là Normal hay Braille
        /// </summary>
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


        /// <summary>
        /// Chọn hệ thông bài tập (tùy theo chế độ gõ hiện tại)
        /// </summary>
        private void ShowLayer2()
        {
            this.lbMainMenu.Items.Clear();

            // Đưa các option vào
            this.lbMainMenu.Items.Add("Nhận diện bàn phím");
            this.lbMainMenu.Items.Add("Bài tập gõ một phím");
            this.lbMainMenu.Items.Add("Bài tập gõ không dấu");

            if (tmCurrMode == TypingMode.NORMAL)
            {
                this.lbMainMenu.Items.Add("Bài tập gõ VNI");
                this.lbMainMenu.Items.Add("Bài tập gõ TELEX");
            }
            else
            {
                this.lbMainMenu.Items.Add("Bài tập gõ có dấu");
            }
            
            
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

        /// <summary>
        /// Chọn một bài tập luyện tập (tùy thuộc vào ExerciseSet hiện tại)
        /// </summary>
        private void ShowLayer3()
        {
            this.lbMainMenu.Items.Clear();

            // Đưa các option vào
            int nSoBaiTap = 10;
            for (int i = 0; i < nSoBaiTap; i++ )
            {
                this.lbMainMenu.Items.Add("Bài tập " + (i+1).ToString());
            }

            
            
            
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

        /// <summary>
        /// Hiển thị vùng xử lý bài tập
        /// </summary>
        private void ShowLayer4()
        {
            this.lbMainMenu.Items.Clear();

            /************************************************************************/
            /* Hiển thị giao diện vùng cửa sổ xử lý bài tập                         */
            /************************************************************************/



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


        /// <summary>
        /// Load dữ liệu ()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewDlg_Load(object sender, EventArgs e)
        {
            /************************************************************************/
            /* Lấy và xử lý User Name ở đây                                         */
            /* Lấy các thông tin sau:                                               */
            /*      isBlindView                                                     */
            /*      tmCurrMode                                                      */
            /*      estCurrExerciseSet                                              */
            /*      iCurrExercise                                                   */
            /*      iCurrLayer : suy ra từ các thông tin ở trên (để T làm cái này)  */
            /************************************************************************/




            /************************************************************************/
            /* Nếu UserName đã tồn tại thì load vô mục bài tập đang làm             */
            /************************************************************************/



            // Nếu UserName chưa tồn tại
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
            int idx = this.lbMainMenu.SelectedIndex;

            switch(e.KeyCode)
            {
                case Keys.Enter:
                    switch (iCurrLayer)
                    {
                        case 1:
                            iCurrLayer++;
                            tmCurrMode = (TypingMode)idx;
                            ShowLayer2();
                            break;
                        case 2:
                            iCurrLayer++;
                            estCurrExerciseSet = (ExerciseSetType)idx;
                            ShowLayer3();
                            break;
                        case 3:
                            iCurrLayer++;
                            iCurrExercise = idx;
                            this.lbMainMenu.Visible = false;
                            ShowLayer4();
                            break;
                    }
                    break;

                case Keys.Escape:
                    switch (iCurrLayer)
                    {
                        case 2:
                            iCurrLayer--;
                            ShowLayer1();
                            break;
                        case 3:
                            iCurrLayer--;
                            ShowLayer2();
                            break;
                        case 4:
                            iCurrLayer--;
                            this.lbMainMenu.Visible = true;
                            ShowLayer3();
                            break;
                    }
                    break;
            }
        }
    }
}