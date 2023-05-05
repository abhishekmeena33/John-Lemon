using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    void Update(){
        if(Input.GetKey("enter")||Input.GetKey("space")){
            Load();
        }
    }

    public void Load()
    {
        SceneManager.LoadScene(1);
    }
}
