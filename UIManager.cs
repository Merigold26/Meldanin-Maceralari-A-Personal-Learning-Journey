using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Slider healthSlider, staminaSlider;
    public TMP_Text healthText, staminaText, coinsText;
    public string mainMenuScene;
    public GameObject pauseScreen;
    public GameObject YüklemeEkraný;
    public Slider bossHealthSlider;
    public TMP_Text bossÝsmi;
    public GameObject deathScreen;

   
   

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateCan();
        UpdateEnerji();
        UpdatePara();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateCan()
    {
        healthSlider.maxValue = OyuncuCan.instance.maxcan;
        healthSlider.value = OyuncuCan.instance.can;
        healthText.text = "Can " + OyuncuCan.instance.can + "/" + OyuncuCan.instance.maxcan;
    }
    public void UpdateEnerji()
    {
        staminaSlider.maxValue = OyuncuHareketi.instance.totalStamina;
        staminaSlider.value = OyuncuHareketi.instance.currentStamina;
        staminaText.text = "Enerji" + Mathf.RoundToInt(OyuncuHareketi.instance.currentStamina) + "/" + OyuncuHareketi.instance.totalStamina;
    }
    public void UpdatePara()
    {
        coinsText.text = "Para :" + GameManager.instance.altýnMiktarý;
    }

    public void GoToMainManu()
    {
        SceneManager.LoadScene(mainMenuScene);

        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("çýkýþþþþþþ");
    }
    
    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    
}
