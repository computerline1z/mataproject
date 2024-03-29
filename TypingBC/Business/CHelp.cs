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

            string m_sResult;

            if ((int)estCurExSet/10 > 0)
            {
                CBrailleMode BrailleMode = new CBrailleMode();
                string Encode;
                if (estCurExSet == ExerciseSetType.NOMARK_BRAILLE)
                    Encode = BrailleMode.Str2Braille(CConverter.Str2NoMark(sWord));
                else
                    Encode = BrailleMode.Str2Braille(sWord);
                string newEncode = "";
                for (int i = 0; i < Encode.Length; i++ )
                {
                        switch (Encode[i])
                        {
                            case 's':
                                newEncode += "một ";
                                break;
                            case 'd':
                                newEncode += "hai ";
                                break;
                            case 'f':
                                newEncode += "ba ";
                                break;
                            case 'j':
                                newEncode += "bốn ";
                                break;
                            case 'k':
                                newEncode += "năm ";
                                break;
                            case 'l':
                                newEncode += "sáu ";
                                break;
                            case '_':
                                newEncode += "và ";
                                break;
                        }
                }
                m_sResult = String.Format("Để gõ {0} theo cách gõ {1} chúng ta phải gõ như sau {2}",
                        sWord, sModeStr, newEncode);
            }
            else
            {
                m_sResult = String.Format("Để gõ {0} theo cách gõ {1} chúng ta phải gõ như sau {2}",
                sWord, sModeStr, CConverter.ConvertStrWithMode(sWord, estCurExSet));
            }
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
