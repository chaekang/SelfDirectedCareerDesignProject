using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public BoxCollider2D[] area;

    private float timer;


    private void Update()
    {
        if (GameManager.instance.player.onSynapse)
        {
            timer += Time.deltaTime;

            if (timer > 0.1f)
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
        float posX = basePosition.x + Random.Range(-size.x * 3, size.x * 3);
        float posY = basePosition.y + Random.Range(-size.y * 3, size.y * 3);

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }

    void Spawn()
    {
        float curPoint = GameManager.instance.SynapseBar.curPoint;
        Vector2 spawnPosition = GetRandomPosition(area[0]);

        GameObject spawnedPrefab = GameManager.instance.pool.Get(GenerateRandomNumber(curPoint));
        spawnedPrefab.transform.position = spawnPosition;
    }

    void SpawnAfterCollide()
    {
        Vector2 spawnPosition = GetRandomPosition(area[1]);

        GameObject spawnedPrefab = GameManager.instance.pool.Get(GenerateRandomAfterCollide());
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

    int GenerateRandomAfterCollide()
    {
        int randomNumber = Random.Range(0, 2);
        return randomNumber + 2;
    }
}
