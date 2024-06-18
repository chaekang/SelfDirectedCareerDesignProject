using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUp : MonoBehaviour
{
    BoxCollider2D area;
    public GameObject ntCling;

    private float timer;

    private void Awake()
    {
        area = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ntCling != null)
        {
            if (collision.gameObject.tag == "NT")
            {
                ntCling.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (!GameManager.instance.player.onSynapse && GameManager.instance.player.disappear)
        {
            ntCling.SetActive(false);
        }

        if (GameManager.instance.spawner.ntFinish && GameManager.instance.player.onSynapse)
        {
            timer += Time.deltaTime;

            // curPoint�� ���� randomTime�� ����
            float curPoint = GameManager.instance.SynapseBar.curPoint;

            // curPoint�� 0���� �۰ų� ������ �к�Ǵ� nt�� ������ ��
            if (curPoint <= 0)
            {
                return;
            }

            float randomTime = Mathf.Lerp(0.2f, 1f, 1f - curPoint / 100f); // curPoint�� Ŭ���� randomTime�� ª����

            if (timer > randomTime)
            {
                Spawn();
                timer = 0;
            }
        }
    }

    private Vector2 GetRandomPosition(BoxCollider2D area)
    {
        Vector2 basePosition = transform.position;  // ������Ʈ�� ��ġ

        // x, y�� ���� ��ǥ ���
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
            GameObject spawnedPrefab = GameManager.instance.pool.Get(1);
            spawnedPrefab.transform.position = spawnPosition;
        }
    }
}