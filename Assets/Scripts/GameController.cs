using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    /*WAVES*/
    public Transform prefab;
    float maxX, maxY;
    float wave1X, wave1Y, wave2X, wave2Y;
    public float frequency = 0.5f, frequencyTimer = 0, hits = 2, actualHits =0;
    /*WAVES*/

    /*GameLoop*/
    public float timeFromLastClic = 0;
    public float maxTimeBetweenClics = 0.5f;
    /*GameLoop*/

    /*UI*/
    public RectTransform menu;
    public Text finalText;

    public Text timerSprite;
    public int timer = 30;
    float timerCorazon = 0, timerTemporizador = 0;

    public float life = 100;

    public Image corazonSprite;
    public Text porcentajeVida;
    public float vidaTotal = 1f;
    bool isReducingLife = false;
    /*UI*/
    
    void Start () {
        finalText.text = "You Lose!";
        maxX = Screen.width;
        maxY = Screen.height;
        GetComponent<Camera>().orthographicSize = Screen.width / 4;
        GetComponent<Camera>().transform.position = new Vector3(Screen.width / 2, Screen.height / 2,-10);
        menu.gameObject.SetActive(false);
    }
	
	void Update () {

        WaveGenerator();
        GameInput();
        UIUpdate();
    }

    public void UIUpdate()
    {
        timerCorazon += Time.deltaTime;
        if (timerCorazon >= .3f)
        {
            if (!isReducingLife)
            {
                if (vidaTotal <= 1)
                {
                    vidaTotal += 0.1f;
                }
            }
            else
            {
                if (vidaTotal > 0)
                {
                    vidaTotal -= 0.1f;
                    corazonSprite.fillAmount = vidaTotal;
                    SetPercentage();
                }
                else
                {
                    finalText.text = "You Lose!";
                    Time.timeScale = 0;
                    DestroyAllWaves();
                }
            }
            timerCorazon = 0; ;
        }
        timerTemporizador += Time.deltaTime;
        if (timerTemporizador >= 1)
        { 
            if (timer == 0)
            {
                if (vidaTotal >= 0.5f)
                {
                    finalText.text = "You Win!!!";
                }
                Time.timeScale = 0;
                DestroyAllWaves();
            }
            else
            {
                if(timer == 20)
                {
                    frequency = 0.2f;
                }
                if (timer == 10)
                {
                    frequency = 0.1f;
                }
                timer--;
                timerSprite.text = timer.ToString();
                timerTemporizador = 0;
            }
        }
    }

    void SetPercentage()
    {
        float temp = vidaTotal *100;
        porcentajeVida.text = temp.ToString("F0") + "%";
    }

    void WaveGenerator()
    {
        frequencyTimer += Time.deltaTime;
        if (frequencyTimer >= frequency && actualHits <= hits)
        {
            wave1X = Random.Range(0, maxX);
            wave2X = Random.Range(0, maxX);
            wave1Y = Random.Range(0, maxY);
            wave2Y = Random.Range(0, maxY);

            if (actualHits == 1)
            {
                Instantiate(prefab, new Vector3(wave1X, wave1Y), Quaternion.identity);
            }
            if (actualHits == 2)
            {
                Instantiate(prefab, new Vector3(wave2X, wave2Y), Quaternion.identity);
            }

            //Reset
            actualHits++;
            frequencyTimer = 0;
        }
        if (actualHits > hits)
        {
            //Reset
            actualHits = 0;
            frequencyTimer = 0;
        }
    }

    void GameInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        timeFromLastClic += Time.deltaTime;
        if(timeFromLastClic >= maxTimeBetweenClics)
        {
            isReducingLife = true;
        }
#if UNITY_ANDROID
        for (var i = 0; i < Input.touchCount; ++i) {
            if (Input.GetTouch(i).phase == TouchPhase.Began) {
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
                // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
                if(hitInfo)
                {
                    timeFromLastClic = 0;
                    isReducingLife=false;
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
                timeFromLastClic = 0;
                isReducingLife = false;
                Destroy(hitInfo.transform.gameObject);
            }
        }
    }

    void DestroyAllWaves()
    {
        menu.gameObject.SetActive(true);
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Waves");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("game");
    }
}
