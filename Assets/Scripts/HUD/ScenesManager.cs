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
        StartCoroutine(CutsceneTransition());

    }

    IEnumerator CutsceneTransition()
    {
        transition.SetTrigger("SceneStart");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("CutsceneDialogue");
    } 

    //esto por si hay 2 o más niveles o escenas, etc
    public void LoadNextScene(string SceneName)
    {
        //Debug.Log(SceneManager.GetSceneByName(SceneName).IsValid());
        //Debug.Log(SceneName);
        StartCoroutine(LoadLevel(SceneName));
    }
    public string GetSceneCurrentName()
    {
        return SceneManager.GetActiveScene().name;
    }

    IEnumerator LoadLevel(string SceneName)
    {
        transition.SetTrigger("SceneStart");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.SceneMainMenu.ToString());
    }

    public void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

}
