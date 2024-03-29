﻿using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.Presentation;
using TypingBC.DataAccess;

namespace TypingBC.Business
{
    public class CConfig
    {
        public const int iDeltaWidth    = 450;
        public const int iDeltaHeight   = 8;
        public const int iInterval      = 50;
        private int m_iBlindRepeatTime;

        private void SaveConfig()
        {
            // TODO: save config vào DB
            CPersistantData.Instance.SaveConfig(this.m_iBlindRepeatTime);
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
            m_iBlindRepeatTime = CPersistantData.Instance.LoadConfig();
        }
    }
}
