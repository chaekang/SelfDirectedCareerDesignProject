using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SomaDendrite : MonoBehaviour
{
    public GameObject targetPosition;
    public DendriteManager dendriteManager;
    public float speed;
    public bool onSoma = false;  // �Ҹ� ���� �ö󰬴��� Ȯ��(���� �Ҹ� ų �� �ʿ�)

    Vector3 velo = Vector3.zero;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition.transform.position, ref velo, speed);

        if (dendriteManager.isOverlap)
        {
            // 1�� ���� ũ�⸦ ����
            StartCoroutine(ShrinkOverTime(1f));
        }
    }

    private IEnumerator ShrinkOverTime(float duration)
    {
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        dendriteManager.isDisappear = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Soma"))
        {
            onSoma = true;
        }
    }
}