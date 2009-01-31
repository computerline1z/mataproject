using System;
using System.Collections.Generic;
using System.Text;

namespace TypingBC.Business
{
    public class CPracticeData
    {
        private string m_sUserName;
        private DateTime m_tPraticeTime;
        private long m_lExerciseCount, m_lKeyCount, m_lFailKeyCount, m_lUsingHelpCount, m_lTotalTime;

        public string UserName
        {
            get { return m_sUserName; }
            set { m_sUserName = value; }
        }

        public DateTime PraticeTime
        {
            get { return m_tPraticeTime; }
            set { m_tPraticeTime = value; }
        }

        /// <summary>
        /// Số bài tập đã tập trong lần này
        /// </summary>
        public long ExerciseCount
        {
            get { return m_lExerciseCount; }
            set { m_lExerciseCount = value; }
        }

        /// <summary>
        /// tổng số phím đã gõ
        /// </summary>
        public long KeyCount
        {
            get { return m_lKeyCount; }
            set { m_lKeyCount = value; }
        }

        /// <summary>
        /// Ttổng sô phím sai đã gõ
        /// </summary>
        public long FailKeyCount
        {
            get { return m_lFailKeyCount; }
            set { m_lFailKeyCount = value; }
        }

        /// <summary>
        /// Tổng sô lần dùng help
        /// </summary>
        public long UsingHelpCount
        {
            get { return m_lUsingHelpCount; }
            set { m_lUsingHelpCount = value; }
        }

        /// <summary>
        /// Tổng thời gian của lần tập này.
        /// </summary>
        public long TotalTime
        {
            get { return m_lTotalTime; }
            set { m_lTotalTime = value; }
        }

        /// <summary>
        /// các hàm sau có thể viết tùy ý, muốn tính gì thì tính. Nhưng nhớ check lỗi devision by zero :|
        /// </summary>
        public float ErrorRate
        {
            get { return (float)(m_lFailKeyCount * 1.0 / m_lKeyCount); }
        }

        public float TimePerKey
        {
            get { return (float)(m_lTotalTime * 1.0 / m_lKeyCount); }
        }

        public float TimePerExercise
        {
            get { return (float)(m_lTotalTime * 1.0 / m_lExerciseCount); }
        }

        public CPracticeData()
        {

        }
    }
}
