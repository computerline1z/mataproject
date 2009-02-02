using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.Presentation;
using TypingBC.DataAccess;

namespace TypingBC.Business
{
    /// <summary>
    /// dùng tìm kiếm user
    /// </summary>
    sealed class CompUser
    {
        private string m_sName = null;

        public string Name
        {
            set { m_sName = value; }
        }

        public bool cmp(string user)
        {
            if (m_sName.CompareTo(user) == 0)
                return true;
            return false; 
        }
    }

    /// <summary>
    /// lớp này có nhiệm vụ trợ giúp tầng Presentation thực hiện một số việc liên quan đến User
    /// View không nên dùng trực tiếp lớp này, mà nên dùng thông qua <see cref="CTypingModel.UserManager"/>
    /// để đảm bảo logic ^^.
    /// </summary>
    public class CUser
    {
        #region ========================= private members ===========

        private CPersistantData m_dataManager;
        private CompUser m_cmpUser;

        #endregion

        public CUser(CPersistantData dataManager)
        {
            this.m_dataManager = dataManager;
            this.m_cmpUser = new CompUser(); 
        }

        public bool IsUserExisted(string sUserName)
        {
//             m_cmpUser.Name = sUserName;
//             string[] listUser = m_dataManager.LoadUser();
// 
//             if (listUser == null)
//                 return false;
//             return Array.Exists<string>(listUser, m_cmpUser.cmp);
            return m_dataManager.IsUserExisted(sUserName);
        }

        public bool AddUser(string sUserName)
        {
            if(!IsUserExisted(sUserName))
            {
                //TODO: add vào Database
                return m_dataManager.UpdateUser(sUserName);
            }
            return false;
        }

        public bool AppendPracticeData(CPracticeData data)
        {
            // TODO: add code.
            return false;
        }

        public CPracticeData[] GetPracticeData(string sUserName)
        {
            //TODO: add code
            return null;
        }

        public int GetUsingExercise(string sUserName)
        {
            //TODO: add code
            return 0;
        }

        public bool SetUsingExercise(string sUserName, int iUsing)
        {
            // TODO: add code
            return false;
        }

        public TypingMode GetUsingTypingMode(string sUserName)
        {
            //TODO: add code
            return TypingMode.NORMAL;
        }

        public bool SetUsingTypingMode(string sUserName, TypingMode mode)
        {
            //TODO: add code
            return false;
        }
    }
}
