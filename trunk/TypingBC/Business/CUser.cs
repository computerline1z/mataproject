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


        #endregion

        public CUser()
        {
        }

        public bool IsUserExisted(string sUserName)
        {
            return CPersistantData.Instance.IsUserExisted(sUserName);
        }

        public bool AddUser(string sUserName)
        {
            if (!IsUserExisted(sUserName))
            {
                //TODO: add vào Database
                return CPersistantData.Instance.AddUser(sUserName);
            }
            return false;
        }

        public bool AppendPracticeData(CPracticeData data)
        {
            return CPersistantData.Instance.UpdatePracData(data);
        }

        public CPracticeData[] GetPracticeData(string sUserName)
        {
            return CPersistantData.Instance.LoadPracData(sUserName);
        }

        public int GetUsingExercise(string sUserName)
        {
            return CPersistantData.Instance.LoadUsingExID(sUserName);
        }

        public ExerciseSetType GetUsingExerciseSet(string sUserName)
        {
            return CPersistantData.Instance.LoadUsingExSetID(sUserName);
        }

        public bool SetUsingExercise(string sUserName, int iUsing)
        {
            return CPersistantData.Instance.SetUsingExID(sUserName, iUsing);
        }

        public TypingMode GetUsingTypingMode(string sUserName)
        {
            return CPersistantData.Instance.LoadUserTypingMode(sUserName);
        }

        public bool SetUsingTypingMode(string sUserName, TypingMode mode)
        {
            return CPersistantData.Instance.UpdateUserTypingMode(sUserName, (int)mode);
        }
    }
}
