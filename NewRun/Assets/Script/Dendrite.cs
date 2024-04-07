using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dendrite : MonoBehaviour
{
    float time = 0;
    void Update()
    {
        transform.localScale = new Vector3(0.07f, 0.07f, 1f) * (1 + time);
        time += Time.deltaTime;

        if (time > 3.5f)
        {
            transform.localScale = new Vector3(0.25f, 0.25f, 1f);
            GameManager.instance.player.appear = false;
        }

    }
}
