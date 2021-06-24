using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    


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

    
}
