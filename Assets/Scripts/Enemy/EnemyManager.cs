using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyHiveBrain hiveBrain;
    [SerializeField] private UFOManager ufoManager; 

    public static EnemyManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void StartRound()
    {
        hiveBrain.StartRound();
        StartCoroutine(ufoManager.UFOClock());
    }

    public void EndRound()
    {
        hiveBrain.StopAllCoroutines();
        ufoManager.StopAllCoroutines();
        Debug.Log("Space is now <b>purified.</b> Nobody came.");
    }
}
