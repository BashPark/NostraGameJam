using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public AudioSource musicSource;
    public AudioSource SFXSource;
    public AudioSource Ambiance;

    public AudioClip pickupAudio;
    public AudioClip woodcutAudio;
    public AudioClip miningAudio;
    public AudioClip Level1;
    public AudioClip Level2;
    public AudioClip Level3;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);

        }

        DontDestroyOnLoad(this);

        Debug.Log(SceneManager.GetActiveScene().buildIndex.CompareTo(2));

        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 7)
        {
            musicSource.Stop();
        }

        playAmbiance();

        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1)
        {
            //AudioManager.instance.PlayMusic(Menu);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            //AudioManager.instance.PlayMusic(tutLVL);
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 3 && SceneManager.GetActiveScene().buildIndex <= 5)
        {
            //AudioManager.instance.PlayMusic(normalLVL);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            //AudioManager.instance.PlayMusic(bossLVL);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            //AudioManager.instance.PlayMusic(win);
        }

    }

    public void PlayClip(AudioClip Clip, bool random, float vol)
    {
        if (random)
        {
            RandomizeSound();
        }

        SFXSource.volume = vol;
        SFXSource.PlayOneShot(Clip);

    }

    private void RandomizeSound()
    {

        SFXSource.pitch = Random.Range(0.8f, 1.0f);
    }

    public void PlayMusic(AudioClip Clip)
    {
        musicSource.clip = Clip;
        musicSource.Play();

    }

    public void PlayUi(AudioClip Clip)
    {

        SFXSource.PlayOneShot(Clip);

    }


    public void playAmbiance()
    {
        //Ambiance.clip = Cricketambiance;

        if (SceneManager.GetActiveScene().buildIndex.CompareTo(2) == 0)
        {

            AudioManager.instance.Ambiance.Play();
        }

    }

}