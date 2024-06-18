using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Info : MonoBehaviour
{
    public Image[] info;
    public GameObject info_pannel;

    int index = 0; //sprite의 배열에대한 순서 첫번째 0부터시작
    void Start()
    {

    }


    void Update()
    {

    }

    public void Click_info_btn()
    {
        info[index].gameObject.SetActive(true);
        info_pannel.SetActive(true);
    }
    public void Click_x_btn()
    {
        info[index].gameObject.SetActive(false);
        index = 0;
        info_pannel.SetActive(false);
    }
    public void Click_rightbtn()
    {
        Debug.Log("right click " + index);
        if(info.Length > index+1)
        {
            info[index].gameObject.SetActive(false);
            index++;
            info[index].gameObject.SetActive(true);
        }
        else if (info.Length == index+1) 
        {
            info[index].gameObject.SetActive(false);
            index = 0;
            info[index].gameObject.SetActive(true);
        }
    }
    public void Click_leftbtn()
    {
        Debug.Log("left click " + index);
        if (0 < index)
        {
            info[index].gameObject.SetActive(false);
            index--;
            info[index].gameObject.SetActive(true);
        }
        else if (index == 0)
        {
            info[index].gameObject.SetActive(false);
            index = info.Length-1;
            info[index].gameObject.SetActive(true);
        }
    }
}
