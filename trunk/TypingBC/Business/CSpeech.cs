using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.DataAccess;
using System.Media;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TypingBC.Business
{
    public class CSpeech
    {
        //[DllImport("VNSPEECH.DLL", EntryPoint = "VietTTS")]
        //static extern int VietTTS(string test);
        //[DllImport("VNSPEECH.DLL", EntryPoint = "VietTTSStop")]
        //static extern bool VietTTSStop();
        [DllImport("TESTDLL2.DLL", EntryPoint = "speak")]
        static extern void speak(string test);

        public static void ReadString(int iStringID)
        {
            try
            {
                string pathFile = CPersistantData.Instance.GetSpeechEntry(iStringID, true);
                SoundPlayer player = new SoundPlayer(pathFile);
                player.Play();
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Lỗi khi đọc file âm thanh");
            }
        }

        public static void ReadString(string sString)
        {
            //string text = CConverter.UniToTCVN(sString);

            //int time = VietTTS(text);
            //System.Threading.Thread.Sleep(time);
            //VietTTSStop();
            speak(sString);
        }

        static CSpeech()
        {
        }
    }
}
