using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.Presentation;

namespace TypingBC.Business
{
    /// <summary>
    /// Lớp này có nhiệm vụ chuyển mã của 1 chuỗi Unicode về các kiểu gõ tương ứng.
    /// Lớp này là static nên khi gọi hàm chỉ cần CConverter.String2VNI(...)...
    /// </summary>
    public static class CConverter
    {
        #region CodeTable
        private static readonly int m_iTableAmount = 60 - 3;    // số phần tử trong bảng ánh xạ các kiểu gõ
        // các bảng dùng để ánh xạ các kiểu gõ
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

        private static readonly byte[] singleByteTables =

        //TCVN3-ABC
        { 65, 97, 184, 184, 181, 181, 182, 182, 183, 183, 185, 185, 
          162, 169, 202, 202, 199, 199, 200, 200, 201, 201, 203, 203, 
          161, 168, 190, 190, 187, 187, 188, 188, 189, 189, 198, 198, 
          66, 98, 67, 99, 68, 100, 
          167, 174, 
          69, 101, 208, 208, 204, 204, 206, 206, 207, 207, 209, 209, 
          163, 170, 213, 213, 210, 210, 211, 211, 212, 212, 214, 214, 
          70, 102, 71, 103, 72, 104, 
          73, 105, 221, 221, 215, 215, 216, 216, 220, 220, 222, 222, 
          74, 106, 75, 107, 76, 108, 77, 109, 78, 110, 
          79, 111, 227, 227, 223, 223, 225, 225, 226, 226, 228, 228, 
          164, 171, 232, 232, 229, 229, 230, 230, 231, 231, 233, 233, 
          165, 172, 237, 237, 234, 234, 235, 235, 236, 236, 238, 238, 
          80, 112, 81, 113, 82, 114, 83, 115, 84, 116, 
          85, 117, 243, 243, 239, 239, 241, 241, 242, 242, 244, 244, 
          166, 173, 248, 248, 245, 245, 246, 246, 247, 247, 249, 249, 
          86, 118, 87, 119, 88, 120, 
          89, 121, 253, 253, 250, 250, 251, 251, 252, 252, 254, 254, 
          90, 122, 
          0x80, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88,
          0x89, 0x8A, 0x8B, 0x8C, 0x8E, 0x91, 0x92, 0x93,
          0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9A, 0x9B,
          0x9C, 0x9E, 0x9F
        };

        private static readonly ushort[] unicodeTable =

        //Unicode
        { 0x0041, 0x0061, 0x00c1, 0x00e1, 0x00c0, 0x00e0, 0x1ea2, 0x1ea3, 0x00c3, 0x00e3, 0x1ea0, 0x1ea1, //a
          0x00c2, 0x00e2, 0x1ea4, 0x1ea5, 0x1ea6, 0x1ea7, 0x1ea8, 0x1ea9, 0x1eaa, 0x1eab, 0x1eac, 0x1ead, //a^
	      0x0102, 0x0103, 0x1eae, 0x1eaf, 0x1eb0, 0x1eb1, 0x1eb2, 0x1eb3, 0x1eb4, 0x1eb5, 0x1eb6, 0x1eb7, //a(
          0x0042, 0x0062, 0x0043, 0x0063, 0x0044, 0x0064,                                                 //B b C c D d
          0x0110, 0x0111,                                                                                 // DD, dd
	      0x0045, 0x0065, 0x00c9, 0x00e9, 0x00c8, 0x00e8, 0x1eba, 0x1ebb, 0x1ebc, 0x1ebd, 0x1eb8, 0x1eb9, //e
	      0x00ca, 0x00ea, 0x1ebe, 0x1ebf, 0x1ec0, 0x1ec1, 0x1ec2, 0x1ec3, 0x1ec4, 0x1ec5, 0x1ec6, 0x1ec7, //e^
          0x0046, 0x0066, 0x0047, 0x0067, 0x0048, 0x0068,                                                 // F f G g H h
	      0x0049, 0x0069, 0x00cd, 0x00ed, 0x00cc, 0x00ec, 0x1ec8, 0x1ec9, 0x0128, 0x0129, 0x1eca, 0x1ecb, //positions
          0x004a, 0x006a, 0x004b, 0x006b, 0x004c, 0x006c, 0x004d, 0x006d, 0x004e, 0x006e,                 // J j K k L l M m N n
	      0x004f, 0x006f, 0x00d3, 0x00f3, 0x00d2, 0x00f2, 0x1ece, 0x1ecf, 0x00d5, 0x00f5, 0x1ecc, 0x1ecd, //o
          0x00d4, 0x00f4, 0x1ed0, 0x1ed1, 0x1ed2, 0x1ed3, 0x1ed4, 0x1ed5, 0x1ed6, 0x1ed7, 0x1ed8, 0x1ed9, //o^
	      0x01a0, 0x01a1, 0x1eda, 0x1edb, 0x1edc, 0x1edd, 0x1ede, 0x1edf, 0x1ee0, 0x1ee1, 0x1ee2, 0x1ee3, //o+
	      0x0050, 0x0070, 0x0051, 0x0071, 0x0052, 0x0072, 0x0053, 0x0073, 0x0054, 0x0074,                 //P p Q q R r S s T t
	      0x0055, 0x0075, 0x00da, 0x00fa, 0x00d9, 0x00f9, 0x1ee6, 0x1ee7, 0x0168, 0x0169, 0x1ee4, 0x1ee5, //u
	      0x01af, 0x01b0, 0x1ee8, 0x1ee9, 0x1eea, 0x1eeb, 0x1eec, 0x1eed, 0x1eee, 0x1eef, 0x1ef0, 0x1ef1, //u+
          0x0056, 0x0076, 0x0057, 0x0077, 0x0058, 0x0078,                                                 // V v W w X x
          0x0059, 0x0079, 0x00dd, 0x00fd, 0x1ef2, 0x1ef3, 0x1ef6, 0x1ef7, 0x1ef8, 0x1ef9, 0x1ef4, 0x1ef5, //y
          0x005a, 0x007a,                                                                                // Z z
          // Symbols that have different code points in Unicode and Western charsets
	      0x20AC, 0x20A1, 0x0192, 0x201E, 0x2026, 0x2020, 0x2021, 0x02C6,
	      0x2030, 0x0160, 0x2039, 0x0152, 0x017D, 0x2018, 0x2019, 0x201C,
	      0x201D, 0x2022, 0x2013, 0x2014, 0x02DC, 0x2122, 0x0161, 0x203A, 
	      0x0153, 0x017E, 0x0178
        };
        
