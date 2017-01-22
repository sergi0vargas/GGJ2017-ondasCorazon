using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSize : MonoBehaviour {

    public float destroyTime = 3;
    public float Speed = .8f;

    public void Start()
    {
        Destroy(gameObject, destroyTime);
    }
	// Update is called once per frame
	void Update () {
        {
            transform.localScale = new Vector3(transform.localScale.x + Speed, transform.localScale.y + Speed);
        }
	}
}
