using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Dendrite dendrite;
    int somaSceneCount = 1;
    bool isListenerRegistered = false;
    private void Start()
    {
        if (!isListenerRegistered)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            isListenerRegistered = true;
        }
    }

    // GameStart : 0, SomaScene : 1, GameScene 1 : 2, GameScene 2 : 3
    public void BtnChangeScenefunc()
    {
        Debug.Log("BtnChangeScenefunc()");
        GameObject clickedObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Debug.Log(clickedObject.name);
        if (clickedObject != null)
        {
            switch (clickedObject.name)
            {
                case "StartBtn":
                    SceneManager.LoadScene(2);
                    break;

                case "HomeBtn":
                    SceneManager.LoadScene(0);
                    break;

                case "NextStageBtn":
                    GoToNextScene();
                    break;
            }
        }

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Soma Scene")
        {
            somaSceneCount++;
            Debug.Log("soma scene " + somaSceneCount);
        }
    }

    private void Update()
    {
        if(dendrite != null)
        {
            if (dendrite.isFinish)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void GoToNextScene()
    {
        int nextSceneIndex = somaSceneCount + 2; // 현재 씬의 다음 인덱스 계산

        // 다음 씬이 존재하는지 확인
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // 다음 씬으로 이동
        }
    }

    private void OnDestroy()
    {
        // 스크립트가 파괴될 때 이벤트 리스너 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
        isListenerRegistered = false;
    }
}
