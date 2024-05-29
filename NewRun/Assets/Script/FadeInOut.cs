using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public AnimationCurve _fadeCurve;
    public Image _image;
    private float _fadeTime = 1f;
    private float currentTime = 0;
    private float percent = 0;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);  // 씬 전환 시 파괴되지 않도록 설정
    }
    public float GetFadeTime()
    {
        return _fadeTime;
    }

    public void StartFadeIn()
    {
        Debug.Log("Fade In");
        currentTime = 0;
        StartCoroutine(Fade(1, 0));
    }

    public void StartFadeOut()
    {
        Debug.Log("Fade Out");
        currentTime = 0;
        StartCoroutine(Fade(0, 1));
    }

    private IEnumerator Fade(float start, float end)
    {
        while (currentTime < _fadeTime)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / _fadeTime;

            Color color = _image.color;
            color.a = Mathf.Lerp(start, end, _fadeCurve.Evaluate(percent));
            _image.color = color;

            yield return null;
        }
    }
}
