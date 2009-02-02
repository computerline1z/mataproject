using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.Presentation;

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

        public CExerciseSet[] LoadExSetList()
        {
            //TODO: load from database
            return null;
        }

        public CExercise[] LoadExerciseList(ExerciseSetType type)
        {
            //TODO: load from DB
            return null;
        }

        public int GetExSetInstruction(bool bBegin, ExerciseSetType type)
        {
            return 0;
        }

        public int GetExerciseInstruction(bool bBegin, int iExID)
        {
            return 0;
        }

        public CExercise LoadExercise(int iExID)
        {
            CExercise ex = new CExercise();
            //TODO: ex.... = ...
            return ex;
        }

        public string[] LoadUser()
        {
            //TODO: load from database
            return null;
        }

        public bool UpdateUser(string user)
        {
            //TODO: add code
            return true;
        }
    }
}
