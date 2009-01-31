using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.Presentation;

namespace TypingBC.Business
{
    /// <summary>
    /// lớp này có nhiệm vụ trợ giúp tầng Presentation thực hiện một số việc liên quan đến User
    /// View không nên dùng trực tiếp lớp này, mà nên dùng thông qua <see cref="CTypingModel.UserManager"/>
    /// để đảm bảo logic ^^.
    /// </summary>
    public class CUser
    {
        public bool IsUserExisted(string sUserName)
        {
            //TODO: kiểm tra...
            return false;
        }

        public bool AddUser(string sUserName)
        {
            if(!IsUserExisted(sUserName))
            {
                //TODO: add vào Database
                return true;
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
