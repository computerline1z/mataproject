using System;
using System.Collections.Generic;
using System.Text;

namespace TypingBC.Business
{
    public class CPracticeData
    {
        private string m_sUserName;
        private DateTime m_tPraticeTime;
        private float m_fErrorRate, m_fAverageTime, m_fUsingHelpTime;

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
        /// tỉ lệ lỗi: số phím sai / tổng số phím đã gõ (??)
        /// </summary>
        public float ErrorRate
        {
            get { return m_fErrorRate; }
            set { m_fErrorRate = value; }
        }

        /// <summary>
        /// thời gian trung bình cho 1 phím: tổng thời gian (giây) / tổng số phím đã gõ.
        /// </summary>
        public float AverageTime
        {
            get { return m_fAverageTime; }
            set { m_fAverageTime = value; }
        }

        /// <summary>
        /// TODO: cái này tính sao đây???
        /// Chẳng lẽ: số lần giúp đỡ/số bài tập đã làm?
        /// </summary>
        public float UsingHelpTime
        {
            get { return m_fUsingHelpTime; }
            set { m_fUsingHelpTime = value; }
        }

        public CPracticeData()
        {

        }
    }
}
