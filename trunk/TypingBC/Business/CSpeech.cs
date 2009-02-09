using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.DataAccess;
using System.Media;
using System.Diagnostics;

namespace TypingBC.Business
{
    public class CSpeech
    {

        public static void ReadString(int iStringID)
        {
            string pathFile = CPersistantData.Instance.GetSpeechEntry(iStringID, true);
            SoundPlayer player = new SoundPlayer(pathFile);
            player.Play();
        }

        public static void ReadString(string sString)
        {
            
        }

        static CSpeech()
        {
        }

        //public static void ReadString(char c)
        //{

        //}
    }
}
