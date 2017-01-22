using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Transform prefab;
    float maxX, maxY;
    float wave1X, wave1Y, wave2X, wave2Y;
    public float frequency = 0.5f, frequencyTimer = 0, hits = 2, actualHits =0;
    public float waitTime = 1f, waitTimer = 0;

    public float life = 100;

	// Use this for initialization
	void Start () {

        maxX = Screen.width;
        maxY = Screen.height;
        Debug.Log(" xxxx "+ Screen.width+ " xxx   "+Screen.height + " xxx   " +Screen.width / Screen.height);
        GetComponent<Camera>().orthographicSize = Screen.width / 4;
        GetComponent<Camera>().transform.position = new Vector3(Screen.width / 2, Screen.height / 2,-10);


        Instantiate(prefab, new Vector3(Screen.width, Screen.height), Quaternion.identity);
        Instantiate(prefab, new Vector3(0, 0), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {

#if UNITY_ANDROID
        for (var i = 0; i < Input.touchCount; ++i) {
        if (Input.GetTouch(i).phase == TouchPhase.Began) {
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
            // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
            if(hitInfo)
            {
                Destroy(hitInfo.transform.gameObject);
            }
        }
    }
#endif

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
            // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
            if (hitInfo)
            {
                Destroy(hitInfo.transform.gameObject);
            }
        }

        waitTimer += Time.deltaTime;

        if(waitTimer >= waitTime)
        {
            frequencyTimer += Time.deltaTime;
            if(frequencyTimer >= frequency && actualHits <= hits)
            {
                wave1X = Random.Range(0, maxX);
                wave2X = Random.Range(0, maxX);
                wave1Y = Random.Range(0, maxY);
                wave2Y = Random.Range(0, maxY);

                if(actualHits ==1)
                {
                    Instantiate(prefab, new Vector3(wave1X,wave1Y),Quaternion.identity);
                }
                if(actualHits == 2)
                {
                    Instantiate(prefab, new Vector3(wave2X, wave2Y), Quaternion.identity);
                }

                //Reset
                actualHits++;
                frequencyTimer = 0;
            }
            if(actualHits > hits)
            {
                //Reset
                actualHits = 0;
                waitTimer = 0;
                frequencyTimer = 0;
            }  
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Entro");
        RaycastHit2D hitInfo = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

        if (hitInfo != null)
        {
            Debug.Log("OKK" + hitInfo.rigidbody.gameObject.name);
        }

        if (hitInfo == null)
        {
            Debug.Log("NULL"+hitInfo.rigidbody.gameObject.name);
        }
    }
}