        #endregion

        /// <summary>
        /// Tra trong bảng m_arrUnicodeCode những kí tự có dấu để tìm được Index 
        /// tương ứng để ánh xạ thành các kiểu gõ khác.
        /// </summary>
        /// <param name="cChar">Kí tự cần tra bảng </param>
        /// <returns>Index của kí tự tìm được trong bảng nếu tìm thấy</returns>
        /// <returns>-1 nếu không tìm thấy </returns>
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

        /// <summary>
        /// Chuyển chuỗi Unicode sang cách gõ VNI
        /// </summary>
        /// <param name="sString">Chuỗi Unicode cần được chuyển</param>
        /// <returns>Chuỗi đã được chuyển </returns>
        /// <example>"Thử nghiệm" -> "Thu73 nghie65m" </example>
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

        /// <summary>
        /// Chuyển chuỗi Unicode sang cách gõ Telex
        /// </summary>
        /// <param name="sString">Chuỗi Unicode cần được chuyển</param>
        /// <returns>Chuỗi đã được chuyển </returns>
        /// <example>"Thử nghiệm" -> "Thuwr nghieejm" </example>
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

        /// <summary>
        /// Chuyển chuỗi Unicode sang kiểu không bỏ dấu
        /// </summary>
        /// <param name="sString">Chuỗi Unicode cần được chuyển</param>
        /// <returns>Chuỗi đã được chuyển </returns>
        /// <example>"Thử nghiệm" -> "Thu nghiem </example>
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

        /// <summary>
        /// Chuyển chuỗi Unicode sang các kiểu gõ khác với mã số của kiểu gõ
        /// </summary>
        /// <param name="sString">Chuỗi Unicode cần được chuyển</param>
        /// <param name="estMode">Mã số kiểu gõ hiện tại
        ///     1: VNI
        ///     2: Telex
        ///     3: NoMark
        /// </param>
        /// <returns>Chuỗi đã được chuyển</returns>
        public static string ConvertStrWithMode(string sString, ExerciseSetType estExSetType)
        {
            switch (estExSetType)
            {
                case ExerciseSetType.NOMARK :
                case ExerciseSetType.NOMARK_BRAILLE:
                    return Str2NoMark(sString);
                case ExerciseSetType.VNI: 
                    return Str2VNI(sString);
                case ExerciseSetType.TELEX: 
                    return Str2Telex(sString);
                case ExerciseSetType.MARK_BRAILLE:
                    return sString;
            }
            return string.Empty;
        }

        public static string UniToTCVN(string sString)
        {
            //TODO: add code
            return string.Empty;
        }
    }
}
