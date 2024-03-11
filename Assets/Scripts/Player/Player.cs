using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void Defeat()
    {
        ParticlesManager.instance.InstantiateParticles(ParticlesManager.instance.PlayerParticles(), transform.position);
        AudioManager.instance.PlayPlayerDefeated();
        GameManager.instance.OnPlayerDefeated();
        // TODO: Give time for death animation, then Signal Game Manager to switch to death screen.
    }

}
