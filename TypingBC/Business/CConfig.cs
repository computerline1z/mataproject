using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.Presentation;

namespace TypingBC.Business
{
    public class CConfig
    {
        private int m_iBlindRepeatTime;

        private void SaveConfig()
        {
            // TODO: save config vào DB
        }

        public int BlindRepeatTime
        {
            get { return m_iBlindRepeatTime; }
            set 
            {
                m_iBlindRepeatTime = value;
                SaveConfig();
            }
        }


        public CConfig()
        {
            //TODO: đọc config từ Database
        }
    }
}
