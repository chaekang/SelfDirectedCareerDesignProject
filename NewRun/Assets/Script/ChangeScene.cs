using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Dendrite dendrite;
    static int somaSceneCount = 1;
    static int poisonSomaScene = 1;
    bool isListenerRegistered = false;
    static bool isPoison = false;

    private void Start()
    {
        RegisterSceneLoadedListener();
    }

    private void RegisterSceneLoadedListener()
    {
        if (!isListenerRegistered)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            isListenerRegistered = true;
        }
    }

    // 0.(GameStart)    1.(SomaScene)     2.(GameScene1)    3.(GameScene2), 4.(GameScene3)
    // 5.(PoisonStart), 6.(PoisonStage1), 7.(PoisonStage2), 8.(PoisonStage3)
    // 9.(Over_Axon),  10.(Over_Vel0),   11.(Over_Syn)     12.(GameEnd),   13.(Tutorial)
    // 14.(Intro Anim) 15.(Soma Intro)
    public void BtnChangeScenefunc()
    {
        GameObject clickedObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (clickedObject != null)
        {
            switch (clickedObject.name)
            {
                case "StartBtn":
                    SceneManager.LoadScene(15);
                    break;

                case "HomeBtn":
                    SceneManager.LoadScene(0);
                    break;

                case "RestartBtn":
                    SceneManager.LoadScene(15);
                    break;

                case "NextStageBtn":
                    GoToNextScene();
                    break;

                case "ChangeToPoisonMode":
                    SceneManager.LoadScene(5);
                    break;

                case "ChangeToBasicMode":
                    SceneManager.LoadScene(0);
                    break;
            }
        }
    }

    void OnEnable()
    {
        RegisterSceneLoadedListener();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "2. Soma Scene")
        {
            if (!isPoison)
            {
                somaSceneCount++;
            }
            else
            {
                poisonSomaScene++;
            }
        }
        else if (scene.name == "7. PoisonStage 1")
        {
            Debug.Log("poison");
            isPoison = true;
        }
    }

    private void Update()
    {
        if (dendrite != null && dendrite.isEnd && dendrite.isFinish)
        {
            Debug.Log("End");
            SceneManager.LoadScene(11);

        }
        if (dendrite != null && dendrite.isFinish && !dendrite.isEnd)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void GoToNextScene()
    {
        if (!isPoison && somaSceneCount <= 3)
        {
            int nextSceneIndex = somaSceneCount + 1; // 현재 씬의 다음 인덱스 계산
            // 다음 씬이 존재하는지 확인
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex); // 다음 씬으로 이동
            }
        }
        if (isPoison && poisonSomaScene <= 3)
        {
            int nextPoisonScene = poisonSomaScene + 5;
            if(nextPoisonScene < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextPoisonScene);
            }
        }
    }

    private void OnDestroy()
    {
        // 스크립트가 파괴될 때 이벤트 리스너 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
        isListenerRegistered = false;
    }

    public void DeadPalayer(string exp)
    {
        if (exp == "Axon")
            SceneManager.LoadScene(9);
        else if (exp == "Vel")
            SceneManager.LoadScene(10);
        else if (exp == "Syn")
            SceneManager.LoadScene(11);
    }
}
