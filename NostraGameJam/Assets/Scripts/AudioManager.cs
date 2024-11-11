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
    public AudioClip eatAudio;
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

        // Ensure this script runs before any other audio script on scene load
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the scene name and play the correct music
        if (scene.name == "0.0MainMenu" || scene.name == "0.2Tutorial" || scene.name == "0.5Credits")
        {
            AudioManager.instance.PlayMusic(menuAudio);
        }
        else if (scene.name == "1Level")
        {
            musicSource.Stop();
            AudioManager.instance.PlayMusic(Level1);
        }
        else if (scene.name == "2Level")
        {
            musicSource.Stop();
            AudioManager.instance.PlayMusic(Level2);
        }
        else if (scene.name == "3Level")
        {
            musicSource.Stop();
            AudioManager.instance.PlayMusic(Level3);
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

    private void OnDestroy()
    {
        // Unsubscribe to avoid errors if this object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}