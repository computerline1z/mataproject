using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.DataAccess;
using System.Media;
using System.Diagnostics;
using System.Windows.Forms;

namespace TypingBC.Business
{
    public class CSpeech
    {
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
            
        }

        static CSpeech()
        {
        }
    }
}
