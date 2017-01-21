using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


    float maxX, maxY;
    float wave1X, wave1Y, wave2X, wave2Y;
    public float frequency = 0.5f, frequencyTimer = 0, hits = 2, actualHits =0;
    public float waitTime = 1f, waitTimer = 0;

	// Use this for initialization
	void Start () {

        maxX = Screen.width;
        maxY = Screen.height;
        

    }
	
	// Update is called once per frame
	void Update () {

        waitTimer += Time.deltaTime;

        if(waitTimer >= waitTime)
        {
            frequencyTimer += Time.deltaTime;
            if(frequencyTimer >= frequency && actualHits < hits)
            {
                wave1X = Random.Range(0, maxX);
                wave2X = Random.Range(0, maxX);
                wave1Y = Random.Range(0, maxY);
                wave2Y = Random.Range(0, maxY);
                hits++;
                frequencyTimer = 0;
                Debug.Log("golpe");
            }
            

        }
    }
}
