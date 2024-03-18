using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private BoxCollider2D area;

    private float timer;

    private void Start()
    {
        area = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (GameManager.instance.player.isSynapse)
        {
            timer += Time.deltaTime;

            if (timer > 0.1f)
            {
                Spawn();
                timer = 0;
            }
        }
    }

    private Vector2 GetRandomPosition()
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
        Vector2 spawnPosition = GetRandomPosition();

        GameObject spawnedPrefab = GameManager.instance.pool.Get(GenerateRandomNumber(curPoint));
        spawnedPrefab.transform.position = spawnPosition;
    }

    int GenerateRandomNumber(float curPoint)
    {
        int randomNumber = Random.Range(0, 100);
        int returnNum = 0;

        if (randomNumber < curPoint - 20)
        {
            returnNum = 0;
        }
        else
        {
            returnNum = 1;
        }
        return returnNum;
    }
}
