using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
	public AudioMixer mixer;
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
        m_TransitionIn = 1.2f;
        m_TransitionOut = 0.3f;
        endstate = false;
        AddLayer(0);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Liftmusic") && endstate == false) {
            if(col.isTrigger)
                StartCoroutine("ChangeBGM");
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Liftmusic") && endstate == false) {
            if (col.isTrigger) {
                ChangeToMain();
                StopCoroutine("ChangeBGM");
            }    
        }
    }

    

    private IEnumerator ChangeBGM() {
        yield return new WaitForSeconds(2);
        ChangeToLift();
    }

    public void GameOverPlay() {
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
    public void SfxPlay(int sound) {
            soundeffects[sound].Play();
    }

    public void SfxStop(int sound) {
            soundeffects[sound].Stop();
    }
    public bool SfxPlaying(int sound) {
        return soundeffects[sound].isPlaying;
    }

    public void ChangeToLift()
    {
        lifttheme.TransitionTo(m_TransitionIn);
    }

    public void ChangeToLift(float trans)
    {
        lifttheme.TransitionTo(trans);
    }

    public void ChangeToMain()
    {
        maintheme[floor].TransitionTo(m_TransitionOut);
    }

    public void SetMasterVolume(float nvol) {
		mixer.SetFloat("masterVolume", Mathf.Clamp (nvol, -80f, 20f));
	}
}
