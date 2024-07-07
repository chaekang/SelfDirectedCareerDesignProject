using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject panel;

    BoxCollider2D area;
    private float timer;
    public bool ntFinish = false;
    private bool panelActivated = false; // 패널이 활성화 되었는지 여부를 저장하는 플래그

    private void Awake()
    {
        area = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (GameManager.instance.changeScene.endStage && GameManager.instance.player.onSynapse)
        {
            if (!panelActivated) // 패널이 활성화되지 않은 경우에만 코루틴 호출
            {
                StartCoroutine(ActivatePanelForDuration(1f));
                panelActivated = true; // 패널 활성화 플래그 설정
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
