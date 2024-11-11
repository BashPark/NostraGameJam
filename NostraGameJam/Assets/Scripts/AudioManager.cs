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
    public AudioClip menuAudio;
    public AudioClip hoverAudio;
    public AudioClip clickAudio;
    public AudioClip deathAudio;
    public AudioClip winAudio;


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

        if (SceneManager.GetActiveScene().name == "0.0MainMenu"|| SceneManager.GetActiveScene().name == "0.2Tutorial"|| SceneManager.GetActiveScene().name == "0.5Credits")
        {
            AudioManager.instance.PlayMusic(menuAudio);
           
        }
        else if (SceneManager.GetActiveScene().name == "1Level")
        {
            AudioManager.instance.PlayMusic(Level1);
        }
        else if (SceneManager.GetActiveScene().name == "2Level" )
        {
            AudioManager.instance.PlayMusic(Level2);
        }
        else if (SceneManager.GetActiveScene().name == "3Level")
        {
            AudioManager.instance.PlayMusic(Level3);
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