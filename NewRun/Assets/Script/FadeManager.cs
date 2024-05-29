using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    public FadeInOut fadeInOut; 
    private int gameScene = 2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
            NextSceneLoading(gameScene);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextSceneLoading(int sceneIndex)
    {
        fadeInOut.StartFadeOut(); 
        StartCoroutine(SceneLoad(sceneIndex));
    }

    private IEnumerator SceneLoad(int sceneIndex)
    {
        float waitTime = fadeInOut.GetFadeTime();  
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneIndex);  
        fadeInOut.StartFadeIn();  
    }
}
