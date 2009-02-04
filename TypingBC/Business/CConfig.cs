using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.Presentation;
using TypingBC.DataAccess;

namespace TypingBC.Business
{
    public class CConfig
    {
        public const int iDeltaWidth = 450;
        public const int iDeltaHeight = 8;
        private int m_iBlindRepeatTime;
        private CPersistantData m_dataManager;

        private void SaveConfig()
        {
            // TODO: save config vào DB
            this.m_dataManager.SaveConfig(this.m_iBlindRepeatTime);
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

        public CConfig(CPersistantData dataManager)
        {
            //TODO: đọc config từ Database
            this.m_dataManager = dataManager;
            m_iBlindRepeatTime = this.m_dataManager.LoadConfig();
        }
    }
}
