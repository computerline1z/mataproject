using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.Presentation;
using System.Data;
using System.IO;
using TypingBC.Business;

namespace TypingBC.DataAccess
{
    public class CPersistantData
    {
        private const string TABLEFILE_EXERCISESET = "Data/XML/tbExerciseSet.xml";
        private const string TABLEFILE_EXERCISE = "Data/XML/tbExercise.xml";
        private const string TABLEFILE_CONFIG = "Data/XML/tbConfig.xml";
        private const string TABLEFILE_PRACTICEDATA = "Data/XML/tbPracticeData.xml";
        private const string TABLEFILE_SPEECH = "Data/XML/tbSpeech.xml";
        private const string TABLEFILE_USER = "Data/XML/tbUser.xml";

        private DataTable m_dtExercise;
        private DataTable m_dtExSet;
        private DataTable m_dtUser;

        private DataTable ReadDataFile(string sFile)
        {
            DataSet dtSet = new DataSet();
            dtSet.ReadXml(CurrentPath + sFile);
            return dtSet.Tables[1];
        }

        public string CurrentPath
        {
            get
            {
                string s = System.Windows.Forms.Application.StartupPath;
                s += s.EndsWith("\\") ? "" : "\\";
                return s;
            }
        }

        /// <summary>
        /// Đọc danh sách các ExerciseSet
        /// </summary>
        /// <returns>Mảng các ExerciseSet đọc từ DB. Nếu có lỗi, trả về null.</returns>
        public CExerciseSet[] LoadExSetList()
        {
            try
            {
                List<CExerciseSet> lsRet = new List<CExerciseSet>();
                foreach (DataRow dtRow in m_dtExSet.Rows)
                {
                    lsRet.Add(new CExerciseSet((ExerciseSetType)dtRow[0],
                            (string)dtRow[1], (int)dtRow[2], (int)dtRow[3]));
                }
                return lsRet.ToArray();
            }
            catch (System.Exception /*ex*/)
            {
                return null;
            }
        }

        /// <summary>
        /// Trả về Danh sách các Exercise thuộc exerciseSet cho trước.
        /// </summary>
        /// <param name="type"><see cref="ExerciseSetType"/></param>
        /// <returns>Danh sách các Exercise. Nếu có lỗi, trả về null.</returns>
        /// <remarks>LƯU Ý: Để tiết kiệm thời gian thì hàm này sẽ không trả về nội dung của các bài tập.
        /// Nghĩa là các <see cref="CExercise"/> trong này chỉ có ID và ExName là nên dùng.</remarks>
        public CExercise[] LoadExerciseList(ExerciseSetType type)
        {
            try
            {
                DataRow[] arrRows = m_dtExercise.Select("ExSetID = " + type);

                List<CExercise> lsRet = new List<CExercise>();

                foreach (DataRow dtRow in arrRows)
                {
                    CExercise ex = new CExercise();
                    ex.ExID = (int)dtRow[0];
                    ex.ExName = (string)dtRow[2];
                    string sDataFile = (string)dtRow[3];
                    ex.BeginInstruction = (int)dtRow[4];
                    ex.EndInstruction = (int)dtRow[5];

                    //StreamReader streamFile = new StreamReader(CurrentPath + sDataFile);
                    //while (!streamFile.EndOfStream)
                    //{
                    //    ex.AddString(streamFile.ReadLine());
                    //}
                    lsRet.Add(ex);
                }
                return lsRet.ToArray();
            }
            catch (System.Exception /*ex*/)
            {
                return null;
            }
        }

        public int GetExSetInstruction(bool bBegin, ExerciseSetType type)
        {
            try
            {
                DataRow[] arrRows = m_dtExSet.Select("ID = " + type);
                if (arrRows == null || arrRows.Length == 0)
                {
                    return 0;
                }
                return (int)arrRows[0][bBegin ? 2 : 3];
            }
            catch (System.Exception /*ex*/)
            {
                return 0;
            }
        }

        public int GetExerciseInstruction(bool bBegin, int iExID)
        {
            try
            {
                DataRow[] arrRows = m_dtExercise.Select("ID = " + iExID);
                if(arrRows == null || arrRows.Length == 0)
                {
                    return 0;
                }
                return (int)arrRows[0][bBegin ? 4 : 5];
            }
            catch (System.Exception /*ex*/)
            {
            }
            return 0;
        }

        public CExercise LoadExercise(int iExID)
        {
            try
            {
                DataRow[] arrRows = m_dtExercise.Select("ID = " + iExID);
               
                if (arrRows != null && arrRows.Length > 0)
                {
                    CExercise ex = new CExercise();
                    DataRow dtRow = arrRows[0];

                    ex.ExID = (int)dtRow[0];
                    ex.ExName = (string)dtRow[2];
                    string sDataFile = (string)dtRow[3];
                    ex.BeginInstruction = (int)dtRow[4];
                    ex.EndInstruction = (int)dtRow[5];

                    StreamReader streamFile = new StreamReader(CurrentPath + sDataFile);
                    while (!streamFile.EndOfStream)
                    {
                        ex.AddString(streamFile.ReadLine());
                    }
                    return ex;
                }
            }
            catch{}
            return null;
        }

        public bool IsUserExisted(string sNick)
        {
            string s = string.Format("UserName like '{0}'", sNick.Trim().Replace("'", "''"));
            DataRow[] arrRows = m_dtUser.Select(s);
            return (arrRows != null && arrRows.Length > 0);
        }

        public string[] LoadUser()
        {
            try
            {
                List<string> lsRet = new List<string>();
                foreach (DataRow dtRow in m_dtUser.Rows)
                {
                    lsRet.Add((string)dtRow[0]);
                }
                return lsRet.ToArray();
            }
            catch{}
            return null;
        }

        public bool UpdateUserName(string UserName)
        {
            //TODO: add code
            return true;
        }

        public int LoadConfig()
        {
            //TODO: load len blind time
            return 1;
        }

        public void SaveConfig(int BlindRepeatTime)
        {
            //TODO: save lai config
        }

        public CPersistantData()
        {
            m_dtUser = ReadDataFile(TABLEFILE_USER);
            m_dtUser.CaseSensitive = false;
            m_dtExercise = ReadDataFile(TABLEFILE_EXERCISE);
            m_dtExercise.CaseSensitive = false;
            m_dtExSet = ReadDataFile(TABLEFILE_EXERCISESET);
            m_dtExSet.CaseSensitive = false;
        }

        public bool UpdatePracData(CPracticeData data)
        {
            //TODO: add code
            return false;
        }

        public bool UpdateUserTypingMode(string UserName, int mode)
        {
            //TODO: add code
            return false;
        }

        public int LoadUserTypingMode(string UserName)
        {
            //TODO: add code
            return 0;
        }

        public bool LoadUsingExID(string UserName, int id)
        {
            //TODO: add code
            return false;
        }

        public int LoadUsingExID(string UserName)
        {
            //TODO: add code
            return 0;
        }

        public CPracticeData[] LoadPracData(string UserName)
        {
            //TODO: add code
            return null;
        }
    }
}
