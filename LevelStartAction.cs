using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStartAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OyuncuHareketi.instance.DoAtLevelStart();

        SaveManager.instance.activeSave.currentScene = SceneManager.GetActiveScene().name;

        SaveManager.instance.activeSave.sceneStartPosition = OyuncuHareketi.instance.transform.position;

        SaveManager.instance.SaveInfo();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
