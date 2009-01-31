using System;
using System.Collections.Generic;
using System.Text;

namespace TypingBC.Presentation
{
    /// <summary>
    /// kiểu gõ dùng trong chương trình
    /// </summary>
    public enum TypingMode
    {
        /// <summary>gõ kiểu bình thường</summary>
        NORMAL = 0,
        /// <summary>gõ 6 phím như người khiếm thị.</summary>
        BRAILLE = 1
    }

    public enum ExerciseSetType
    {
        RECORNITION = 0,
        SIMPLE = 1,
        NOMARK = 2,
        VNI = 3,
        TELEX = 4
    }

    /// <summary>
    /// chỉ chứa dữ liệu về các ExericiseSet.
    /// </summary>
    public class CExerciseSet
    {
        private ExerciseSetType m_iExSetType;
        private int m_iBeginInstruction, m_iEndInstruction;
        private string m_sName;

        public string ExSetName
        {
            get { return m_sName; }
            set { m_sName = value; }
        }

        public ExerciseSetType ExSetType
        {
            get { return m_iExSetType; }
            set { m_iExSetType = value; }
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

        public CExerciseSet(ExerciseSetType type, string sName, int beginInstruction, int endInstruction)
        {
            m_iExSetType = type;
            m_sName = sName;
            m_iBeginInstruction = beginInstruction;
            m_iEndInstruction = endInstruction;
        }

        public CExerciseSet(){}
    }
}
