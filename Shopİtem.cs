using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopİtem : MonoBehaviour
{
    [TextArea]
    public string description;

    public int itemCost;

    private bool itemActive;

    public bool isHealthUpgrade, isStamUpgrade;

    public int amountToAdd;

    public bool removeAfterPurchase;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (itemActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(GameManager.instance.altınMiktarı >= itemCost)
                {
                    GameManager.instance.altınMiktarı -= itemCost;
                    UIManager.instance.UpdatePara();

                    if(isHealthUpgrade )
                    {
                        OyuncuCan.instance.maxcan += amountToAdd;
                        OyuncuCan.instance.can += amountToAdd;

                        SaveManager.instance.activeSave.maxHealth = OyuncuCan.instance.maxcan;

                        UIManager.instance.UpdateCan();

                    }

                    if(isStamUpgrade)
                    {
                        OyuncuHareketi.instance.totalStamina += amountToAdd;
                        OyuncuHareketi.instance.currentStamina += amountToAdd;

                        SaveManager.instance.activeSave.maxStamina = OyuncuHareketi.instance.totalStamina;

                        UIManager.instance.UpdateEnerji();
                    }

                    SaveManager.instance.activeSave.currentCoins = GameManager.instance.altınMiktarı;

                    if(removeAfterPurchase )
                    {
                        gameObject.SetActive(false);


                    }


                    DialogManager.instance.dialogBox.SetActive(false);
                    itemActive = false;
                } else
                {
                    DialogManager.instance.dialogText.text = "Gerekli Altinin Yok";
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            itemActive = true;

            DialogManager.instance.dialogBox.SetActive(true);
            DialogManager.instance.dialogText.text = description;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            itemActive = false;

            DialogManager.instance.dialogBox.SetActive(false);
            
        }

    }
}
