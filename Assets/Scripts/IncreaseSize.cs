using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSize : MonoBehaviour {

    public float destroyTime = 3;
    float Speed = .5f;

    public void Start()
    {
        Destroy(gameObject, destroyTime);
    }
	// Update is called once per frame
	void Update () {
        {
            transform.RotateAround(Vector3.forward, .2f);
            transform.localScale = new Vector3(transform.localScale.x + Speed, transform.localScale.y + Speed);
        }
	}
}
