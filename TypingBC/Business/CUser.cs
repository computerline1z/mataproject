using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.Presentation;
using TypingBC.DataAccess;

namespace TypingBC.Business
{
    /// <summary>
    /// lớp này có nhiệm vụ trợ giúp tầng Presentation thực hiện một số việc liên quan đến User
    /// View không nên dùng trực tiếp lớp này, mà nên dùng thông qua <see cref="CTypingModel.UserManager"/>
    /// để đảm bảo logic ^^.
    /// </summary>
    public class CUser
    {
        #region ========================= private members ===========

        private CPersistantData m_dataManager;

        #endregion

        public CUser(CPersistantData dataManager)
        {
            this.m_dataManager = dataManager;
        }

        public bool IsUserExisted(string sUserName)
        {
            return m_dataManager.IsUserExisted(sUserName);
        }

        public bool AddUser(string sUserName)
        {
            if(!IsUserExisted(sUserName))
            {
                //TODO: add vào Database
                return m_dataManager.UpdateUserName(sUserName);
            }
            return false;
        }

        public bool AppendPracticeData(CPracticeData data)
        {
            // TODO: add code
            return m_dataManager.UpdatePracData(data);
        }

        public CPracticeData[] GetPracticeData(string sUserName)
        {
            //TODO: add code
            return m_dataManager.LoadPracData(sUserName);
        }

        public int GetUsingExercise(string sUserName)
        {
            //TODO: add code
            return m_dataManager.LoadUsingExID(sUserName);
        }

        public bool SetUsingExercise(string sUserName, int iUsing)
        {
            // TODO: add code
            return m_dataManager.LoadUsingExID(sUserName, iUsing);
        }

        public TypingMode GetUsingTypingMode(string sUserName)
        {
            //TODO: add code
            int idTypingMode = m_dataManager.LoadUserTypingMode(sUserName);
            if (idTypingMode == 0)
                return TypingMode.BRAILLE;
            return TypingMode.NORMAL;
        }

        public bool SetUsingTypingMode(string sUserName, TypingMode mode)
        {
            //TODO: add code
            return m_dataManager.UpdateUserTypingMode(sUserName, (int)mode);
        }
    }
}
