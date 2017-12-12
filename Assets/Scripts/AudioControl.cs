﻿using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    public AudioMixerSnapshot[] maintheme;
    public AudioMixerSnapshot lifttheme;
    public AudioMixerSnapshot end;
    public AudioSource gameovers;
    public AudioSource[] soundeffects;


    private float m_TransitionIn;
    private float m_TransitionOut;
    private int floor = 0;
    private bool endstate;

    public GameObject _GameControler;

    // Use this for initialization
    void Start() {
        m_TransitionIn = 0.3f;
        m_TransitionOut = 0.3f;
        endstate = false;
        AddLayer(0);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Liftmusic") && endstate == false) {
            if (col.isTrigger) {
                lifttheme.TransitionTo(m_TransitionIn);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Liftmusic") && endstate == false) {
            if (col.isTrigger) 
                maintheme[floor].TransitionTo(m_TransitionOut);
            
        }
    }

    public void gameoverplay() {
        if (endstate == false) {
            end.TransitionTo(0f);
            gameovers.Play();
            endstate = true;
        }
    }

    public void AddLayer(int floor) {
        maintheme[floor].TransitionTo(1);
        this.floor = floor;
    }

    /* SFX List
     * 0 : Elevator
       1 : Coffe Danger
       2 : Job getto
       3 : Job accomplished
       4 : Job failed */
    public void sfxplay(int sound) {
            soundeffects[sound].Play();
    }

    public void sfxstop(int sound) {
            soundeffects[sound].Stop();
    }
    public bool sfxplaying(int sound) {
        return soundeffects[sound].isPlaying;
    }
}
