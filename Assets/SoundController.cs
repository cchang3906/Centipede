using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource fireBulletSound;
    [SerializeField] AudioSource gameOverSound;
    [SerializeField] AudioSource lobbyMusic;
    [SerializeField] AudioSource shriekSound;
    [SerializeField] List<AudioSource> logHitSound;
    [SerializeField] AudioSource ahSound;
    [SerializeField] AudioSource playerHitSound;
    [SerializeField] AudioSource demogorgonWalkSound;
    [SerializeField] AudioSource demogorgonHit;
    [SerializeField] AudioSource uiClickSound;
    [SerializeField] AudioSource forestMusic;
    [SerializeField] AudioSource demobatSound;
    [SerializeField] AudioSource generatorOffSound;
    [SerializeField] AudioSource flashlightSound;
    [SerializeField] AudioSource vecnaSound;
    [SerializeField] AudioSource gainLifeSound;
    [SerializeField] AudioSource forestSounds;

    public void PlayPlayerDeath(bool shouldPlay)
    {
        if (shouldPlay)
        {
            deathSound.Play();
        }
        else
        {
            deathSound.Stop();
        }
    }

    public void PlayFireBullet(bool shouldPlay)
    {
        if (shouldPlay)
        {
            fireBulletSound.Play();
        }
        else
        {
            fireBulletSound.Stop();
        }
    }

    public void PlayGameOver(bool shouldPlay)
    {
        if (shouldPlay)
        {
            gameOverSound.Play();
        }
        else
        {
            gameOverSound.Stop();
        }
    }

    public void PLayForestMusic (bool shouldPlay)
    {
        if (shouldPlay)
        {
            forestMusic.Play();
        }
        else
        {
            forestMusic.Stop();
        }
    }

    public void PlayLobbyMusic(bool shouldPlay)
    {
        if (shouldPlay)
        {
            lobbyMusic.Play();
        }
        else
        {
            lobbyMusic.Stop();
        }
    }

    public void PlayShriek(bool shouldPlay)
    {
        if (shouldPlay)
        {
            shriekSound.Play();
        }
        else
        {
            shriekSound.Stop();
        }
    }

    public void PlayLogHit(int version)
    {
        logHitSound[version].Play();
    }

    public void PlayAh()
    {
        ahSound.Play();
    }

    public void PlayPlayerHit(bool shouldPlay)
    {
        if (shouldPlay)
        {
            playerHitSound.Play();
        }
        else
        {
            playerHitSound.Stop();
        }
    }

    public void PlayDemogorgonMove(bool shouldPlay)
    {
        if (shouldPlay)
        {
            demogorgonWalkSound.Play();
        }
        else
        {
            demogorgonWalkSound.Stop();
        }
    }

    public void PlayDemogorgonHit(bool shouldPlay)
    {
        if (shouldPlay)
        {
            demogorgonHit.Play();
        }
        else
        {
            demogorgonHit.Stop();
        }
    }

    public void PlayUIClick()
    {
        uiClickSound.Play();
    }

    public void PlayDemobat(bool shouldPlay)
    {
        if (shouldPlay)
        {
            demobatSound.Play();
        }
        else
        {
            demobatSound.Stop();
        }
    }

    public void PlayGeneratorOff()
    {
        generatorOffSound.Play();
    }

    public void PlayVecna(bool shouldPlay)
    {
        if (shouldPlay)
        {
            vecnaSound.Play();
        }
        else
        {
            vecnaSound.Stop();
        }
    }

    public void PlayFlashlight()
    {
        flashlightSound.Play();
    }

    public void PlayGainLife()
    {
        gainLifeSound.Play();
    }

    public void PlayForestAmbience(bool shouldPlay)
    {
        if (shouldPlay)
        {
            forestSounds.Play();
        }
        else
        {
            forestSounds.Stop();
        }
        
    }
}
