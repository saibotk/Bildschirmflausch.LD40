using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{


    public AudioMixerSnapshot maintheme;
    public AudioMixerSnapshot lifttheme;
    public AudioSource elevatorpling;


    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;

    // Use this for initialization
    void Start() {
        m_TransitionIn = 1;
        m_TransitionOut = 1;

    }

    void OnTriggerEnter2D(Collider2D col) {

        if (col.CompareTag("Lift")) {
            if (col.isTrigger) {
                lifttheme.TransitionTo(m_TransitionIn);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Lift")) {
            if (col.isTrigger) {
                maintheme.TransitionTo(m_TransitionOut);
            }
        }
    }

    void Pling(){
        elevatorpling.Play();
    }
}
