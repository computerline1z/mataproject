﻿using System;
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
        NOMARK = 1,
        VNI = 2,
        TELEX = 3,
        RECORNITION_BRAILLE = 10,
        NOMARK_BRAILLE = 11,
        MARK_BRAILLE = 12,
    }

    /// <summary>
    /// chỉ chứa dữ liệu về các ExericiseSet.
    /// </summary>
    public class CExerciseSet
    {
        private ExerciseSetType m_iExSetType;
        private TypingMode m_typingMode;
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

        public TypingMode Mode
        {
            get{return m_typingMode;}
            set{m_typingMode = value;}
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

        public CExerciseSet(ExerciseSetType type, TypingMode mode, string sName, int beginInstruction, int endInstruction)
        {
            m_iExSetType = type;
            m_typingMode = mode;
            m_sName = sName;
            m_iBeginInstruction = beginInstruction;
            m_iEndInstruction = endInstruction;
        }

        public CExerciseSet(){}
    }
}
