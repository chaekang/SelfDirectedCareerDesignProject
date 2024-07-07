using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    BoxCollider2D area;
    private float timer;
    public bool ntFinish = false;

    private void Awake()
    {
        area = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (GameManager.instance.changeScene.endStage && GameManager.instance.player.onSynapse)
        {
            timer += Time.deltaTime;

            float randomTime = Random.Range(0.5f, 0.8f);

            if (timer > randomTime)
            {
                Spawn();
                timer = 0;
                ntFinish = true;
            }
        }
        if (GameManager.instance.player.onSynapse && !ntFinish)
        {
            timer += Time.deltaTime;

            float randomTime = Random.Range(0.5f, 0.8f);

            if (timer > randomTime)
            {

                StartCoroutine(SpawnCoroutine());

                timer = 0;
            }
        }
    }

    private Vector2 GetRandomPosition(BoxCollider2D area)
    {
        Vector2 basePosition = transform.position;  // 오브젝트의 위치

        // x, y축 랜덤 좌표 얻기
        float posX = basePosition.x;
        float posY = basePosition.y;

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }

    IEnumerator SpawnCoroutine()
    {
        Spawn();
        yield return new WaitForSeconds(5f);
        ntFinish = true;
    }

    void Spawn()
    {
        Vector2 spawnPosition = GetRandomPosition(area);

        GameObject spawnedPrefab = GameManager.instance.pool.Get(0);
        spawnedPrefab.transform.position = spawnPosition;
    }
}