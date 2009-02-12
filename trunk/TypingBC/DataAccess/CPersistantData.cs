using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.Presentation;
using System.Data;
using System.IO;
using System.Windows.Forms;
using TypingBC.Business;
using System.Xml;

namespace TypingBC.DataAccess
{
    public class CPersistantData : IDisposable
    {

        #region ===================== Private members ==============================

        private const string TABLEFILE_EXERCISESET = "Data/XML/tbExerciseSet.xml";
        private const string TABLEFILE_EXERCISE = "Data/XML/tbExercise.xml";
        private const string TABLEFILE_CONFIG = "Data/XML/tbConfig.xml";
        private const string TABLEFILE_PRACTICEDATA = "Data/XML/tbPracticeData.xml";
        private const string TABLEFILE_SPEECH = "Data/XML/tbSpeech.xml";
        private const string TABLEFILE_USER = "Data/XML/tbUser.xml";

        private DataTable m_dtExercise;
        private DataTable m_dtExSet;
        private DataTable m_dtUser;
        private DataTable m_dtPracticeData;
        private DataTable m_dtSpeech;
        private Dictionary<string, DataSet> m_lsDataSets = new Dictionary<string, DataSet>();

        private static CPersistantData m_Instance = null;

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            SaveData();
            foreach (string sKey in m_lsDataSets.Keys)
            {
                m_lsDataSets[sKey].Dispose();
            }
            m_lsDataSets.Clear();
            m_Instance = null;
        }

        #endregion

        #region =============== Singleton, Constructor, Utilities ==================

