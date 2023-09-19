using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] Button _newGame;

    void Start()
    {
        _newGame.onClick.AddListener(StartNewGame);
    }

    private void StartNewGame()
    {
        ScenesManager.Instance.LoadNewGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown("3"))
        {
            ScenesManager.Instance.LoadNextScene("Level3");
        }
    }

    public void ExitGame()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
