using System;
using System.Collections.Generic;
using System.Text;

namespace TypingBC.Business
{
    public class CBrailleMode
    {
        #region ========================= private members ===========

        private Dictionary<char, int> m_arrBrailleKeys;

        private bool m_bUpper;
        private bool m_bDigit;
        private int m_iMark;

        private char[] m_arrAlphabet = 
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 
            'u', 'v', 'w', 'x', 'y', 'z', 
            'ă', 'â', 'đ', 'ê', 'ô', 'ơ', 'ư',
            ',', ';', ':', '.', '?', '!', '=', '"', '"', '[', ']', '\\'
        };

        private int[] m_arrIndex = 
        {
            1, 3, 9, 25, 17, 11, 27, 19, 10, 26, 
            5, 7, 13, 29, 21, 15, 31, 23, 14, 30, 
            37, 39, 58, 45, 61, 53, 
            28, 33, 46, 35, 57, 42, 51,
            2, 18, 6, 50, 34, 38, 70, 54, 68, 55, 62, 44
        };

        private char[] m_arrMark =
        {
            'á', 'à', 'ả', 'ã', 'ạ', 'ắ', 'ằ', 'ẳ', 'ẵ', 'ặ',
            'ấ', 'ầ', 'ẩ', 'ẫ', 'ậ', 'é', 'è', 'ẻ', 'ẽ', 'ẹ',
            'ế', 'ề', 'ể', 'ễ', 'ệ', 'í', 'ì', 'ỉ', 'ĩ', 'ị',
            'ó', 'ò', 'ỏ', 'õ', 'ọ', 'ố', 'ồ', 'ổ', 'ỗ', 'ộ',
            'ớ', 'ờ', 'ở', 'ỡ', 'ợ', 'ú', 'ù', 'ủ', 'ũ', 'ụ',
            'ứ', 'ừ', 'ử', 'ữ', 'ự', 'ý', 'ỳ', 'ỷ', 'ỹ', 'ỵ'
        };

        #endregion

        #region private methods

        private bool special(int b)
        {
            bool isSpecial = true;
            switch (b)
            {
                case 20:            //dấu sắc
                    m_iMark = 0;
                    break;
                case 48:            //dấu huyền
                    m_iMark = 1;
                    break;
                case 34:            //dấu hỏi
                    m_iMark = 2;
                    break;
                case 36:            //dấu ngã
                    m_iMark = 3;
                    break;
                case 32:            //dấu nặng
                    m_iMark = 4;
                    break;
                case 60:            //báo số
                    m_bDigit = true;
                    break;
                case 40:            //báo viết hoa
                    m_bUpper = true;
                    break;
                default:
                    isSpecial = false;
                    break;

            }
            return isSpecial;
        }

        private int vowel(char result)
        {
            switch (result)
            {
                case 'a':
                    return 0;
                    break;
                case 'ă':
                    return 1;
                    break;
                case 'â':
                    return 2;
                    break;
                case 'e':
                    return 3;
                    break;
                case 'ê':
                    return 4;
                    break;
                case 'i':
                    return 5;
                    break;
                case 'o':
                    return 6;
                    break;
                case 'ô':
                    return 7;
                    break;
                case 'ơ':
                    return 8;
                    break;
                case 'u':
                    return 9;
                    break;
                case 'ư':
                    return 10;
                    break;
                case 'y':
                    return 11;
                    break;
                default:
                    return -1;
                    break;
            }
        }

        #endregion

        public void SetBrailleKeys(char[] arrKeys)
        {
            //lưu lại tất cả các phím đã set, thay đổi giá trị nếu phím đã được set
            for (int i = 0; i < 5; i++)
            {
                if (m_arrBrailleKeys.ContainsKey(arrKeys[i]))
                    m_arrBrailleKeys[arrKeys[i]] = i + 1;
                else
                    m_arrBrailleKeys.Add(arrKeys[i], i + 1);
            }
        }

        public char ConvertToChar(params char[] arrKeyPressed)
        {
            //do bản mã chưa thống nhất nên vẫn còn nhiều trường hợp chưa chính xác
            char result = '\0';
            int b = 0;
            //tạm báo có khoảng trắng
            if (arrKeyPressed.Length > 6)
            {
                m_bDigit = false;
                return result;
            }

            //nếu 6 phím truyền vào khác 6 phím đã đăng ký sẽ sai
            //biểu diễn các chấm vào các bit
            foreach (char c in arrKeyPressed)
            {
                if (!m_arrBrailleKeys.ContainsKey(c))
                    return result;
                b |= 0x1 << (m_arrBrailleKeys[c] - 1);
            }
            //xử lý số và các kí hiệu toán học
            if (m_bDigit)
            {
                int p = Array.IndexOf<int>(m_arrIndex, b);
                if (p > -1 && p < 11)
                {
                    return (p + 1).ToString()[0];
                }
                return result;
            }
            //trường hợp ko xuất ra kí tự
            if (special(b))
                return result;

            //xác định kí tự
            int index = Array.IndexOf<int>(m_arrIndex, b);
            if (index == -1)
                return result;
            result = m_arrAlphabet[index];

            //thêm dấu
            if (m_iMark != -1)
            {
                int p = vowel(result);
                if (p == -1)
                    return '\0';
                p = p * 5 + m_iMark;
                m_iMark = -1;
            }
            //viết hoa
            if (m_bUpper)
            {
                m_bUpper = false;
                result = Char.ToUpper(result);
            }

            return result;
        }

        public CBrailleMode()
        {
            //TODO: đọc từ DB vào m_arrBrailleKeys
            m_bUpper = false;
            m_bDigit = false;
            m_iMark = -1;
            m_arrBrailleKeys = new Dictionary<char, int>();
            //do hiện nay đang fix 6 phím này nên chưa load từ database
            m_arrBrailleKeys.Add('f', 1);
            m_arrBrailleKeys.Add('d', 2);
            m_arrBrailleKeys.Add('s', 3);
            m_arrBrailleKeys.Add('j', 4);
            m_arrBrailleKeys.Add('k', 5);
            m_arrBrailleKeys.Add('l', 6);
        }
    }
}
