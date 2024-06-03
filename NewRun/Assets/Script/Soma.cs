using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soma : MonoBehaviour
{
    public DendriteManager dendriteManager;
    public Material SomaMaterial;
    public GameObject somaPlayer;
    private bool isIncreasingTransparency = false;
    public bool isFinish = false;

    private void Start()
    {
        if (SomaMaterial != null)
        {
            Color color = SomaMaterial.color;
            color.a = 155f / 255f;
            SomaMaterial.color = color;
        }
    }

    private void Update()
    {
        // isDisappear가 true일 때 투명도를 서서히 증가시킴
        if (dendriteManager.isDisappear && !isIncreasingTransparency)
        {
            StartCoroutine(IncreaseTransparencyOverTime(1.5f));
        }

        if (isFinish)
        {
            somaPlayer.SetActive(true);
        }
    }

    IEnumerator IncreaseTransparencyOverTime(float duration)
    {
        isIncreasingTransparency = true;
        float startTime = Time.time;
        float elapsedTime = 0f;
        Color startColor = SomaMaterial.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            SomaMaterial.color = Color.Lerp(startColor, targetColor, t);
            elapsedTime = Time.time - startTime;
            yield return null;
        }

        SomaMaterial.color = targetColor;
        isIncreasingTransparency = false;
        isFinish = true;
    }
}
