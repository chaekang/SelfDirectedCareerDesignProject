using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Dendrite dendrite;
    int previousSceneIndex;

    private void Start()
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // GameStart : 0, SomaScene : 1, GameScene 1 : 2, GameScene 2 : 3
    public void BtnChangeScenefunc()
    {
        switch (this.gameObject.name)
        {
            case "StartBtn":
                SceneManager.LoadScene(2);
                break;

            case "HomeBtn":
                SceneManager.LoadScene(0);
                break;

            case "NextStage":
                GoToNextScene();
                break;
        }
    }

    private void Update()
    {
        if (dendrite.isFinish)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void GoToNextScene()
    {
        int nextSceneIndex = previousSceneIndex + 1; // ���� ���� ���� �ε��� ���

        // ���� ���� �����ϴ��� Ȯ��
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // ���� ������ �̵�
        }
    }
}
