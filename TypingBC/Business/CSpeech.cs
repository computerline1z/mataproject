using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.DataAccess;
using System.Media;

namespace TypingBC.Business
{
    public class CSpeech
    {
        private static CPersistantData m_Data;

        public static void ReadString(int iStringID)
        {
            string pathFile = m_Data.GetSpeechEntry(iStringID, true);

            SoundPlayer player = new SoundPlayer(pathFile);
            player.Play();
        }

        public static void ReadString(string sString)
        {
            
        }

        static CSpeech()
        {
            m_Data = new CPersistantData();
        }

        //public static void ReadString(char c)
        //{

        //}
    }
}
