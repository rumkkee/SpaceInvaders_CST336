using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAdvancePanel : MonoBehaviour
{
    public static ScoreAdvancePanel instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
