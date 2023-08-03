using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    public Animator transition;

    public float transitionTime = 1f;

    private void Awake()
    {
        Instance = this;
    }

    public enum Scene
    {
        SceneMainMenu,
        Tutorial,
        THE_GANTLET
    }
    
    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene("CutsceneDialogue");
    }

    //esto por si hay 2 o más niveles o escenas, etc
    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("SceneStart");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.SceneMainMenu.ToString());
    }

}
