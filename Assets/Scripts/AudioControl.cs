using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{
    public AudioMixerSnapshot[] maintheme;
    public AudioMixerSnapshot lifttheme;
    public AudioSource elevatorpling;


    private float m_TransitionIn;
    private float m_TransitionOut;
    private int layerstate;
    private int score;

    public GameObject _GameControler;

    // Use this for initialization
    void Start() {
        m_TransitionIn = 0.5f;
        m_TransitionOut = 1;
        layerstate = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AddLayer();
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
        if(layerstate < 4 && (score + 35) <= _GameControler.GetComponent<GameController>().getScore())
        {
            score = _GameControler.GetComponent<GameController>().getScore();
            layerstate++;   
            maintheme[layerstate].TransitionTo(7);
        }
    }

	public void Pling(){
        elevatorpling.Play();
    }
}
