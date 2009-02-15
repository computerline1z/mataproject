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

        public string HowToTypeWord_str(string sWord, ExerciseSetType estCurExSet)
        {
            //string sModeStr = CPersistantData.Instance.LoadCurrentTypeMode(ref m_iModeCode);
            string sModeStr = "";
            switch (estCurExSet)
            {
                case ExerciseSetType.NOMARK:
                    sModeStr = "Không dấu";
                    break;
                case ExerciseSetType.NOMARK_BRAILLE:
                    sModeStr = "Không dấu Braille";
                    break;
                case ExerciseSetType.TELEX:
                    sModeStr = "TELEX";
                    break;
                case ExerciseSetType.VNI:
                    sModeStr = "VNI";
                    break;
                case ExerciseSetType.MARK_BRAILLE:
                    sModeStr = "Có dấu Braille";
                    break;
            }
            string m_sResult = String.Format("Để gõ {0} theo cách gõ {1} chúng ta phải gõ như sau {2}",
                sWord, sModeStr, CConverter.ConvertStrWithMode(sWord, estCurExSet));
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
