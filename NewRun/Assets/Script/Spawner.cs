using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    BoxCollider2D area;

    private float timer;

    private void Awake()
    {
        area = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (GameManager.instance.player.onSynapse)
        {
            timer += Time.deltaTime;

            float randomTime = Random.Range(0.5f, 0.8f);

            if (timer > randomTime)
            {
                Spawn();
                timer = 0;
            }
        }
    }

    private Vector2 GetRandomPosition(BoxCollider2D area)
    {
        Vector2 basePosition = transform.position;  //오브젝트의 위치

        //x, y축 랜덤 좌표 얻기
        float posX = basePosition.x;
        float posY = basePosition.y;

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }

    void Spawn()
    {
        float curPoint = GameManager.instance.SynapseBar.curPoint;
        Vector2 spawnPosition = GetRandomPosition(area);
        if (GameManager.instance.changeScene.endStage)
        {
            GameObject spawnedPrefab = GameManager.instance.pool.Get(0);
            spawnedPrefab.transform.position = spawnPosition;
        }
        else
        {
            GameObject spawnedPrefab = GameManager.instance.pool.Get(GenerateRandomNumber(curPoint));
            spawnedPrefab.transform.position = spawnPosition;
        }
    }

    int GenerateRandomNumber(float curPoint)
    {
        int randomNumber = Random.Range(0, 100);
        int returnNum = 0;

        if (curPoint < 50)
        {
            if (randomNumber < curPoint)
            {
                returnNum = 0;
            }
            else
            {
                returnNum = 1;
            }
        }
        else
        {
            if (randomNumber < curPoint - 10)
            {
                returnNum = 0;
            }
            else
            {
                returnNum = 1;
            }
        }

        return returnNum;
    }
}
