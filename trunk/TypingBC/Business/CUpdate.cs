using System;
using System.Collections.Generic;
using System.Text;

namespace TypingBC.Business
{
    public class CUpdate
    {
        private int m_iUpdateInterval;
        private bool m_bAutoUpdate;

        public int UpdateInterval
        {
            get { return m_iUpdateInterval; }
            set { m_iUpdateInterval = value; }
        }

        public bool AutoUpdate
        {
            get { return m_bAutoUpdate; }
            set { m_bAutoUpdate = value; }
        }

        public void GetData()
        {
            //TODO: 8-x
        }
    }
}