        public static CPersistantData Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CPersistantData();
                }
                return m_Instance;  
            }
        }

        protected CPersistantData()
        {
            m_dtUser = ReadDataFile(TABLEFILE_USER);
            m_dtUser.CaseSensitive = false;
            m_dtUser.PrimaryKey = new DataColumn[] { m_dtUser.Columns[0] };

            m_dtExercise = ReadDataFile(TABLEFILE_EXERCISE);
            m_dtExercise.CaseSensitive = false;

            m_dtExSet = ReadDataFile(TABLEFILE_EXERCISESET);
            m_dtExSet.CaseSensitive = false;

            m_dtPracticeData = ReadDataFile(TABLEFILE_PRACTICEDATA);
            m_dtPracticeData.CaseSensitive = false;
            m_dtPracticeData.PrimaryKey = new DataColumn[] { m_dtPracticeData.Columns[0] };

            m_dtSpeech = ReadDataFile(TABLEFILE_SPEECH);
            m_dtSpeech.CaseSensitive = false;
            m_dtSpeech.PrimaryKey = new DataColumn[] { m_dtSpeech.Columns[0] };
        }

        private DataTable ReadDataFile(string sFile)
        {
            DataSet dtSet = new DataSet();
            dtSet.ReadXml(CurrentPath + sFile, XmlReadMode.ReadSchema);
            m_lsDataSets.Add(CurrentPath + sFile, dtSet);
            return dtSet.Tables[dtSet.Tables.Count - 1];
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

        public void SaveData()
        {
            string sPath = CurrentPath;

            foreach(string sKeys in m_lsDataSets.Keys)
            {
                m_lsDataSets[sKeys].WriteXml(sKeys, XmlWriteMode.WriteSchema);
            }
            //m_dtExercise.DataSet.WriteXml(sPath + TABLEFILE_EXERCISE);
            //m_dtExSet.DataSet.WriteXml(sPath + TABLEFILE_EXERCISESET);
            //m_dtUser.DataSet.WriteXml(sPath + TABLEFILE_USER);
            //m_dtPracticeData.DataSet.WriteXml(sPath + TABLEFILE_PRACTICEDATA);
            //m_dtSpeech.DataSet.WriteXml(sPath + TABLEFILE_SPEECH);
        }

        #endregion

        #region ======================== Public interface ==========================

        /// <summary>
        /// Đọc danh sách các ExerciseSet
        /// </summary>
        /// <param name="mode">một giá trị <see cref="TypingMode"/> cho biết kiểu gõ.</param>
        /// <returns>Mảng các ExerciseSet đọc từ DB. Nếu có lỗi, trả về null.</returns>
        public CExerciseSet[] LoadExSetList(TypingMode mode)
        {
            try
            {
                List<CExerciseSet> lsRet = new List<CExerciseSet>();
                DataTableReader reader = m_dtExSet.CreateDataReader();
                while(reader.Read())
                {
                    if(reader.GetInt32(1) == (int)mode)
                    {
                        lsRet.Add(new CExerciseSet((ExerciseSetType)reader.GetInt32(0),
                                (TypingMode)reader.GetInt32(1),
                                reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4)));
                    }
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
                DataRow[] arrRows = m_dtExercise.Select("ExSetID = " + (int)type);

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
                DataRow[] arrRows = m_dtExSet.Select("ID=" + (int)type);
                if (arrRows == null || arrRows.Length == 0)
                {
                    return 0;
                }
                return (int)arrRows[0][bBegin ? 3 : 4];
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
                DataRow[] arrRows = m_dtExercise.Select("ID=" + iExID.ToString());
               
                if (arrRows != null && arrRows.Length > 0)
                {
                    CExercise ex = new CExercise();
                    DataRow dtRow = arrRows[0];

                    ex.ExID = (int)dtRow[0];
                    ex.ExName = (string)dtRow[2];
                    string sDataFile = (string)dtRow[3];
                    ex.BeginInstruction = (int)dtRow[4];
                    ex.EndInstruction = (int)dtRow[5];

                    /************************************************************/
                    /* CurrentPath: duong dan khong dung?
                    /************************************************************/
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

        public bool AddUser(string UserName)
        {
            try
            {
                DataRow dtRowNew = m_dtUser.NewRow();
                dtRowNew[0] = UserName;
                dtRowNew[1] = -1;
                dtRowNew[2] = 0;
                m_dtUser.Rows.Add(dtRowNew);
                m_dtUser.AcceptChanges();
                return true;
            }
            catch
            {
                return false;
            }
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

        public bool UpdatePracData(CPracticeData data)
        {
            try
            {
                int iNewID;
                for (iNewID = 0; m_dtPracticeData.Rows.Contains(iNewID); ++iNewID) ;
                DataRow dtRowNew = m_dtPracticeData.NewRow();
                dtRowNew[0] = iNewID;
                dtRowNew[1] = data.UserName;
                dtRowNew[2] = data.PraticeTime.ToBinary();
                dtRowNew[3] = data.ExerciseCount;
                dtRowNew[4] = data.KeyCount;
                dtRowNew[0] = iNewID;
                dtRowNew[0] = iNewID;
                dtRowNew[0] = iNewID;
                m_dtPracticeData.Rows.Add(iNewID, data.UserName, data.PraticeTime.ToBinary(), data.ExerciseCount,
                    data.KeyCount, data.FailKeyCount, data.UsingHelpCount, data.TotalTime);
                m_dtPracticeData.AcceptChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateUserTypingMode(string UserName, int mode)
        {
            DataRow dtRow = m_dtUser.Rows.Find(UserName);
            if (dtRow != null)
            {
                dtRow[2] = mode;
                m_dtUser.AcceptChanges();
            }
            return dtRow != null;
        }

        public TypingMode LoadUserTypingMode(string UserName)
        {
            DataRow dtRow = m_dtUser.Rows.Find(UserName);
            if (dtRow != null)
                return (TypingMode)dtRow[2];
            return TypingMode.NORMAL;
        }

        public bool SetUsingExID(string UserName, int id)
        {
            DataRow dtRow = m_dtUser.Rows.Find(UserName);
            if (dtRow != null)
            {
                dtRow[1] = id;
                m_dtUser.AcceptChanges();
            }
            return dtRow != null;
        }

        public int LoadUsingExID(string UserName)
        {
            DataRow dtRow = m_dtUser.Rows.Find(UserName);
            if (dtRow != null)
                return (int)dtRow[1];
            return -1;
        }

        public CPracticeData[] LoadPracData(string UserName)
        {
            try
            {
                string s = string.Format("UserName like '{0}'", UserName.Trim().Replace("'", "''"));
                DataRow[] arrRows = m_dtPracticeData.Select(s);
                if (arrRows != null && arrRows.Length > 0)
                {
                    List<CPracticeData> lsRet = new List<CPracticeData>();
                    foreach (DataRow dtRow in arrRows)
                    {
                        CPracticeData data = new CPracticeData(dtRow[1].ToString());
                        data.PraticeTime = new DateTime(long.Parse(dtRow[2].ToString()));
                        data.ExerciseCount = long.Parse(dtRow[3].ToString());
                        data.KeyCount = long.Parse(dtRow[4].ToString());
                        data.FailKeyCount = long.Parse(dtRow[5].ToString());
                        data.UsingHelpCount = long.Parse(dtRow[6].ToString());
                        data.TotalTime = long.Parse(dtRow[7].ToString());
                        lsRet.Add(data);
                    }
                    return lsRet.ToArray();
                }
                return null;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Practice Data");
            }
            return null;
        }

        /// <summary>
        /// Lấy về 1 chuỗi trợ giúp từ bảng SPEECH trong CSDL.
        /// </summary>
        /// <param name="iStringID">ID của chuỗi.</param>
        /// <param name="getWavFile">muốn lấy về đường dẫn đến file wav hay là nội dung chuỗi trợ giúp?</param>
        /// <returns>tùy theo tham số getWavFile sẽ trả về đường dẫn đến file wav (getWavFile = TRUE)
        /// hoặc nội dung chuỗi trợ giúp trong CSDL (getWavFile = FALSE). Trả về <see cref="string.Empty"/>
        /// nếu không tìm thấy.</returns>
        public string GetSpeechEntry(int iStringID, bool getWavFile)
        {
            DataRow[] arrRows = m_dtSpeech.Select("ID = " + iStringID);
            if(arrRows.Length > 0)
            {
                return arrRows[0][getWavFile ? 2 : 1].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// Lấy về kiểu gõ đang sử dụng
        /// </summary>
        /// <param name="ModeCode">Biến tham số trả về kiểu gõ. 
        ///     1: VNI
        ///     2: Telex
        ///     3: NoMark
        /// </param>
        /// <returns>Chuỗi tên của kiểu gõ. Như "VNI" hay "Telex" hay "Không dấu".</returns>
        public string LoadCurrentTypeMode(ref int ModeCode)
        {
            return string.Empty;
        }

        #endregion
    }
}
