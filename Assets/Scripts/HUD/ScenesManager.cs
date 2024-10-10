using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    public Animator transition;

    public float transitionTime = 1f;

    private bool ready2Activate = false;

    private void Awake()
    {
        Instance = this;
    }

    public enum Scene
    {
        SceneMainMenu,
        Tutorial,
        THE_GANTLET,
        MainMenuVS,
        VSTest

    }
    public void SetNextSceneName(string name)
    {
        PersistentGameInfo.Scene2Load = name;
    }
    
    public void LoadScene(Scene scene)
    {
        PersistentGameInfo.Scene2Load = scene.ToString();
        SceneManager.LoadScene("LoadingScreen");
    }

    public void LoadNewGame()
    {
        StartCoroutine(CutsceneTransition());

    }

    IEnumerator CutsceneTransition()
    {
        transition.SetTrigger("SceneStart");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("CutsceneNave");
        //SceneManager.LoadScene("BlablablaScene");
    } 

    //esto por si hay 2 o más niveles o escenas, etc
    public void LoadNextScene(string SceneName)
    {
        //Debug.Log(SceneManager.GetSceneByName(SceneName).IsValid());
        //Debug.Log(SceneName);
        StartCoroutine(LoadLevel(SceneName));
    }

    IEnumerator LoadLevel(string SceneName)
    {
        transition.SetTrigger("SceneStart");

        yield return new WaitForSeconds(transitionTime);

        PersistentGameInfo.Scene2Load = SceneName;
        SceneManager.LoadScene("LoadingScreen");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenuVS.ToString());
    }

    public void ReloadScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void LoadLevelAsync(bool ActivateScene = true)
    {
        StartCoroutine(LLAsync(ActivateScene));
    }

    IEnumerator LLAsync(bool ActivateScene = true)
    {
        
        AsyncOperation ass = SceneManager.LoadSceneAsync(PersistentGameInfo.Scene2Load);
        ass.allowSceneActivation = ActivateScene;
        //yield return new WaitUntil(() => ass.isDone == true);
        while (!ass.isDone)
        {
            if (!ass.allowSceneActivation)
            {
                ass.allowSceneActivation = ready2Activate;
            }
            yield return null;
        }

    }

    public void ActivateLoadedScene()
    {
        transition.SetTrigger("SceneStart");
        ready2Activate = true;
    }

}
