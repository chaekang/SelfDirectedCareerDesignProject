using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Info : MonoBehaviour
{
    public Image[] info;
    public GameObject info_pannel;
    public GameObject exitBtn;

    int index = 0; //sprite의 배열에 대한 순서 첫 번째 0부터 시작

    public void Click_info_btn()
    {
        info[index].gameObject.SetActive(true);
        info_pannel.SetActive(true);
        exitBtn.SetActive(false); // info_pannel이 켜지면 exitBtn을 끔
    }

    public void Click_x_btn()
    {
        info[index].gameObject.SetActive(false);
        index = 0;
        info_pannel.SetActive(false);
        exitBtn.SetActive(true); // info_pannel이 꺼지면 exitBtn을 켬
    }

    public void Click_rightbtn()
    {
        if (info.Length > index + 1)
        {
            info[index].gameObject.SetActive(false);
            index++;
            info[index].gameObject.SetActive(true);
        }
        else if (info.Length == index + 1)
        {
            info[index].gameObject.SetActive(false);
            index = 0;
            info[index].gameObject.SetActive(true);
        }
    }

    public void Click_leftbtn()
    {
        if (0 < index)
        {
            info[index].gameObject.SetActive(false);
            index--;
            info[index].gameObject.SetActive(true);
        }
        else if (index == 0)
        {
            info[index].gameObject.SetActive(false);
            index = info.Length - 1;
            info[index].gameObject.SetActive(true);
        }
    }
}
