using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject htp1, htp2;


    public void Scenechanger(int level)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }

    public void Exit()
    {
        //fade.Play("FadeOut", 0, 0f);
        //StartCoroutine(Delay());

        Application.Quit();


    }

    public void ToggleObjectOff(GameObject target)
    {
        
            //set inactive
            target.SetActive(false);
        
    }

    public void ToggleObjectOn(GameObject target)
    {
        //set active
        target.SetActive(true);
    }


    public void HTP( int type)
    {
        if (type == 0)
        {
            htp1.SetActive(false);
            htp2.SetActive(true);
        }
        else
        {
            htp1.SetActive(true);
            htp2.SetActive(false);
        }
    }

    
}
