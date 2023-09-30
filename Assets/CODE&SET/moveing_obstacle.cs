using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveing_obstacle : MonoBehaviour {

	// Use this for initialization
    [SerializeField]Vector3 vector_for_move;
    [Range(0,1)]
    [SerializeField]
    float muserofmoving;
    Vector3 keep_position;
    [SerializeField]float period=2f;
    void Start () {
        keep_position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (period <=Mathf.Epsilon) { return; }
        float taw = Mathf.PI * 2f;
        float cycle = Time.time / period;
        float racycle = Mathf.Sin(cycle * taw);
        muserofmoving = racycle / 2f + 0.5f;
        Vector3 offset = vector_for_move * muserofmoving;
        transform.position = keep_position + offset;
	}
}
