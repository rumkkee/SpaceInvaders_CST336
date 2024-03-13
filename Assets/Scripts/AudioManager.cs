using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource playerFire;
    [SerializeField] private AudioSource enemyFire;

    [SerializeField] private AudioSource enemyDefeated;
    [SerializeField] private AudioSource playerDefeated;

    [SerializeField] private AudioSource inGameMusic;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayPlayerFire()
    {
        playerFire.Play();
    }

    public void PlayEnemyFire()
    {
        enemyFire.Play();
    }

    public void PlayEnemyDefeated()
    {
        enemyDefeated.Play();
    }

    public void PlayPlayerDefeated()
    {
        playerDefeated.Play();
    }

    public void PlayMusic()
    {
        inGameMusic.Play();
    }
}


