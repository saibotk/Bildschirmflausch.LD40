using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    public AudioMixerSnapshot[] maintheme;
    public AudioMixerSnapshot lifttheme;
    public AudioSource elevatorpling;


    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;
    private int layerstate;
    private int score;

    private GameObject _GameControler;

    // Use this for initialization
    void Start() {
        m_TransitionIn = 1;
        m_TransitionOut = 1;
        layerstate = 0;
        score = 0;
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
                maintheme[layerstate].TransitionTo(m_TransitionOut);
            }
        }
    }

    void AddLayer()
    {
        if((score + 35) == _GameControler.GetComponent<GameController>().getScore() )
        {
            score = _GameControler.GetComponent<GameController>().getScore();
            layerstate++;
            maintheme[layerstate].TransitionTo(50);
        }
    }

	public void Pling(){
        elevatorpling.Play();
    }
}
