using System;
using System.Collections.Generic;
using System.Text;

namespace TypingBC.Business
{
    /// <summary>
    /// Lớp này có nhiệm vụ chuyển mã của 1 chuỗi Unicode về các kiểu gõ tương ứng.
    /// Lớp này là static nên khi gọi hàm chỉ cần CConverter.String2VNI(...)...
    /// </summary>
    public static class CConverter
    {
        #region CodeTable
        private static readonly int m_iTableAmount = 60 - 3;
        private static readonly char[] m_arrUnicodeCode = 
        {
            'đ','á','à','ả','ã','ạ',
            'â','ấ','ầ','ẩ','ẫ','ậ',
            'ă','ắ','ằ','ẳ','ẵ','ặ',
                'é','è','ẻ','ẽ','ẹ',
            'ê','ế','ề','ể','ễ','ệ',
                'ó','ò','ỏ','õ','ọ',
            'ơ','ớ','ờ','ở','ỡ','ợ',
            'ô','ố','ồ','ổ','ỗ','ộ',
                'ú','ù','ủ','õ','ụ',
            'ư','ứ','ừ','ử','ữ','ự'
        };

        private static readonly string[] m_arrVNICode = 
        {
            "d9","a1","a2","a3","a4","a5",
            "a6","a61","a62","a63","a64","a65",
            "a8","a81","a82","a83","a84","a85",
                "e1","e2","e3","e4","e5",
            "e6","e61","e62","e63","e64","e65",
                "o1","o2","o3","o4","o5",
            "o7","o71","o72","o73","o74","o75",
            "o6","o61","o62","o63","o64","o65",
                "u1","u2","u3","u4","u5",
            "u7","u71","u72","u73","u74","u75"
        };

        private static readonly string[] m_arrTelexCode = 
        {
            "dd","as","af","ar","ax","aj",
            "aa","aas","aaf","aar","aax","aaj",
            "aw","aws","awf","awr","awx","awj",
                "es","ef","er","ex","ej",
            "ee","ees","eef","eer","eex","eej",
                "os","of","or","ox","oj",
            "ow","ows","owf","owr","owx","owj",
            "oo","oos","oof","oor","oox","ooj",
                "us","uf","ur","ux","uj",
            "uw","uws","uwf","uwr","uwx","uwj"
        };

        private static readonly string[] m_arrNoMarkCode = 
        {
            "d","a","a","a","a","a",
            "a","a","a","a","a","a",
            "a","a","a","a","a","a",
                "e","e","e","e","e",
            "e","e","e","e","e","e",
                "o","o","o","o","o",
            "o","o","o","o","o","o",
            "o","o","o","o","o","o",
                "u","u","u","u","u",
            "u","u","u","u","u","u"
        };
        
        #endregion

        private static int FindCharInTable(char cChar)
        {
            if ('a' <= cChar && cChar <= 'z')
                return -1;
            for (int i = 0; i < m_iTableAmount; i++)
            {
                if (m_arrUnicodeCode[i] == cChar)
                    return i;
            }
            return -1;
        }

        public static string Str2VNI(string sString)
        {
            char[] arrString = sString.ToCharArray();
            string finalString = "";
            int result;
            bool isUpper;
            for (int i = 0; i < sString.Length; i++)
            {
                isUpper = false;
                if (Char.IsUpper(arrString[i]))
                {
                    result = FindCharInTable(Char.ToLower(arrString[i]));
                    isUpper = true;
                }
                else
                    result = FindCharInTable(arrString[i]);
                if (result == -1)
                    finalString += arrString[i];
                else
                {
                    char[] temp = m_arrVNICode[result].ToCharArray();
                    if (isUpper == true)
                        temp[0] = Char.ToUpper(temp[0]);
                    finalString += new string(temp);
                }
            }
            return finalString;
        }

        public static string Str2Telex(string sString)
        {
            char[] arrString = sString.ToCharArray();
            string finalString = "";
            int result;
            bool isUpper;
            for (int i = 0; i < sString.Length; i++)
            {
                isUpper = false;
                if (Char.IsUpper(arrString[i]))
                {
                    result = FindCharInTable(Char.ToLower(arrString[i]));
                    isUpper = true;
                }
                else
                    result = FindCharInTable(arrString[i]);
                if (result == -1)
                    finalString += arrString[i];
                else
                {
                    char[] temp = m_arrTelexCode[result].ToCharArray();
                    if (isUpper == true)
                        temp[0] = Char.ToUpper(temp[0]);
                    finalString += new string(temp);
                }
            }
            return finalString;
        }

        public static string Str2NoMark(string sString)
        {
            char[] arrString = sString.ToCharArray();
            string finalString = "";
            int result;
            bool isUpper;
            for (int i = 0; i < sString.Length; i++)
            {
                isUpper = false;
                if (Char.IsUpper(arrString[i]))
                {
                    result = FindCharInTable(Char.ToLower(arrString[i]));
                    isUpper = true;
                }
                else
                    result = FindCharInTable(arrString[i]);
                if (result == -1)
                    finalString += arrString[i];
                else
                {
                    char[] temp = m_arrNoMarkCode[result].ToCharArray();
                    if (isUpper == true)
                        temp[0] = Char.ToUpper(temp[0]);
                    finalString += new string(temp);
                }
            }
            return finalString;
        }
    }
}
