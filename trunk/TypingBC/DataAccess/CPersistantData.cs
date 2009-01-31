using System;
using System.Collections.Generic;
using System.Text;
using TypingBC.Presentation;

namespace TypingBC.DataAccess
{
    public class CPersistantData
    {
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
    }
}
