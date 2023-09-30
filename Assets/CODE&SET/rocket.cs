using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour {
    enum State{alive,dead,traspot};
    State state = State.alive;
    Rigidbody MOVE;
    [SerializeField]float fuel = 100;
    AudioSource voice;
    bool collsiondisablity=false;
    [SerializeField]float powerofmotor=100f;
    [SerializeField]float powerofrotate=100f;
    [SerializeField]AudioClip audio;
    [SerializeField] ParticleSystem win;
    [SerializeField] ParticleSystem deaf;
    [SerializeField]ParticleSystem fly;
	// Use this for initialization
	void Start () {
        MOVE = GetComponent<Rigidbody>();
        voice = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.alive)
        {
            if (fuel > 0)
            {
                functioning();
            }
        }
        if (Debug.isDebugBuild)
        {
            respondTodeBug();
        }
    }
    void respondTodeBug()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            state = State.traspot;
            showscreen();
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            collsiondisablity = !collsiondisablity;
        }
    }

    void OnCollisionEnter(Collision collision) {
        int level = SceneManager.GetActiveScene().buildIndex;
        if (state != State.alive||collsiondisablity)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "resting":
                state = State.traspot;
                    win.Play();
                    Invoke("showscreen", 1f);
                break;
            case"dead":
                fly.Stop();
                voice.Stop();
                state = State.dead;
                deaf.Play();
                Invoke("showscreen", 1f);
                break;
        }    
    }
    void showscreen()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        if (state == State.traspot)
        {
            if (level < SceneManager.sceneCountInBuildSettings-1)
            {
                level++;
            }
            else
            {
                level = 0;
            }
        }
        else if (state == State.dead)
        {
            level = 0;
        }
        SceneManager.LoadScene(level);   
    }
     void functioning(){
            float motormove = powerofmotor * Time.deltaTime;
        float motorrotate = powerofrotate * Time.deltaTime;

            if (Input.GetKey(KeyCode.Space))
            {
                fuel = fuel - 0.5f;
                fly.Play();
                MOVE.AddRelativeForce(Vector3.up * motormove);
                if (!voice.isPlaying) 
                { 
                    voice.PlayOneShot(audio); 
                }
            } else {
                fly.Stop();
                voice.Stop();
            }
            MOVE.freezeRotation = true;
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward * motorrotate);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward * motorrotate);
            }
            MOVE.freezeRotation = false;
        }
      }


