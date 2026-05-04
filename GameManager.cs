using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int altýnMiktarý;
    public bool dialogActive;
    public float waithForDeathScreen = 1f, waitToRespawn = 2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        altýnMiktarý = SaveManager.instance.activeSave.currentCoins;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void GetCoins(int coinstoAdd)
    {
        altýnMiktarý += coinstoAdd;

        UIManager.instance.UpdatePara();

        SaveManager.instance.activeSave.currentCoins = GameManager.instance.altýnMiktarý;
    }

    public void PauseUnpause()
    {
        if (!UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(true);

            Time.timeScale = 0f;

            OyuncuHareketi.instance.canMove = false;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(false);

            Time.timeScale = 1f;

            OyuncuHareketi.instance.canMove = true;
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        yield return new WaitForSeconds(waithForDeathScreen);

        UIManager.instance.deathScreen.SetActive(true);

        yield return new WaitForSeconds(waitToRespawn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        UIManager.instance.YüklemeEkraný.SetActive(true);

        OyuncuHareketi.instance.ResetOnRespawn();
    }

    
}
