using System;
using System.Collections.Generic;
using System.Text;

namespace TypingBC.Presentation
{
    public class CExercise
    {
        private int m_iExID;
        private string m_sExName;
        private int m_iBeginInstruction, m_iEndInstruction;
        private List<string> m_lstContents;
        private int m_iCurrentString;
        
        public int ExID
        {
            get { return m_iExID; }
            set { m_iExID = value; }
        }

        public string ExName
        {
            get { return m_sExName; }
            set { m_sExName = value; }
        }

        public int BeginInstruction
        {
            get { return m_iBeginInstruction; }
            set { m_iBeginInstruction = value; }
        }

        public int EndInstruction
        {
            get { return m_iEndInstruction; }
            set { m_iEndInstruction = value; }
        }

        public int CurrentString
        {
            get { return m_iCurrentString; }
        }

        public string GetNextString()
        {
            string sRet = string.Empty;
            if(m_iCurrentString < m_lstContents.Count)
            {
                sRet = m_lstContents[m_iCurrentString];
                m_iCurrentString++;
            }
            return sRet;
        }

        public void ResetPosition()
        {
            m_iCurrentString = 0;
        }

        public void AddString(string sString)
        {
            m_lstContents.Add(sString);
        }

        public void AddString(params string[] arrStrings)
        {
            m_lstContents.AddRange(arrStrings);
        }

        public void ReadContents(string sPath)
        {
            //TODO: đọc dữ liệu Exercise từ file txt (sPath).
        }

        public CExercise()
        {
            m_lstContents = new List<string>();
            m_iCurrentString = 0;
        }
    }
}
