using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyuncuCan : MonoBehaviour
{
    public static OyuncuCan instance;
    public int can;
    public int maxcan;
    public float görünmezliksüre = 1f;
    private float görünmezlikcounter;
    public GameObject ÖlmeEfekti;

    
    private void Awake()
    {
        
        
        if(instance == null)
        {
            instance = this;
        }
        
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        maxcan = SaveManager.instance.activeSave.maxHealth;
        can = maxcan;
        UIManager.instance.UpdateCan();

    }

    // Update is called once per frame
    void Update()
    {
        if(görünmezlikcounter > 0)
        {
            görünmezlikcounter -= Time.deltaTime;
        }
    }
    public void OyuncuHasar(int hasaroraný)
    {
        if (ÖlmeEfekti != null)
        {
            Instantiate(ÖlmeEfekti, transform.position, transform.rotation);
        }



        if (görünmezlikcounter <= 0)
        {


            can -= hasaroraný;

            görünmezlikcounter = görünmezliksüre;

            if(can <= 0)
            {
                can = 0;

                gameObject.SetActive(false);

                Instantiate(ÖlmeEfekti, transform.position, transform.rotation);

                AudioManager.instance.PlaySFX(4);

                GameManager.instance.Respawn();
            }

            UIManager.instance.UpdateCan();

            AudioManager.instance.PlaySFX(7);

            
        }
    }
    public void RestoreHealth(int healthToRestore)
    {
        can += healthToRestore;

        if(can > maxcan)
        {
            can = maxcan;
        }
        UIManager.instance.UpdateCan();
    }
}
