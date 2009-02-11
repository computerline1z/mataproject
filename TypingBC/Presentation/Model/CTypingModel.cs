using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.Business;
using TypingBC.DataAccess;

namespace TypingBC.Presentation.Model
{
    public class CTypingModel
    {
        #region ========================= private members ===========

        private CExercise m_usingExercise;
        private ExerciseSetType m_usingExSet;

        private CConfig m_configManager;
        private CUser m_userManager;

        #endregion

        #region ==================== public properties ==============

        public ExerciseSetType UsingExSet
        {
            get { return m_usingExSet; }
        }

        public CUser UserManager
        {
            get { return m_userManager; }
        }

        #endregion

        #region ====================== public interface =============

        public CExerciseSet[] GetExSetList(TypingMode mode)
        {
            return CPersistantData.Instance.LoadExSetList(mode);
        }

        public CExercise[] GetExerciseList(ExerciseSetType type)
        {
            return CPersistantData.Instance.LoadExerciseList(type);
        }

        public string GetNextString()
        {
            return m_usingExercise == null ? string.Empty : m_usingExercise.GetNextString();
        }

        /// <summary>
        /// lấy thông tin trợ giúp của exSet đang dùng (m_usingExSet)
        /// </summary>
        /// <param name="bBegin">TRUE: lấy BeginInstruction; FALSE: lấy EndInstruction.</param>
        /// <returns></returns>
        public int GetExSetInstruction(bool bBegin)
        {
            return CPersistantData.Instance.GetExSetInstruction(bBegin, m_usingExSet);
        }

        /// <summary>
        /// lấy thông tin trợ giúp của exercise đang dùng (m_usingExercise)
        /// </summary>
        /// <param name="bBegin">TRUE: lấy BeginInstruction; FALSE: lấy EndInstruction.</param>
        /// <returns></returns>
        public int GetExerciseInstruction(bool bBegin)
        {
            if(m_usingExercise != null)
            {
                return CPersistantData.Instance.GetExerciseInstruction(bBegin, m_usingExercise.ExID);
            }
            return -1;
        }
        
        public bool LoadExercise(int iExID)
        {
            m_usingExercise = CPersistantData.Instance.LoadExercise(iExID);
            return (m_usingExercise != null);
        }


        public CTypingModel()
        {
            m_configManager = new CConfig();
            m_userManager = new CUser();
            m_usingExercise = null;
        }

        #endregion
    }
}
