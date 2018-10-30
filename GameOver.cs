using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    [SerializeField]
    private Button _tryAgainButton;
    [SerializeField]
    private Button _quitButton;
    [SerializeField]
    private GameObject _gameOverMenu;
    [SerializeField]
    private Text _gameOverText;
    // Use this for initialization
    void Start()
    {
        //Start time in case we come from time stopped game over
        Time.timeScale = 1;
        _tryAgainButton.onClick.AddListener(TryAgain);
        _quitButton.onClick.AddListener(QuitGame);
    }

    public void GameIsOver()
    {
        //Show menu
        _gameOverMenu.SetActive(true);
        //Stop time
        Time.timeScale = 0;
    }

    public void WonGame()
    {
      /*   //Change text to reflect success
        _gameOverText.text = "CONGRATULATIONS!";
        //Show menu
        _gameOverMenu.SetActive(true);
        //Stop time
        Time.timeScale = 0;*/
    }

    //Continue Button's method
    void TryAgain()
    {
        //Load the scene again
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Quit Button's method
    void QuitGame()
    {
        //Quit scene here
        Debug.Log("Quit game");
    }
}
