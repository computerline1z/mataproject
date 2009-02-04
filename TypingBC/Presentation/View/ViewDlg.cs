using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
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
        private bool isBlindView = false;
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
            try
            {
                StreamReader file = new StreamReader("..\\..\\Data\\TXT\\Help Layer 1.txt");
                strHelpString = file.ReadToEnd();
                file.Close();
                ShowHelp();
            }
            catch (System.Exception e)
            {
            	MessageBox.Show(e.Message);
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
            this.lbMainMenu.Items.Add("Bài tập gõ một kí tự không dấu");
            this.lbMainMenu.Items.Add("Bài tập gõ không dấu");
            
            if (tmCurrMode == TypingMode.NORMAL)
            {
                this.lbMainMenu.Items.Add("Bài tập gõ một kí tự có dấu kiểu gõ VNI");
                this.lbMainMenu.Items.Add("Bài tập gõ một kí tự có dấu kiểu gõ TELEX");
                this.lbMainMenu.Items.Add("Bài tập gõ có dấu kiểu gõ VNI");
                this.lbMainMenu.Items.Add("Bài tập gõ có dấu kiểu gõ TELEX");
            }
            else
            {
                this.lbMainMenu.Items.Add("Bài tập gõ một kí tự có dấu");
                this.lbMainMenu.Items.Add("Bài tập gõ có dấu");
            }
            
            
            /************************************************************************/
            /* Hướng dẫn cho người dùng về hề thống các bài tập                    */
            /************************************************************************/
            try
            {
                if (tmCurrMode == TypingMode.NORMAL)
                {
                    StreamReader file = new StreamReader("..\\..\\Data\\TXT\\Help Layer 21.txt");
                    strHelpString = file.ReadToEnd();
                    file.Close();
                    ShowHelp();
                }
                else
                {
                    StreamReader file = new StreamReader("..\\..\\Data\\TXT\\Help Layer 22.txt");
                    strHelpString = file.ReadToEnd();
                    file.Close();
                    ShowHelp();
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            ShowHelp();
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
            /* Hướng dẫn cho người dùng                                             */
            /************************************************************************/
            try
            {
                StreamReader file = new StreamReader("..\\..\\Data\\TXT\\Help Layer 3.txt");
                strHelpString = file.ReadToEnd();
                file.Close();
                ShowHelp();
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
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
            /* Hướng dẫn cho người dùng                                             */
            /************************************************************************/

            strHelpString = "Sau khi bạn gõ hết một câu thì câu khác sẽ hiện ra tiếp theo cho đến hết đoạn văn hoặc đoạn thơ.";
            ShowHelp();
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

            string strListHotKey = "F1: Giới thiệu chương trình\n" +
                                   "F2: Giới thiệu hệ thống các phím tắt\n" +
                                   "F3: Chuyển đổi giữa các chế độ: Blind và Normal\n" +
                                   "F4: Hướng dẫn gõ từ đang gõ\n" +
                                   "F9: Dừng đọc hướng dẫn\n" +
                                   "F10: Đọc lại hướng dẫn\n" +
                                   "F11: Giảm cỡ chữ\n" +
                                   "F12: Tăng cỡ chữ\n";
            
            this.notifyIcon.ShowBalloonTip(10000, "Hotkeys", strListHotKey, ToolTipIcon.Info);
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
                        case 1:
                            if (MessageBox.Show("Bạn có muốn thoát khỏi chương trình không?", "Thoát chương trình?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                this.Close();
                            }
                            break;

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
                    /* Chuyển đổi qua lại giữ các chế độ BlindView và ClearView             */
                    /************************************************************************/





                    break;


                /*******************/
                /* Nhấn phím F4    */
                /*******************/
                case Keys.F4:
                    /************************************************************************/
                    /* Hướng dẫn cách gõ từ đang gõ                                         */
                    /************************************************************************/




                    break;

                /*******************/
                /* Nhấn phím F9    */
                /*******************/
                case Keys.F9:
                    /************************************************************************/
                    /* Ngừng đọc hướng dẫn                                                  */
                    /************************************************************************/




                    break;


                /*******************/
                /* Nhấn phím F10    */
                /*******************/
                case Keys.F10:
                    /************************************************************************/
                    /* Đọc lại hướng dẫn                                                    */
                    /************************************************************************/




                    break;


                /*******************/
                /* Nhấn phím F11    */
                /*******************/
                case Keys.F11:
                    /************************************************************************/
                    /* Giảm kích cỡ chữ                                                     */
                    /************************************************************************/




                    break;


                /*******************/
                /* Nhấn phím F12    */
                /*******************/
                case Keys.F12:
                    /************************************************************************/
                    /* Tăng kích cỡ chữ                                                     */
                    /************************************************************************/




                    break;

            }
        }


        /// <summary>
        /// Hàm này thực hiện việc giúp đỡ người dùng bằng 2 cách
        ///     *** Speak đối với BlindView
        ///     *** Show trên labelHint đối với ClearView
        /// </summary>
        private void ShowHelp()
        {
            if (isBlindView)
            {
                /************************************************************************/
                /* Đối với người mù thì đọc tất cả thông tin hướng dẫn ở đây            */
                /************************************************************************/
                MessageBox.Show(strHelpString, "Speaking...");





            }
            else
            {
                /************************************************************************/
                /* Đối với người người thấy đường thì hiển thị thông tin hướng dẫn      */
                /************************************************************************/
                this.rtbHint.Text = strHelpString;

            }
        }

        /// <summary>
        /// Xử lý khi người dùng thay đổi các Option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMainMenu_SelectedValueChanged(object sender, EventArgs e)
        {
            /************************************************************************/
            /* Hướng dẫn người dùng các Option                                      */
            /************************************************************************/

            strHelpString = (string)this.lbMainMenu.SelectedItem;

            if (isBlindView)
            {
                MessageBox.Show(strHelpString, "Speaking...");
            }
            else
            {
                this.rtbHint.Text = strHelpString;
            }
        }
    }
}