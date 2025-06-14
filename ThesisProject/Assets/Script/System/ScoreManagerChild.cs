using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagerChild : BaseAbstractScoreManager
{
    public ScoreManagerChild(ILevelDataProvider dataProvider) : base(dataProvider)
    {
       
    }


    protected override void setScore(int index)
    {
        base.setScore(index);
        Debug.Log(index);
    }
}
