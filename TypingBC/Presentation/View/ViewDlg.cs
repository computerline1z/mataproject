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
        private Business.CHelp hHelper = new Business.CHelp();
        private static int WM_HOTKEY = 0x0312;

        /// <summary>
        /// Những thuộc tính mới được thêm vào
        /// </summary>
        private Presentation.Model.CTypingModel curModel = new Presentation.Model.CTypingModel(); // mô hình đang dùng
        private string strNeedTypingText = "";  //chuỗi text cần gõ
        private string strNeedShowedtext = "";  // chuỗi text cần show
        private string strCurWord = "";         // tu hien can go
        private int iCharInWord = 0;            // so thu tu cua chu trong tu hien can go
        private int iCharNumInWord = 0;         // so chu trong tu can go
        private string strTypedText = "";       // chuỗi text đã được gõ đúng
        private bool bEndstring = true;         // ket thuc chuoi
        private string strUserTypedText = "";
        private int iWrongType = 0;
        /// <summary>
        /// Phần các biến toàn cục xử dụng trong chế độ Braille
        /// </summary>
        private List<char> keyPressed = new List<char>();
        private bool isTimerStarted = false;
        private bool isUserPressed = true;

        /// <summary>
        /// for Braille Mode
        /// </summary>
        string[] arrStrListWord;
        int iCurWord = 0;
        int iLenTypingWord = 0;
        int iTimesError = 0;

        [Flags]
        public enum SystemKeys : uint
        {
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        }

        #endregion

        #region Imports

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion
        
        #region Constructor - Form Closed
        
        public ViewDlg()
        {
            InitializeComponent();

            /************************************************************************/
            /* Đăng ký hot key                                                      */
            /************************************************************************/
            bool Success = RegisterHotKey(this.Handle, this.GetType().GetHashCode(),
                (uint)(SystemKeys.Control), (uint)Keys.Space);
            if (!Success)
                MessageBox.Show("Register Ctrl + Space Unsuccessfully!");

            /************************************************************************/
            /* Fix Clock Interval to 50 miliseconds                                 */
            /************************************************************************/
            Clock.Interval = Business.CConfig.iInterval;
            Clock.Tick += new EventHandler(Timer_Click);

            /************************************************************************/
            /* Start Position                                                       */
            /************************************************************************/
            this.StartPosition = FormStartPosition.CenterScreen;

            ShowLayer0();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            DataAccess.CPersistantData.Instance.SetUsingExID(strCurrentUser, iCurrExercise);
            curModel.UserManager.SetUsingExercise(strCurrentUser, ComputeExID(tmCurrMode, estCurrExerciseSet, iCurrExercise));
            DataAccess.CPersistantData.Instance.SaveData();
        }

        #endregion
        
        #region Window Proc

        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == WM_HOTKEY)
            {
                // get the keys.
                Keys key = (Keys)(((int)msg.LParam >> 16) & 0xFFFF);
                SystemKeys modifier = (SystemKeys)((int)msg.LParam & 0xFFFF);

                if (modifier == SystemKeys.Control && key == Keys.Space)
                    this.Activate();
            }
            base.WndProc(ref msg);
        }
        #endregion

        #region Shows
      
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
            CExerciseSet[] arrExSet = curModel.GetExSetList(tmCurrMode);
            foreach (CExerciseSet ExSet in arrExSet)
                this.lbMainMenu.Items.Add(ExSet.ExSetName);

            // Chọn lại Option cũ
            this.lbMainMenu.SelectedIndex = (int)estCurrExerciseSet % 10;


            /************************************************************************/
            /* Hướng dẫn cho người dùng về hề thống các bài tập                    */
            /************************************************************************/
            try
            {
                if (tmCurrMode == TypingMode.NORMAL)
                    ShowHelp(201);
                else
                    ShowHelp(202);
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

            // Load hết số bài tập lên
            CExercise[] arrExList = curModel.GetExerciseList(estCurrExerciseSet);
            foreach (CExercise Ex in arrExList)
                this.lbMainMenu.Items.Add(Ex.ExName);

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
            if (estCurrExerciseSet == ExerciseSetType.RECORNITION ||
                estCurrExerciseSet == ExerciseSetType.RECORNITION_BRAILLE)
            {
                ShowHelp(40);
                return;
            }


            /****************************************************************************/
            /*  Đối với các loại bài tập khác                                           */
            /****************************************************************************/
            curModel.UserManager.SetUsingExercise(strCurrentUser, iCurrExercise);
            curModel.UserManager.SetUsingTypingMode(strCurrentUser, tmCurrMode);

            /****************************************************************************/
            /*  Lay chua dung loai bai tap, phai la iCurrExercise + 1  ????             */
            /****************************************************************************/
            int ExID = ComputeExID(tmCurrMode, estCurrExerciseSet, iCurrExercise);
            if (curModel.LoadExercise(ExID))
            {
                strNeedTypingText = curModel.GetNextString();
                strNeedShowedtext = strNeedTypingText;
                if (strNeedTypingText == "")
                {
                    MessageBox.Show("Error when load and convert a string!");
                }
                else
                {
                    strNeedTypingText = Business.CConverter.ConvertStrWithMode(strNeedTypingText, estCurrExerciseSet);
                    this.layer4LabelString.Text = strNeedShowedtext + '\n' + strNeedTypingText;
                    this.layer4LabelString.Update();
                    if ((int)estCurrExerciseSet / 10 > 0)
                        arrStrListWord = strNeedTypingText.Split(' ', '\t', '\n');
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
                                   "F5: Tạm ngừng đọc hướng dẫn\n" +
                                   "F9: Giảm thời gian chờ giữa các phím Braille 10 mili giây\n" +
                                   "F9: Tăng thời gian chờ giữa các phím Braille 10 mili giây\n" +
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
                tmCurrMode = curModel.UserManager.GetUsingTypingMode(strCurrentUser);
                estCurrExerciseSet = curModel.UserManager.GetUsingExerciseSet(strCurrentUser);
                if (estCurrExerciseSet == ExerciseSetType.RECORNITION||
                    estCurrExerciseSet == ExerciseSetType.RECORNITION_BRAILLE)
                {
                    iCurrLayer = 4;
                    ShowLayer4();
                }
                else
                {
                    iCurrExercise = curModel.UserManager.GetUsingExercise(strCurrentUser);
                    iCurrExercise = DecodeExIndex(iCurrExercise);
                    iCurrLayer = 3;
                    ShowLayer3();
                }
            }
            else
            {
                // User mới, khởi tạo thông số mặc định
                tmCurrMode = TypingMode.NORMAL;
                iCurrExercise = 0;
                if (!curModel.UserManager.AddUser(strCurrentUser))
                    MessageBox.Show("Cannot add user", "Error");
                iCurrLayer = 1;
                ShowLayer1();
            }

            ShowListHotKeyBalloon();
        }

        #endregion

        #region Events
 
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
                    estCurrExerciseSet = (ExerciseSetType)(10 * (int)tmCurrMode + idx);

                    if (estCurrExerciseSet == ExerciseSetType.RECORNITION ||
                        estCurrExerciseSet == ExerciseSetType.RECORNITION_BRAILLE)
                    {
                        iCurrLayer += 2;
                        iCurrExercise = ((int)estCurrExerciseSet == 0 ? -1 : -2);
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
                            estCurrExerciseSet = (ExerciseSetType)(10 * (int)tmCurrMode + idx);

                            if (estCurrExerciseSet == ExerciseSetType.RECORNITION ||
                                estCurrExerciseSet == ExerciseSetType.RECORNITION_BRAILLE)
                            {
                                iCurrLayer += 2;
                                iCurrExercise = ((int)estCurrExerciseSet == 0 ? -1 : -2);
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
                                this.Close();
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
                            if (estCurrExerciseSet == ExerciseSetType.RECORNITION ||
                                estCurrExerciseSet == ExerciseSetType.RECORNITION_BRAILLE)
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
                    ShowHelp(51);

                    break;

                /*******************/
                /* Nhấn phím F2    */
                /*******************/
                case Keys.F2:
                    /************************************************************************/
                    /* Giới thiệu hệ thống các Hot Key sử dụng trong chương trình           */
                    /************************************************************************/
                    ShowHelp(52);
                    break;


                /*******************/
                /* Nhấn phím F3    */
                /*******************/
                case Keys.F3:
                    /************************************************************************/
                    /* Chuyển đổi qua lại giữ các chế độ BlindView và ClearView             */
                    /************************************************************************/
                    ShowHelp(53);
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
                    /* Tạm ngừng đọc hướng dẫn                                              */
                    /************************************************************************/
                    ShowHelp(54);

                    break;
                case Keys.F5:
                    /************************************************************************/
                    /* Hướng dẫn cách gõ từ đang gõ                                         */
                    /************************************************************************/
                    ShowHelp(55);

                    break;

                /*******************/
                /* Nhấn phím F9    */
                /*******************/
                case Keys.F9:
                    /************************************************************************/
                    /* Giảm thời gian chờ giữa các phím Braille 10 mili giây                */
                    /************************************************************************/
                    try
                    {
                        Clock.Interval -= 10;
                    }
                    catch { }

                    ShowHelp(59);

                    break;


                /*******************/
                /* Nhấn phím F10    */
                /*******************/
                case Keys.F10:
                    /************************************************************************/
                    /* Tăng thời gian chờ giữa các phím Braille 10 mili giây                */
                    /************************************************************************/
                    try
                    {
                        Clock.Interval += 10;
                    }
                    catch { }
                    ShowHelp(510);
                    break;


                /*******************/
                /* Nhấn phím F11    */
                /*******************/
                case Keys.F11:
                    /************************************************************************/
                    /* Giảm kích cỡ chữ                                                     */
                    /************************************************************************/
                    ShowHelp(511);

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
                    ShowHelp(512);

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
        /// Timer_Tik event cho đồng hồ đếm giờ của bộ gõ 6 phím Braille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Click(Object sender, EventArgs e)
        {
            int len = keyPressed.Count;
            Clock.Stop();
            isTimerStarted = false;

            Business.CBrailleMode BrailleMode = new Business.CBrailleMode();
            char c = BrailleMode.ConvertToChar(keyPressed);

            if (c != '\0')
                iLenTypingWord++;

            /************************************************************************/
            /*  Đối với bài tập nhận diện bàn phím                                  */
            /************************************************************************/
            if (estCurrExerciseSet == ExerciseSetType.RECORNITION ||
                estCurrExerciseSet == ExerciseSetType.RECORNITION_BRAILLE)
            {
                if (c != '\0')
                    layer4LabelString.Text = "Kí tự vừa gõ là: " + c.ToString();
                else
                    layer4LabelString.Text = "Phím không hợp lệ";
                isUserPressed = false;
                layer4RTBVungTapGo.Clear();
                layer4RTBVungTapGo.AppendText(c.ToString());
                isUserPressed = true;
                //ShowHelp(c.ToString());
            }
            /************************************************************************/
            /*  Đối với các dạng bài tập khác                                       */
            /************************************************************************/
            else
            {
                // Đưa vào chuỗi từ mới thêm.
                // Kiểm tra kí tự vừa gõ có phải là kí tự trắng không?
                // Nếu là kí tự trắng
                //      Lấy từ hiện tại
                //      Kiểm tra từ hiện tại với từ tiếp theo có giống nhau không?
                //          Nếu giống thì dừng
                //          Nếu khác thì xóa từ đã gõ
                isUserPressed = false;
                this.layer4RTBVungTapGo.Undo();
                this.layer4RTBVungTapGo.AppendText(c.ToString());
                isUserPressed = true;

                if (keyPressed[len - 1] == (char)Keys.Space ||
                    keyPressed[len - 1] == (char)Keys.Enter ||
                    keyPressed[len - 1] == (char)Keys.Tab)
                {
                    string temp = this.layer4RTBVungTapGo.Text;
                    temp = temp.TrimEnd(' ', '\n', '\t');
                    string[] tempstr = temp.Split(' ', '\n', '\t');
                    string curWord = tempstr[tempstr.Length - 1];
                    if (curWord == arrStrListWord[iCurWord] && tempstr.Length == iCurWord + 1)
                    {
                        this.isUserPressed = false;
                        this.layer4RTBVungTapGo.AppendText(keyPressed[len - 1].ToString());
                        this.isUserPressed = true;
                        iLenTypingWord = 0;
                        iTimesError = 0;


                        if (iCurWord + 1 == arrStrListWord.Length)
                        {
                            strNeedShowedtext = curModel.GetNextString();
                            if (strNeedShowedtext == "")
                            {
                                MessageBox.Show("Hoàn thành bài tập");
                                // Quay trở lại
                                iCurrLayer--;
                                this.lbMainMenu.Visible = true;
                                ShowLayer3();
                            }
                            else
                            {
                                this.layer4RTBVungTapGo.Clear();
                                strNeedTypingText = Business.CConverter.ConvertStrWithMode(strNeedShowedtext, estCurrExerciseSet);
                                arrStrListWord = strNeedTypingText.Split(' ', '\t', '\n');
                                iCurWord = 0;
                                layer4LabelString.Text = strNeedShowedtext + "\n";
                                for (int i = iCurWord; i < arrStrListWord.Length; i++)
                                    layer4LabelString.Text += arrStrListWord[i] + " ";
                            }
                        }
                        else
                        {
                            layer4LabelString.Text = strNeedShowedtext + "\n";
                            iCurWord++;
                            for (int i = iCurWord; i < arrStrListWord.Length; i++)
                                layer4LabelString.Text += arrStrListWord[i] + " ";
                        }
                    }
                    else
                    {
                        this.isUserPressed = false;
                        for (int i = 0; i < iLenTypingWord; i++)
                            this.layer4RTBVungTapGo.Undo();

                        this.isUserPressed = true;
                        iTimesError++;
                        if (iTimesError >= 3)
                        {
                            string strHelpString = hHelper.HowToTypeWord_str(arrStrListWord[iCurWord], estCurrExerciseSet);
                            ShowHelp(strHelpString);
                        }
                        iLenTypingWord = 0;
                    }
                }
            }

            keyPressed.Clear();
        }

        #endregion
        
        #region New methods

        /// <summary>
        /// Tính ID của bài tập dựa vào các tham số đầu vào
        /// </summary>
        /// <param name="Mode"></param>
        /// <param name="ExSetType"></param>
        /// <param name="idxEx"></param>
        /// <returns></returns>
        private int ComputeExID(TypingMode Mode, ExerciseSetType ExSetType, int idxEx)
        {
            if (idxEx < 0)
                return idxEx;
            return 10 * (10 * ((int)Mode + 1) + (int)ExSetType % 10) + idxEx;
        }

        private int DecodeExIndex(int idx)
        {
            if (idx < 0)
                return idx;
            return (idx % 10) ;
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
                Business.CSpeech.ReadString(str);
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
            try
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
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Lỗi show help ID");
            }
        }

        /// <summary>
        /// Thay đổi một số kích thướng của form, các control
        /// </summary>
        /// <param name="delta"></param>
        private void ChangeHeight(int delta)
        {
            this.Height += delta / 2;
            /*this.lbMainMenu.Height += delta;
            this.layer4RTBVungTapGo.Height += delta;
            this.groupBox1.Height += delta;
            this.rtbHint.Height += delta;*/
            this.lbMainMenu.Top += delta;
            this.layer4RTBVungTapGo.Top += delta;
            this.groupBoxHint.Top += delta / 2;
            this.groupBoxWorkSpace.Top += delta / 2;

        }

        /// <summary>
        /// Đây là phần xử lý của Long đưa ra một Methods riêng nha
        /// Ở đây sẽ tập trung xử lý chuỗi, kiểm tra xem người dùng gõ có đúng ko?
        /// </summary>
        private void ProcessLayer4()
        {
            // Chua ho tro backspace!!
            layer4RTBVungTapGo.Update();
            string curText = layer4RTBVungTapGo.Text;
            int curTextLen = curText.Length;

            if (curTextLen == 0)
            {
                //MessageBox.Show("nothing in text box");
                return;
            }

            if (strUserTypedText != curText.Substring(0, curTextLen - 1))
            {
                layer4RTBVungTapGo.Clear();
                layer4RTBVungTapGo.AppendText(strUserTypedText);
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
                    strUserTypedText = strTypedText;

                    if (bEndstring)
                    {
                        layer4LabelString.Text = strNeedShowedtext + '\n' + strNeedTypingText;
                    }

                    iWrongType = 0;
                }
                else
                {
                    // keu "bip bip" de  bao loi
                    // MessageBox.Show("False!");

                    // khoi tao lai ban dau
                    iWrongType++;

                    if (iWrongType == 3)
                    {
                        MessageBox.Show("Type incorrectly 3 times!");
                        MessageBox.Show(hHelper.HowToTypeString_str(strCurWord));
                        iWrongType = 0;
                    }

                    iCharInWord = 0;
                    iCharNumInWord = 0;
                    strNeedTypingText = strCurWord + t + strNeedTypingText;

                    layer4RTBVungTapGo.Clear();
                    layer4RTBVungTapGo.AppendText(strTypedText);
                    layer4RTBVungTapGo.Update();
                    strUserTypedText = strTypedText;

                    return;
                }
                //MessageBox.Show("Check accuracy!");
            }

            iCharInWord++;

            if (iCharInWord > iCharNumInWord)
            {
                if ((curTextLen > 1))
                {
                    if ((curText[curTextLen - 2] != ' ') && (curText[curTextLen - 2] != '\n') && (curText[curTextLen - 2] != '\t'))
                    {
                        MessageBox.Show("Wrong type!");
                        iWrongType++;

                        if (iWrongType == 3)
                        {
                            MessageBox.Show("Type incorrectly 3 times!");
                            MessageBox.Show(hHelper.HowToTypeString_str(strCurWord));
                            iWrongType = 0;
                        }

                        iCharInWord = 0;
                        iCharNumInWord = 0;

                        layer4RTBVungTapGo.Clear();
                        layer4RTBVungTapGo.AppendText(strTypedText);
                        layer4RTBVungTapGo.Update();
                        strNeedTypingText = strCurWord + " " + strNeedTypingText;
                        strUserTypedText = strTypedText;

                        return;
                    }
                }
                string[] tempNeedTypingWords = strNeedTypingText.Split('\n', ' ', '\t');
                if (tempNeedTypingWords.Length == 1)
                {
                    // chi con mot tu thi lay chuoi ke:
                    strNeedShowedtext = curModel.GetNextString();
                    string tempstr = Business.CConverter.ConvertStrWithMode(strNeedShowedtext, estCurrExerciseSet);

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

            strUserTypedText = layer4RTBVungTapGo.Text;

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
                        strUserTypedText = strTypedText;
                        iWrongType = 0;
                    }
                    else
                    {
                        // keu "bip bip" de  bao loi
                        // MessageBox.Show("False!");

                        iWrongType++;

                        if (iWrongType == 3)
                        {
                            MessageBox.Show("Type incorrectly 3 times!");
                            MessageBox.Show(hHelper.HowToTypeString_str(strCurWord));
                            iWrongType = 0;
                        }
                        // khoi tao lai ban dau
                        iCharInWord = 0;
                        iCharNumInWord = 0;
                        strNeedTypingText = strCurWord;

                        layer4RTBVungTapGo.Clear();
                        layer4RTBVungTapGo.AppendText(strTypedText);
                        layer4RTBVungTapGo.Update();
                        strUserTypedText = strTypedText;

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
            strNeedShowedtext = "";
            strCurWord = "";
            iCharInWord = 0;
            iCharNumInWord = 0;
            strTypedText = "";
            strUserTypedText = "";
            bEndstring = true;
            layer4LabelString.Text = "";
            layer4RTBVungTapGo.Clear();
            iCurWord = 0;
            iLenTypingWord = 0;
            iTimesError = 0;
            iWrongType = 0;
        }

        #endregion
    }
}