using System;
using System.Collections.Generic;
using System.Text;

namespace TypingBC.Business
{
    public class CBrailleMode
    {
        private char[] m_arrBrailleKeys;
        
        public void SetBrailleKeys(char[] arrKeys)
        {
            m_arrBrailleKeys = arrKeys;
        }

        public char ConvertToChar(params char[] arrKeyPressed)
        {
            return (char)0;
        }

        public CBrailleMode()
        {
            //TODO: đọc từ DB vào m_arrBrailleKeys
        }
    }
}
