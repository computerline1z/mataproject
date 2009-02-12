using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.DataAccess;
using TypingBC.Presentation;

namespace TypingBC.Business
{
    class CHelp
    {
        int m_iModeCode;

        public int HowToTypeChar(char c)
        {
            return -1;
        }

        public int HowToTypeWord(string sWord)
        {
            return -1;
        }

        public int HowToTypeString(string sString)
        {
            return -1;
        }

        public string HowToTypeChar_str(char c)
        {
            string sModeStr = CPersistantData.Instance.LoadCurrentTypeMode(ref m_iModeCode);
            string m_sResult = String.Format("Để gõ {0} theo cách gõ {1} chúng ta phải gõ như sau {2}",
                c.ToString(), sModeStr, CConverter.ConvertStrWithMode(c.ToString(), (ExerciseSetType)m_iModeCode));
            return m_sResult;
        }

        public string HowToTypeWord_str(string sWord)
        {
            string sModeStr = CPersistantData.Instance.LoadCurrentTypeMode(ref m_iModeCode);
            string m_sResult = String.Format("Để gõ {0} theo cách gõ {1} chúng ta phải gõ như sau {2}",
                sWord, sModeStr, CConverter.ConvertStrWithMode(sWord, (ExerciseSetType)m_iModeCode));
            return m_sResult;
        }

        public string HowToTypeString_str(string sString)
        {
            string sModeStr = CPersistantData.Instance.LoadCurrentTypeMode(ref m_iModeCode);
            string m_sResult = String.Format("Để gõ {0} theo cách gõ {1} chúng ta phải gõ như sau {2}",
                sString, sModeStr, CConverter.ConvertStrWithMode(sString, (ExerciseSetType)m_iModeCode));
            return m_sResult;
        }
    }
}
