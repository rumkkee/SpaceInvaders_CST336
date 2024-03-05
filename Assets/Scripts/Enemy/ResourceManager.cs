using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    public GameObject leftWall;
    public GameObject rightWall;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
