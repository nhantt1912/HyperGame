using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDefine 
{
    public struct OnBackHome{}
    
    public struct OnSelectLevel
    {
        public int levelId;
    }
    
    public struct OnLevelComplete
    {
        public int levelId;
    }
    
    public struct OnLevelFail
    {
        public int levelId;
    }
    
}
