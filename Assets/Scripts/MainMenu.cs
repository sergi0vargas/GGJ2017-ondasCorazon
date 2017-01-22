using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public RectTransform menu;
    private Vector2 main,instructions;
    public RectTransform instrucciones;

	// Use this for initialization
	void Start () {
        menu = GetComponent<RectTransform>();
        main = new Vector2(0, 0);
        instructions = new Vector2((-Screen.width), 0);
        instrucciones.anchoredPosition = new Vector2((Screen.width), 0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("game");
    }

    public void Toggle(bool irMenu)
    {
        if (irMenu)
        {
            menu.anchoredPosition = main;
        }
        else
        {
            menu.anchoredPosition = instructions;
        }

    }

}
