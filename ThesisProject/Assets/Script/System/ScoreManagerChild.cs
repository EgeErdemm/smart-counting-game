using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagerChild : BaseAbstractScoreManager
{
    protected override void setScore(int index)
    {
        base.setScore(index);
        Debug.Log("overide worked");
        Debug.Log(index);
    }
}
