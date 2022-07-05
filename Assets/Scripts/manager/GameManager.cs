using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int curCherryNum;

    private int curGemNum;

    public int CurCherryNum
    {
        get { return curCherryNum; }
        set { curCherryNum = value; }
    }

    public int CurGemNum
    {
        get { return curGemNum; }
        set { curGemNum = value; }
    }


}
