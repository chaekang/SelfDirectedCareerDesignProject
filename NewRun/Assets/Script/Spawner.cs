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

            float randomTime = Random.Range(0.3f, 0.6f);

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
        Vector2 size = area.size;                   //box colider2d, 즉 맵의 크기 벡터

        //x, y축 랜덤 좌표 얻기
        float posX = basePosition.x + Random.Range(-size.x, size.x);
        float posY = basePosition.y + Random.Range(-size.y, size.y);

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }

    void Spawn()
    {
        float curPoint = GameManager.instance.SynapseBar.curPoint;
        Vector2 spawnPosition = GetRandomPosition(area);

        GameObject spawnedPrefab = GameManager.instance.pool.Get(GenerateRandomNumber(curPoint));
        spawnedPrefab.transform.position = spawnPosition;
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
