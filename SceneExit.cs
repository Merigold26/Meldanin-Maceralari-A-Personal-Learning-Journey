using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 exitLocation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") 
        {
            OyuncuHareketi.instance.transform.position = exitLocation;

            OyuncuHareketi.instance.RB.velocity = Vector2.zero;
            OyuncuHareketi.instance.canMove = false;
            UIManager.instance.YüklemeEkraný.SetActive(true);
            SceneManager.LoadScene(sceneToLoad);

        }
    }
}
