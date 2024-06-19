using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStageSpawner : MonoBehaviour
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
        Vector2 basePosition = transform.position;  //������Ʈ�� ��ġ

        //x, y�� ���� ��ǥ ���
        float posX = basePosition.x;
        float posY = basePosition.y;

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }

    void Spawn()
    {
        float curPoint = GameManager.instance.SynapseBar.curPoint;
        Vector2 spawnPosition = GetRandomPosition(area);
        GameObject spawnedPrefab = GameManager.instance.pool.Get(0);
        spawnedPrefab.transform.position = spawnPosition;
    }
}
