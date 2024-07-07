using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUp : MonoBehaviour
{
    BoxCollider2D area;
    public GameObject ntCling;

    private float timer;
    private bool previousNtFinish = false;

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
            if (ntCling != null)
            {
                ntCling.SetActive(false);
            }
        }

        // ntFinish�� true�� �� ���Ŀ� �� ���� curPoint�� 0���� �ʱ�ȭ
        if (GameManager.instance.spawner.ntFinish && !previousNtFinish)
        {
            GameManager.instance.SynapseBar.curPoint = 0;
        }

        // ���� ntFinish ���¸� ����
        previousNtFinish = GameManager.instance.spawner.ntFinish;

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
        Vector2 spawnPosition = GetRandomPosition(area);

        GameObject spawnedPrefab = GameManager.instance.pool.Get(1);
        spawnedPrefab.transform.position = spawnPosition;
    }
}
