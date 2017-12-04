using UnityEngine;
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
    private int layerstate;
    private bool endstate;
    private int floor;

    public GameObject _GameControler;

    // Use this for initialization
    void Start() {
        m_TransitionIn = 0.5f;
        m_TransitionOut = 1;
        layerstate = 1;
        floor = 0;
        endstate = false;
    }

    // Update is called once per frame
    void Update()
    {
        AddLayer();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Lift") && endstate == false) {
            if (col.isTrigger) {
                lifttheme.TransitionTo(m_TransitionIn);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.CompareTag("Lift") && endstate == false) {
            if (col.isTrigger) {
                maintheme[layerstate].TransitionTo(m_TransitionOut);
            }
        }
    }

    public void gameoverplay() {
        if (endstate == false) {
            end.TransitionTo(0f);
            gameovers.Play();
            endstate = true;
        }    
    }

    void AddLayer() {
        if(layerstate < 4 && floor < _GameControler.GetComponent<GameController>().GetFloor()) {
            floor = _GameControler.GetComponent<GameController>().GetFloor();
            layerstate++;   
            maintheme[layerstate].TransitionTo(7);
        }
    }
    /* SFX List
     * 0 : Elevator
       1 : Coffe Danger
       2 : Job accomplished
       3 : Job failed */
	public void sfxplay(int sound){
        if(endstate == false)
            soundeffects[sound].Play();
    }
}
