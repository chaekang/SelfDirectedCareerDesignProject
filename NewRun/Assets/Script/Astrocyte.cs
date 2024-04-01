using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astrocyte : MonoBehaviour
{
    Animator anim;
    bool isMoving;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.instance.player.isStart)
        {
            anim.SetTrigger("isApproach");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("NT"))
        {
            anim.SetBool("isMoving", true);
            isMoving = true;
            StartCoroutine(ResetIsMoving());
        }
    }
    private IEnumerator ResetIsMoving()
    {
        yield return new WaitForSeconds(0.6f);
        anim.SetBool("isMoving", false);
        isMoving = false;
    }
}
