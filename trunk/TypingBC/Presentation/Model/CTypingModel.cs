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
        private CPersistantData m_dataManager;

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

        public CExerciseSet[] GetExSetList()
        {
            return m_dataManager.LoadExSetList();
        }

        public CExercise[] GetExerciseList(ExerciseSetType type)
        {
            return m_dataManager.LoadExerciseList(type);
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
            return m_dataManager.GetExSetInstruction(bBegin, m_usingExSet);
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
                return m_dataManager.GetExerciseInstruction(bBegin, m_usingExercise.ExID);
            }
            return -1;
        }

        public bool LoadExercise(int iExID)
        {
            m_usingExercise = m_dataManager.LoadExercise(iExID);
            return (m_usingExercise != null);
        }


        public CTypingModel()
        {
            m_dataManager = new CPersistantData();
            m_configManager = new CConfig(m_dataManager);
            m_userManager = new CUser(m_dataManager);
            m_usingExercise = null;
        }

        #endregion
    }
}
