using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    static string _nextScene;
    [SerializeField]
    private Slider _loadingBar;
   

    public static void LoadScene(string sceneName)
    {
        _nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(_nextScene);
        ao.allowSceneActivation = false;

        float _temp = 0f;
        while (!ao.isDone)
        {
            yield return null;

            if (ao.progress < 0.9f)
            {
                _loadingBar.value = ao.progress;
                //ao.allowSceneActivation = true;
            }
            else
            {

                _temp += Time.unscaledDeltaTime;
                _loadingBar.value = Mathf.Lerp(0.9f, 1f, _temp);

                if (_loadingBar.value >= 1f)
                {
                    ao.allowSceneActivation = true;

                    yield break;
                }

            }


        }


    }


}
