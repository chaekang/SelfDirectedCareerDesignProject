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

    // 0.(GameStart)    1.(SomaScene)     2.(GameScene1)    3.(GameScene2), 4.(GameScene3)
    // 5.(PoisonStart), 6.(PosionStage1), 7.(PosionStage2), 8.(PosionStage3)
    // 9.(GameOver),   10.(GameEnd),     11.(Tutorial)
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

                case "ChangeToPoisonMode":
                    SceneManager.LoadScene(5);
                    break;

                case "ChangeToBasicMode":
                    SceneManager.LoadScene(0);
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
        int nextSceneIndex = somaSceneCount + 2; // ���� ���� ���� �ε��� ���

        // ���� ���� �����ϴ��� Ȯ��
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // ���� ������ �̵�
        }
    }

    private void OnDestroy()
    {
        // ��ũ��Ʈ�� �ı��� �� �̺�Ʈ ������ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
        isListenerRegistered = false;
    }
}
