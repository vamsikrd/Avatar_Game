using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject mainMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OptionMenu()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackButton()
    {
        optionMenu.GetComponent<Animator>().SetTrigger("clickedBack");
        Invoke("MainMenu", 0.8f);
    }

    private void MainMenu()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
    }
}
