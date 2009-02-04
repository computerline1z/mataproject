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
            HideLayer4();
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
            HideLayer4();
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
            HideLayer4();
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
            HideLayer4();
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
            // Ẩn Main Menu
            this.lbMainMenu.Items.Clear();
            this.lbMainMenu.Visible = false;

            /************************************************************************/
            /* Hiển thị giao diện vùng cửa sổ xử lý bài tập                         */
            /************************************************************************/
            this.layer4Label.Visible = true;
            this.layer4LabelString.Visible = true;
            this.layer4RTBVungTapGo.Visible = true;


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
        /// Ẩn layer 4
        /// </summary>
        private void HideLayer4()
        {
            this.layer4Label.Visible = false;
            this.layer4LabelString.Visible = false;
            this.layer4RTBVungTapGo.Visible = false;
        }


        /// <summary>
        /// Hiện List Hot Keys
        /// </summary>
        private void ShowListHotKeyBalloon()
        {
            /************************************************************************/
            /* List Hot Keys Baloon                                                 */
            /************************************************************************/
            this.notifyIcon.BalloonTipIcon = ToolTipIcon.Info;

            string strListHotKey = "F1 : Giới thiệu chương trình và hệ thống bài tập\n" +
                                   "F2 : Giới thiệu hệ thống các Hot Key\n" +
                                   "F3 : Làm gì đó\n" +
                                   "F4 : Làm gì đó\n" +
                                   "F5: Làm gì đó\n" +
                                   "F6: Làm gì đó\n" +
                                   "F7 Làm gì đó\n" +
                                   "F8: Làm gì đó\n" +
                                   "F9: Làm gì đó\n" +
                                   "F10 Làm gì đó\n" +
                                   "F11 Làm gì đó\n" +
                                   "F12 : Làm gì đó\n";
            
            this.notifyIcon.ShowBalloonTip(8000, "Hotkeys", strListHotKey, ToolTipIcon.Info);
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


            ShowListHotKeyBalloon();
        }

        /// <summary>
        /// Khi có sự kiện double click vào Main Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMainMenu_DoubleClick(object sender, EventArgs e)
        {
            /************************************************************************/
            /* Double Click vô Main Menu                                            */
            /************************************************************************/

            int idx = this.lbMainMenu.SelectedIndex;

            if (idx < 0)
                return;

            switch (iCurrLayer)
            {
                case 1:
                    iCurrLayer++;
                    tmCurrMode = (TypingMode)idx;
                    ShowLayer2();
                    break;
                case 2:
                    estCurrExerciseSet = (ExerciseSetType)idx;

                    if (estCurrExerciseSet == ExerciseSetType.RECORNITION)
                    {
                        iCurrLayer += 2;
                        ShowLayer4();
                    }
                    else
                    {
                        iCurrLayer++;
                        ShowLayer3();
                    }
                    break;
                case 3:
                    iCurrLayer++;
                    iCurrExercise = idx;
                    this.lbMainMenu.Visible = false;
                    ShowLayer4();
                    break;
            }

        }

        
        /// <summary>
        /// Khi có sự kiện nhấn phím vào Main Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            /************************************************************************/
            /* Nhấn phím Enter vô Main Menu                                         */
            /************************************************************************/
            int idx = this.lbMainMenu.SelectedIndex;

            switch(e.KeyCode)
            {
                /*******************/
                /* Nhấn phím Enter */
                /*******************/
                case Keys.Enter:
                    if (idx < 0)
                    {
                        /************************************************************************/
                        /* Speak "Bạn chưa chọn gì cả, hãy chọn một Option"                       */
                        /************************************************************************/



                        
                        break;
                    }
                    switch (iCurrLayer)
                    {
                        case 1:
                            iCurrLayer++;
                            tmCurrMode = (TypingMode)idx;
                            ShowLayer2();
                            break;
                        case 2:
                            estCurrExerciseSet = (ExerciseSetType)idx;

                            if (estCurrExerciseSet == ExerciseSetType.RECORNITION)
                            {
                                iCurrLayer += 2;
                                ShowLayer4();
                            }
                            else
                            {
                                iCurrLayer ++;
                                ShowLayer3();
                            }
                            
                            break;
                        case 3:
                            iCurrLayer++;
                            iCurrExercise = idx;
                            this.lbMainMenu.Visible = false;
                            ShowLayer4();
                            break;
                    }
                    break;

                /*******************/
                /* Nhấn phím Escape */
                /*******************/
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
                            if (estCurrExerciseSet == ExerciseSetType.RECORNITION)
                            {
                                iCurrLayer -= 2;
                                this.lbMainMenu.Visible = true;
                                ShowLayer2();
                            }
                            else
                            {
                                iCurrLayer--;
                                this.lbMainMenu.Visible = true;
                                ShowLayer3();
                            }
                            break;
                    }
                    break;
                
                
                /*******************/
                /* Nhấn phím F1    */
                /*******************/
                case Keys.F1:
                    /************************************************************************/
                    /* Giới thiệu về chương trình, hệ thống các bài tập...                  */
                    /************************************************************************/




                    
                    break;

                /*******************/
                /* Nhấn phím F2    */
                /*******************/
                case Keys.F2:
                    /************************************************************************/
                    /* Giới thiệu hệ thống các Hot Key sử dụng trong chương trình           */
                    /************************************************************************/







                    break;


                /*******************/
                /* Nhấn phím F3    */
                /*******************/
                case Keys.F3:
                    /************************************************************************/
                    /* Giới thiệu hệ thống các Hot Key sử dụng trong chương trình           */
                    /************************************************************************/





                    break;


                /*******************/
                /* Nhấn phím F4    */
                /*******************/
                case Keys.F4:
                    break;

            }
        }
    }
}