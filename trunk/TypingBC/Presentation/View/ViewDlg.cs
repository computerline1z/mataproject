using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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

        /// <summary>
        /// Những thuộc tính mới được thêm vào
        /// </summary>
        private Presentation.Model.CTypingModel curModel = new Presentation.Model.CTypingModel(); // mô hình đang dùng
        private string strNeedTypingText = "";  //chuỗi text cần gõ
        private string strCurWord = "";         // tu hien can go
        private int iCharInWord = 0;            // so thu tu cua chu trong tu hien can go
        private int iCharNumInWord = 0;         // so chu trong tu can go
        private string strTypedText = "";       // chuỗi text đã được gõ đúng
        private bool bEndstring = true;         // ket thuc chuoi
        /// <summary>
        /// Phần các biến toàn cục xử dụng trong chế độ Braille
        /// </summary>
        private List<char> keyPressed = new List<char>();
        private bool isTimerStarted = false;
        private bool isUserPressed = true;


        #endregion

        public ViewDlg()
        {
            InitializeComponent();

            /************************************************************************/
            /* Fix Clock Interval to 30 miliseconds                                 */
            /************************************************************************/
            Clock.Interval = 30;
            Clock.Tick += new EventHandler(Timer_Click);
            
            /************************************************************************/
            /* Start Position                                                       */
            /************************************************************************/
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
            DialogResult dlgRes = dlg.ShowDialog();
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

            // Chọn lại Option cũ
            this.lbMainMenu.SelectedIndex = (int)tmCurrMode;
            this.lbMainMenu.Focus();

            /************************************************************************/
            /* Hướng dẫn cho người dùng về 2 chế độ gõ là như thế nào               */
            /************************************************************************/
            try
            {
                ShowHelp(10);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Lỗi show layer 1");
            }

        }


        /// <summary>
        /// Chọn hệ thống bài tập (tùy theo chế độ gõ hiện tại)
        /// </summary>
        private void ShowLayer2()
        {
            HideLayer4();
            this.lbMainMenu.Items.Clear();
            this.groupBoxWorkSpace.Text = "Menu";
            this.lbMainMenu.Focus();

            // Đưa các option vào
            this.lbMainMenu.Items.Add("Nhận diện bàn phím");
            this.lbMainMenu.Items.Add("Bài tập gõ một kí tự không dấu");
            this.lbMainMenu.Items.Add("Bài tập gõ không dấu");

            if (tmCurrMode == TypingMode.NORMAL)
            {
                this.lbMainMenu.Items.Add("Bài tập gõ một kí tự có dấu kiểu VNI");
                this.lbMainMenu.Items.Add("Bài tập gõ một kí tự có dấu kiểu TELEX");
                this.lbMainMenu.Items.Add("Bài tập gõ có dấu kiểu VNI");
                this.lbMainMenu.Items.Add("Bài tập gõ có dấu kiểu TELEX");
            }
            else
            {
                this.lbMainMenu.Items.Add("Bài tập gõ một kí tự có dấu");
                this.lbMainMenu.Items.Add("Bài tập gõ có dấu");
            }

            // Chọn lại Option cũ
            this.lbMainMenu.SelectedIndex = (int)estCurrExerciseSet;


            /************************************************************************/
            /* Hướng dẫn cho người dùng về hề thống các bài tập                    */
            /************************************************************************/
            try
            {
                if (tmCurrMode == TypingMode.NORMAL)
                {
                    ShowHelp(201);
                }
                else
                {
                    ShowHelp(202);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Lỗi show layer 2");
            }
        }

        /// <summary>
        /// Chọn một bài tập luyện tập (tùy thuộc vào ExerciseSet hiện tại)
        /// </summary>
        private void ShowLayer3()
        {
            HideLayer4();
            this.lbMainMenu.Items.Clear();
            this.groupBoxWorkSpace.Text = "Menu";
            this.lbMainMenu.Focus();

            // Đưa các option vào
            int nSoBaiTap = 10;
            for (int i = 0; i < nSoBaiTap; i++)
            {
                this.lbMainMenu.Items.Add("Bài tập " + (i + 1).ToString());
            }

            // Chọn lại Option cũ
            this.lbMainMenu.SelectedIndex = iCurrExercise;


            /************************************************************************/
            /* Hướng dẫn cho người dùng                                             */
            /************************************************************************/
            try
            {
                ShowHelp(30);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Lỗi show layer 3");
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
            this.groupBoxWorkSpace.Text = "Tập gõ ở đây";

            /************************************************************************/
            /* Hiển thị giao diện vùng cửa sổ xử lý bài tập                         */
            /************************************************************************/
            this.layer4LabelString.Visible = true;
            this.layer4RTBVungTapGo.Visible = true;
            this.layer4RTBVungTapGo.Focus();

            /************************************************************************/
            /* Đối với loại bài tập nhận diện bàn phím thì dễ rồi, không cần load   */
            /************************************************************************/
            if (estCurrExerciseSet == ExerciseSetType.RECORNITION)
            {
                try
                {
                    ShowHelp(40);
                    return;

                }
                catch (System.Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi show layer 4");
                }
            }
            

            /****************************************************************************/
            /*  Đối với các loại bài tập khác                                           */
            /****************************************************************************/
            curModel.UserManager.SetUsingExercise(strCurrentUser, iCurrExercise);
            curModel.UserManager.SetUsingTypingMode(strCurrentUser, tmCurrMode);

            /****************************************************************************/
            /*  Lay chua dung loai bai tap, phai la iCurrExercise + 1                   */
            /****************************************************************************/
            if (curModel.LoadExercise(iCurrExercise + 1))
            {
                strNeedTypingText = curModel.GetNextString();
                strNeedTypingText = TypingBC.Business.CConverter.ConvertStrWithMode(strNeedTypingText, (int)estCurrExerciseSet - 1);

                if (strNeedTypingText == "")
                {
                    MessageBox.Show("Error when load and convert a string!");
                }
                else
                {
                    this.layer4LabelString.Text = strNeedTypingText;
                    this.layer4LabelString.Update();
                }
            }
            else
            {
                /*********************************************/
                // hien tai chuong trinh luon nhay vao cho nay, vi ham curModel.LoadExercise luon tra ra gia tri null khi user co gia tri cu
                /*********************************************/
                MessageBox.Show("Error when load exercise!");
            }

            
            /************************************************************************/
            /* Hướng dẫn cho người dùng                                             */
            /************************************************************************/
            ShowHelp(41);
        }


        /// <summary>
        /// Ẩn layer 4
        /// </summary>
        private void HideLayer4()
        {
            this.layer4LabelString.Visible = false;
            this.layer4RTBVungTapGo.Visible = false;
            resetProcessProperties();
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
            

            // cần lấy thông tin từ dialog về curModel

            /************************************************************************/
            /* Nếu UserName đã tồn tại thì load vô mục bài tập đang làm             */
            /************************************************************************/
            if (curModel.UserManager.IsUserExisted(strCurrentUser))
            {
                iCurrExercise = curModel.UserManager.GetUsingExercise(strCurrentUser);
                tmCurrMode = curModel.UserManager.GetUsingTypingMode(strCurrentUser);

                iCurrLayer = 4;
                ShowLayer4();
            }
            else
            {
                // User mới, khởi tạo thông số mặc định
                iCurrExercise = 0;
                tmCurrMode = TypingMode.NORMAL;
            }

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
            string strHelpString;

            switch (e.KeyCode)
            {
                /*******************/
                /* Nhấn phím Enter */
                /*******************/
                case Keys.Enter:
                    if (idx < 0 && this.lbMainMenu.Visible == true)
                    {
                        /************************************************************************/
                        /* Speak "Bạn chưa chọn gì cả, hãy chọn một Option"                       */
                        /************************************************************************/
                        strHelpString = "Bạn chưa chọn lựa chọn nào cả. Hãy chọn và nhấn phím Enter hoặc nhấn nhanh chuột trái 2 lần liên tiếp";
                        ShowHelp(strHelpString);



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
                    break;

                /*******************/
                /* Nhấn phím Escape */
                /*******************/
                case Keys.Escape:
                    switch (iCurrLayer)
                    {
                        case 1:
                            if (MessageBox.Show("Bạn có muốn thoát chương trình không?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                DataAccess.CPersistantData.Instance.SaveData();
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

                            resetProcessProperties();
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
                    strHelpString = "Phím F1\n";
                    StreamReader file1 = new StreamReader("Data\\TXT\\Help.txt");
                    strHelpString += file1.ReadToEnd();
                    file1.Close();
                    ShowHelp(strHelpString);

                    break;

                /*******************/
                /* Nhấn phím F2    */
                /*******************/
                case Keys.F2:
                    /************************************************************************/
                    /* Giới thiệu hệ thống các Hot Key sử dụng trong chương trình           */
                    /************************************************************************/
                    strHelpString = "Phím F2\n";
                    StreamReader file2 = new StreamReader("Data\\TXT\\Help Hot Key.txt");
                    strHelpString += file2.ReadToEnd();
                    file2.Close();
                    ShowHelp(strHelpString);


                    break;


                /*******************/
                /* Nhấn phím F3    */
                /*******************/
                case Keys.F3:
                    /************************************************************************/
                    /* Chuyển đổi qua lại giữ các chế độ BlindView và ClearView             */
                    /************************************************************************/
                    strHelpString = "Phím F3 : chuyển đổi qua lại giữa hai chế độ giao diện\n";
                    ShowHelp(strHelpString);
                    isBlindView = !isBlindView;

                    if (isBlindView)
                    {
                        this.Width -= Business.CConfig.iDeltaWidth;
                    }
                    else
                    {
                        this.Width += Business.CConfig.iDeltaWidth;
                    }
                    break;


                /*******************/
                /* Nhấn phím F4    */
                /*******************/
                case Keys.F4:
                    /************************************************************************/
                    /* Hướng dẫn cách gõ từ đang gõ                                         */
                    /************************************************************************/
                    strHelpString = "Phím F4: hướng dẫn cách gõ từ đang gõ. Sử dụng khi bạn không biết hoặc quên mất cách gõ.\n";
                    ShowHelp(strHelpString);

                    break;

                /*******************/
                /* Nhấn phím F9    */
                /*******************/
                case Keys.F9:
                    /************************************************************************/
                    /* Ngừng đọc hướng dẫn                                                  */
                    /************************************************************************/
                    strHelpString = "Phím F9: tắt chế độ đọc hướng dẫn sử dụng.\n";
                    ShowHelp(strHelpString);

                    break;


                /*******************/
                /* Nhấn phím F10    */
                /*******************/
                case Keys.F10:
                    /************************************************************************/
                    /* Đọc lại hướng dẫn                                                    */
                    /************************************************************************/
                    strHelpString = "Phím F10: bật chế độc đọc hướng dẫn sử dụng.\n";
                    ShowHelp(strHelpString);

                    break;


                /*******************/
                /* Nhấn phím F11    */
                /*******************/
                case Keys.F11:
                    /************************************************************************/
                    /* Giảm kích cỡ chữ                                                     */
                    /************************************************************************/
                    strHelpString = "Phím F11: giảm kích cỡ font chữ trên ứng dụng.\n";
                    ShowHelp(strHelpString);
                    
                    try
                    {
                        this.rtbHint.ZoomFactor -= (float)0.1;
                        this.layer4RTBVungTapGo.ZoomFactor -= (float)0.1;
                        this.layer4LabelString.Font = ChangeFontSize(this.layer4LabelString.Font, 0.9F * this.layer4LabelString.Font.Size);
                        this.lbMainMenu.Font = ChangeFontSize(this.lbMainMenu.Font, 0.9F * (this.lbMainMenu.Font.Size));
                        ChangeHeight(-Business.CConfig.iDeltaHeight);
                    }
                    catch (System.Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }

                    break;


                /*******************/
                /* Nhấn phím F12    */
                /*******************/
                case Keys.F12:
                    /************************************************************************/
                    /* Tăng kích cỡ chữ                                                     */
                    /************************************************************************/
                    strHelpString = "Phím F12: tăng kích cỡ font chữ trên ứng dụng.\n";
                    ShowHelp(strHelpString);

                    try
                    {
                        this.rtbHint.ZoomFactor += (float)0.1;
                        this.layer4RTBVungTapGo.ZoomFactor += (float)0.1;
                        this.layer4LabelString.Font = ChangeFontSize(this.layer4LabelString.Font, 1.1F * this.layer4LabelString.Font.Size);
                        this.lbMainMenu.Font = ChangeFontSize(this.lbMainMenu.Font, 1.1F * this.lbMainMenu.Font.Size);
                        ChangeHeight(Business.CConfig.iDeltaHeight);
                    }
                    catch (System.Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }


                    break;

            }
        }

        /// <summary>
        /// Thay đổi kích cỡ font chữ
        /// </summary>
        /// <param name="font"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        private Font ChangeFontSize(Font font, float fontSize)
        {
            if (font != null)
            {
                float currentSize = font.Size;
                if (currentSize != fontSize)
                {
                    font = new Font(font.Name, fontSize,
                        font.Style, font.Unit,
                        font.GdiCharSet, font.GdiVerticalFont);
                }
            }
            return font;
        }

        /// <summary>
        /// Hàm này thực hiện việc giúp đỡ người dùng bằng 2 cách
        ///     *** Speak đối với BlindView
        ///     *** Show trên labelHint đối với ClearView
        /// </summary>
        private void ShowHelp(string str)
        {
            if (isBlindView)
            {
                /************************************************************************/
                /* Đối với người mù thì đọc tất cả thông tin hướng dẫn ở đây            */
                /************************************************************************/
                MessageBox.Show(str, "Speaking...");

            }
            else
            {
                /************************************************************************/
                /* Đối với người người thấy đường thì hiển thị thông tin hướng dẫn      */
                /************************************************************************/
                this.rtbHint.Text = str;

            }
        }

        /// <summary>
        /// Hàm này thực hiện việc giúp đỡ người dùng bằng 2 cách
        ///     *** Speak đối với BlindView
        ///     *** Show trên labelHint đối với ClearView
        /// </summary>
        private void ShowHelp(int id)
        {
            if (isBlindView)
            {
                /************************************************************************/
                /* Đối với người mù thì đọc tất cả thông tin hướng dẫn ở đây            */
                /************************************************************************/
                Business.CSpeech.ReadString(id);
                
            }
            else
            {
                /************************************************************************/
                /* Đối với người người thấy đường thì hiển thị thông tin hướng dẫn      */
                /************************************************************************/
                this.rtbHint.Text = DataAccess.CPersistantData.Instance.GetSpeechEntry(id, false);

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

            string strHelpString = (string)this.lbMainMenu.SelectedItem;

            if (isBlindView)
            {
                // Get ID
                int id = iCurrLayer;
                id = id * 10 + this.lbMainMenu.SelectedIndex + 1;
                if (iCurrLayer == 2)
                    id = id * 10 + (int)tmCurrMode + 1;
                if (iCurrLayer == 3)
                {
                    ShowHelp(strHelpString);
                }
                else
                {
                    Business.CSpeech.ReadString(id);
                }
            }
            else
            {
                this.rtbHint.Text = strHelpString;
            }
        }

        /// <summary>
        /// Thay đổi một số kích thướng của form, các control
        /// </summary>
        /// <param name="delta"></param>
        private void ChangeHeight(int delta)
        {
            this.Height += delta/2;
            /*this.lbMainMenu.Height += delta;
            this.layer4RTBVungTapGo.Height += delta;
            this.groupBox1.Height += delta;
            this.rtbHint.Height += delta;*/
            this.lbMainMenu.Top += delta;
            this.layer4RTBVungTapGo.Top += delta;
            this.groupBoxHint.Top += delta/2;
            this.groupBoxWorkSpace.Top += delta/2;

        }

        /// <summary>
        /// Xử lý khi có sự kiện Text Changed trong Vùng Tập Gõ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layer4RTBVungTapGo_TextChanged(object sender, EventArgs e)
        {
            int len = this.layer4RTBVungTapGo.Text.Length;
            
            switch (tmCurrMode)
            {
                /************************************************************************/
                /*  Xử lý cho chế độ gõ Normal                                          */
                /************************************************************************/
                case TypingMode.NORMAL:
                    switch (estCurrExerciseSet)
                    {
                        case ExerciseSetType.RECORNITION:
                            string str = (len >= 2 ? this.layer4RTBVungTapGo.Text[len - 1].ToString() : this.layer4RTBVungTapGo.Text);
                            this.layer4LabelString.Text = "Kí tự vừa gõ là:  " + str;
                            this.rtbHint.Text = str;
                            this.layer4RTBVungTapGo.Clear();
                            layer4RTBVungTapGo.AppendText(str);
                            break;
                        default:
                            ProcessLayer4();
                            break;
                    }
                    break;


                /************************************************************************/
                /*  Xử lý cho chế độ gõ Braille                                         */
                /************************************************************************/
                case TypingMode.BRAILLE:

                    if (!isUserPressed)
                        return;
                    if (len == 0)
                        return;

                    char c = this.layer4RTBVungTapGo.Text[len - 1];
                    if (isTimerStarted)
                    {
                        keyPressed.Add(c);
                    }
                    else
                    {
                        isTimerStarted = true;
                        Clock.Start();
                        keyPressed.Add(c);
                    }
                    break;
            }
        }

        /// <summary>
        /// Đây là phần xử lý của Long đưa ra một Methods riêng nha
        /// Ở đây sẽ tập trung xử lý chuỗi, kiểm tra xem người dùng gõ có đúng ko?
        /// </summary>
        private void ProcessLayer4()
        {
            // chua ho tro backspace!!
            layer4RTBVungTapGo.Update();
            string curText = layer4RTBVungTapGo.Text;
            int curTextLen = curText.Length;

            if (curTextLen == 0)
            {
                //MessageBox.Show("nothing in text box");
                return;
            }

            char t = curText[curTextLen - 1];
             
            if ((t == ' ') || (t == '\n') || (t == '\t'))
            {
                string[] temp_string = layer4RTBVungTapGo.Text.TrimEnd('\n', ' ', '\t').Split('\n', ' ', '\t');
                int tempLen = temp_string.Length;
                // kiểm tra chuỗi bằng nhau ở đây
                if (temp_string[tempLen - 1] == strCurWord)
                {
                    // thuc hien tinh do chinh xac
                    //MessageBox.Show("Right!");
                    strTypedText += temp_string[tempLen - 1];
                    strTypedText += t;
                    layer4RTBVungTapGo.Update();

                    if (bEndstring)
                    {
                        layer4LabelString.Text = strNeedTypingText;
                    }
                }
                else
                {
                    // keu "bip bip" de  bao loi
                    // MessageBox.Show("False!");

                    // khoi tao lai ban dau
                    iCharInWord = 0;
                    iCharNumInWord = 0;
                    strNeedTypingText = strCurWord + t + strNeedTypingText;

                    layer4RTBVungTapGo.Clear();
                    layer4RTBVungTapGo.AppendText(strTypedText);
                    layer4RTBVungTapGo.Update();

                    return;
                }
                //MessageBox.Show("Check accuracy!");
            }

            iCharInWord++;

            if (iCharInWord > iCharNumInWord)
            {
                string[] tempNeedTypingWords = strNeedTypingText.Split('\n', ' ', '\t');
                if (tempNeedTypingWords.Length == 1)
                {
                    // chi con mot tu thi lay chuoi ke:
                    string tempstr = TypingBC.Business.CConverter.ConvertStrWithMode(curModel.GetNextString(), (int)estCurrExerciseSet - 1);

                    if (tempstr == string.Empty)
                    {
                        strNeedTypingText = tempstr;
                    }
                    else
                    {
                        strNeedTypingText += tempstr;
                    }

                    bEndstring = true;
                }

                strCurWord = tempNeedTypingWords[0];
                
                // Neu khong lay duoc chuoi ke thi ket thuc bai tap
 
                iCharNumInWord = strCurWord.Length;
                iCharInWord = 0;

                if (strNeedTypingText != string.Empty)
                {
                    strNeedTypingText = strNeedTypingText.Substring(tempNeedTypingWords[0].Length);
                    strNeedTypingText = strNeedTypingText.TrimStart('\n', ' ', '\t');
                }
            }

            if (strNeedTypingText == string.Empty)
            {
                if (iCharNumInWord - 1 == iCharInWord)
                {
                    // ket thuc bai tap sau khi go xong tu cuoi

                    string[] temp_string = layer4RTBVungTapGo.Text.TrimEnd('\n', ' ', '\t').Split('\n', ' ', '\t');
                    int tempLen = temp_string.Length;
                    // kiểm tra chuỗi bằng nhau ở đây
                    if (temp_string[tempLen - 1] == strCurWord)
                    {
                        // thuc hien tinh do chinh xac
                        //MessageBox.Show("Right!");
                        strTypedText += temp_string[tempLen - 1];
                        strTypedText += t;
                        layer4RTBVungTapGo.Update();
                    }
                    else
                    {
                        // keu "bip bip" de  bao loi
                        // MessageBox.Show("False!");

                        // khoi tao lai ban dau
                        iCharInWord = 0;
                        iCharNumInWord = 0;
                        strNeedTypingText = strCurWord;

                        layer4RTBVungTapGo.Clear();
                        layer4RTBVungTapGo.AppendText(strTypedText);
                        layer4RTBVungTapGo.Update();

                        return;
                    }

                    MessageBox.Show("Exercise completed!");
                    layer4RTBVungTapGo.Clear();
                    
                    // thoat ra layer 3
                    iCurrLayer--;
                    this.lbMainMenu.Visible = true;
                    ShowLayer3();
     
                    // set lai thuoc tinh
                    resetProcessProperties();

                    return;
                }
            }

        }

        /// <summary>
        /// Set lai mot so thuoc tinh su dung trong form
        /// </summary>
        private void resetProcessProperties()
        {
            strNeedTypingText = "";
            strCurWord = "";
            iCharInWord = 0;
            iCharNumInWord = 0;
            strTypedText = "";
            bEndstring = true;
            layer4LabelString.Text = "";
            layer4RTBVungTapGo.Clear();
        }

        
        /// <summary>
        /// Timer_Tik event cho đồng hồ đếm giờ của bộ gõ 6 phím Braille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Click(Object sender, EventArgs e)
        {
            Clock.Stop();
            isTimerStarted = false;

            if (estCurrExerciseSet == ExerciseSetType.RECORNITION)
            {
                Business.CBrailleMode BrailleMode = new Business.CBrailleMode();
                string str = BrailleMode.ConvertToChar(keyPressed).ToString();
                keyPressed.Clear();
                layer4LabelString.Text = "Kí tự vừa gõ là: " + str;
                isUserPressed = false;
                layer4RTBVungTapGo.Clear();
                layer4RTBVungTapGo.AppendText(str);
                isUserPressed = true;
                //ShowHelp(str);
            }
        }
    }
}