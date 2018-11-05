using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectFour_AudioManager : MonoBehaviour {
    public AudioSource musicSource;
    public AudioClip startMusicClip, gameplayMusicClip, endMusicClip;

    public void PlayMusic(ConnectFour_GameManager.ConnectFour_phases inputPhase)
    {
        if (!musicSource) return;
        Debug.Log("Loading audio clip...");
        AudioClip currClip = musicSource.clip;
        AudioClip nextClip = null;
        switch (inputPhase)
        {
            case ConnectFour_GameManager.ConnectFour_phases.start:
                nextClip = startMusicClip;
                break;
            case ConnectFour_GameManager.ConnectFour_phases.p1turn:
            case ConnectFour_GameManager.ConnectFour_phases.p2turn:
                nextClip = gameplayMusicClip;
                break;
            case ConnectFour_GameManager.ConnectFour_phases.end:
                nextClip = endMusicClip;
                break;
        }
        if (currClip != nextClip && nextClip != null)
        {
            Debug.Log("Setting clip!");
            musicSource.Stop();
            musicSource.clip = nextClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}