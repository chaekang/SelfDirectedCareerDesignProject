using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject panel;

    BoxCollider2D area;
    private float timer;
    public bool ntFinish = false;
    private bool panelActivated = false; // �г��� Ȱ��ȭ �Ǿ����� ���θ� �����ϴ� �÷���

    private void Awake()
    {
        area = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (GameManager.instance.changeScene.endStage && GameManager.instance.player.onSynapse)
        {
            if (!panelActivated) // �г��� Ȱ��ȭ���� ���� ��쿡�� �ڷ�ƾ ȣ��
            {
                StartCoroutine(ActivatePanelForDuration(1f));
                panelActivated = true; // �г� Ȱ��ȭ �÷��� ����
            }

            timer += Time.deltaTime;

            float randomTime = Random.Range(0.5f, 0.8f);

            if (timer > randomTime)
            {
                Spawn();
                timer = 0;
                ntFinish = true;
            }
        }
        else if (GameManager.instance.player.onSynapse && !ntFinish)
        {
            timer += Time.deltaTime;

            float randomTime = Random.Range(0.5f, 0.8f);

            if (timer > randomTime)
            {
                StartCoroutine(SpawnCoroutine());
                GameManager.instance.SynapseBar.curPoint = 0;
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

    private IEnumerator ActivatePanelForDuration(float duration)
    {
        if (panel != null)
        {
            panel.SetActive(true);
            yield return new WaitForSeconds(duration);
            panel.SetActive(false);
        }
    }
}
