using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DendriteManager : MonoBehaviour
{
    public List<GameObject> dendrites;
    public bool isOverlap = false;  // 5개가 다 모였는지 확인
    public bool isDisappear =false; // 전기신호가 사라졌는지 확인
    public bool SomaPlayer = false; // 액션포텐셜이 사라졌는지 확인
    public bool nextStagebtn = false;

    private void Start()
    {
        dendrites[0].gameObject.SetActive(true);
    }

    private void Update()
    {

        dendrites[0].GetComponent<SomaDendrite>();
        dendrites[1].gameObject.SetActive(true);
        dendrites[2].gameObject.SetActive(true);
        dendrites[3].gameObject.SetActive(true);
        dendrites[4].gameObject.SetActive(true);


        if (dendrites[dendrites.Count - 1].transform.position.x >= 148.95)
        {
            isOverlap = true;
        }
    }
}
