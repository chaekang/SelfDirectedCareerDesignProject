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
    public static bool isPoison = false;
    public bool isTutorial = false;
    public bool endStage = false;

    private void Start()
    {
        RegisterSceneLoadedListener();
        if (GameManager.instance.changeScene.isTutorial)
        {
            somaSceneCount = 0;
        }
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
    // 14.(Intro Anim) 15.(Poison Soma)
    public void BtnChangeScenefunc()
    {
        GameObject clickedObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (clickedObject != null)
        {
            switch (clickedObject.name)
            {
                case "StartBtn":
                    start();
                    break;

                case "HomeBtn":
                    goHome();
                    break;

                case "RestartBtn":
                    start();
                    break;

                case "NextStageBtn":
                    GameManager.instance.dendriteManager.nextStagebtn = true;
                    break;

                case "ChangeToPoisonMode":
                    SceneManager.LoadScene(5);
                    break;

                case "ChangeToBasicMode":
                    SceneManager.LoadScene(0);
                    break;

                case "GameExpBtn":
                    SceneManager.LoadScene(13);
                    break;
                case "StartBtn_Poison":
                    SceneManager.LoadScene(6);
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
        if (scene.name == "2. Soma Scene" || scene.name == "14. Poison Soma Scene")
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
        else if (scene.name == "6. PoisonStart")
        {
            Debug.Log("poison");
            isPoison = true;
        }
        else if (scene.name == "1. GameStart")
        {
            Debug.Log("basic");
            isPoison = false;
        }
        else if (scene.name == "5. GameScene 3" || scene.name == "9. PoisonStage 3")
        {
            endStage = true;
        }

        if (scene.name == "12. Tutorial")
        {
            isTutorial = true;
            GameManager.instance.player.canChangeDirection = false;
        }
        else
        {
            isTutorial = false;
        }
    }

    private void Update()
    {
        if (endStage && GameManager.instance.player.appear)
        {
            Debug.Log("End");
            SceneManager.LoadScene(12);

        }
        // �Ҹ������� �̵�
        if (!isPoison && dendrite != null && dendrite.isFinish && !dendrite.isEnd)
        {
            SceneManager.LoadScene(1);
        }
        else if (isPoison && dendrite != null && dendrite.isFinish && !dendrite.isEnd)
        {
            SceneManager.LoadScene(15);
        }
        // �Ҹ����� ���� ���������� �̵�
        if (GameManager.instance.dendriteManager != null)
        {
            if (GameManager.instance.dendriteManager.SomaPlayer)
            {
                GoToNextScene();
            }
        }

    }

    public void GoToNextScene()
    {
        // �Ϲݸ�� ����ȯ
        if (!isPoison && somaSceneCount <= 3)
        {
            int nextSceneIndex = somaSceneCount + 1; // ���� ���� ���� �ε��� ���
            // ���� ���� �����ϴ��� Ȯ��
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex); // ���� ������ �̵�
            }
        }
        // ����� ����ȯ
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
        // ��ũ��Ʈ�� �ı��� �� �̺�Ʈ ������ ����
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

    public void start()
    {
        if (!isPoison)
            SceneManager.LoadScene(2);
        else if (isPoison)
            SceneManager.LoadScene(6);
    }
    public void goHome()
    {
        if (!isPoison)
            SceneManager.LoadScene(0);
        else if (isPoison)
            SceneManager.LoadScene(5);
    }
}
