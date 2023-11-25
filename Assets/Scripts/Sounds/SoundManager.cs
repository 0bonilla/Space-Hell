using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] musicSounds, sfxSounds;

    public AudioSource musicSource, playerSfxSource, enemySfxSource;

    private float timeToPlay = 0f;

    private void Awake()
    {
        if (Instance == null) 
            Instance = this;
        else 
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic("Background");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, s => s.name == name);
        if (s != null)
        {
            musicSource.clip = s.clip;
            musicSource.volume = s.volume;
            musicSource.mute = s.mute;
            musicSource.loop = s.loop;
            musicSource.Play();
        }
        else Debug.Log("Sound Not Found");

    }

    public void PlayPlayerSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, s => s.name == name);
        if (s != null)
        {
            playerSfxSource.clip = s.clip;
            playerSfxSource.Play();
        }
        else Debug.Log("Sound Not Found");

    }

    public void PlayEnemySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, s => s.name == name);

        Debug.Log(s.clip);
        if (s != null)
        {
            enemySfxSource.clip = s.clip;
            enemySfxSource.Play();
        }
        else Debug.Log("Sound Not Found");

    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        playerSfxSource.mute = !playerSfxSource.mute;
            
        enemySfxSource.mute = !enemySfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        playerSfxSource.volume = volume;

        enemySfxSource.volume = volume;
    }



    /*public void Stop(string name)
    {
        Sound s = Array.Find(soundList, s => s.name == name);

        if (s != null) s.Audio.Stop();
    }*/

    /*private bool canPlay(string name)
    {
        bool answer = false;

        Sound s = Array.Find(soundList, s => s.name == name);

        if (s.name == "walk")
        {
            float nexTimeToPlay = 1.032f;
            if (timeToPlay + nexTimeToPlay < Time.time)
            {
                timeToPlay = Time.time;
                answer = true;
            }
        }
        else if (s.name == "water")
        {
            float nexTimeToPlay = 1f;
            if (timeToPlay + nexTimeToPlay < Time.time)
            {
                timeToPlay = Time.time;
                answer = true;
            }
        }
        else if (s.name == "wind")
        {
            float nexTimeToPlay = 7f;
            if (timeToPlay + nexTimeToPlay < Time.time)
            {
                timeToPlay = Time.time;
                answer = true;
            }
        }

        return answer;
    }*/
}
