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

        // ntFinish가 true가 된 직후에 한 번만 curPoint를 0으로 초기화
        if (GameManager.instance.spawner.ntFinish && !previousNtFinish)
        {
            GameManager.instance.SynapseBar.curPoint = 0;
        }

        // 현재 ntFinish 상태를 저장
        previousNtFinish = GameManager.instance.spawner.ntFinish;

        if (GameManager.instance.spawner.ntFinish && GameManager.instance.player.onSynapse)
        {
            timer += Time.deltaTime;

            // curPoint에 따라 randomTime을 조정
            float curPoint = GameManager.instance.SynapseBar.curPoint;

            // curPoint가 0보다 작거나 같으면 분비되는 nt가 없도록 함
            if (curPoint <= 0)
            {
                return;
            }

            float randomTime = Mathf.Lerp(0.2f, 1f, 1f - curPoint / 100f); // curPoint가 클수록 randomTime이 짧아짐

            if (timer > randomTime)
            {
                Spawn();
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

    void Spawn()
    {
        Vector2 spawnPosition = GetRandomPosition(area);

        GameObject spawnedPrefab = GameManager.instance.pool.Get(1);
        spawnedPrefab.transform.position = spawnPosition;
    }
}
