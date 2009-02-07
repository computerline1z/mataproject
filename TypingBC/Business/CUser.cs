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
                return m_dataManager.AddUser(sUserName);
            }
            return false;
        }

        public bool AppendPracticeData(CPracticeData data)
        {
            return m_dataManager.UpdatePracData(data);
        }

        public CPracticeData[] GetPracticeData(string sUserName)
        {
            return m_dataManager.LoadPracData(sUserName);
        }

        public int GetUsingExercise(string sUserName)
        {
            return m_dataManager.LoadUsingExID(sUserName);
        }

        public bool SetUsingExercise(string sUserName, int iUsing)
        {
            return m_dataManager.SetUsingExID(sUserName, iUsing);
        }

        public TypingMode GetUsingTypingMode(string sUserName)
        {
            return m_dataManager.LoadUserTypingMode(sUserName);
        }

        public bool SetUsingTypingMode(string sUserName, TypingMode mode)
        {
            return m_dataManager.UpdateUserTypingMode(sUserName, (int)mode);
        }
    }
}
