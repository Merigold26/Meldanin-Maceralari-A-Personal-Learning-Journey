using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyuncuHareketi : MonoBehaviour
{
    public GameObject HitEffect;
    private bool geriatma;
    public float geriatmazaman, geriatmagücü;
    private float geriatmacounter;
    private Vector2 knockDir;
    public static OyuncuHareketi instance;
    public float HareketHýzý;
    public Rigidbody2D RB;

    private Animator Animasyon;
    public SpriteRenderer SB;
    public Sprite[] OyuncuYönSprite;
    public Animator VurmaTuþu;
    public float koþmaHýzý,koþmaUzun;
    private float koþmaCounter,activeMoveSpeed;
    public float totalStamina, staminaRefillSpeed, dashStamCost;
    public float currentStamina;
    public bool canMove;

    public SpriteRenderer swordSR;
    public Sprite[] allSwords;
    public KýlýçVur swordDmg;
    public int currentSword;
         
    private Vector3 respawnPos;

    private void Awake()
    {
        if(instance == null)
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

        currentSword = SaveManager.instance.activeSave.currentSword;
        swordSR.sprite = allSwords[currentSword];
        swordDmg.hasaroraný = SaveManager.instance.activeSave.swordDamage;

        transform.position = SaveManager.instance.activeSave.sceneStartPosition;
        totalStamina = SaveManager.instance.activeSave.maxStamina;
        Animasyon = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        activeMoveSpeed = HareketHýzý;
        currentStamina = totalStamina;
        UIManager.instance.UpdateEnerji();
        
    }

    // Update is called once per frame
    void Update()
    {
        // eski kod transform.position = new Vector3(transform.position.x + (Input.GetAxisRaw("Horizontal")*HareketHýzý*Time.deltaTime),transform.position.y + (Input.GetAxisRaw("Vertical")*HareketHýzý* Time.deltaTime),transform.position.z);

        if (canMove && !GameManager.instance.dialogActive)
        {

            if (!geriatma)
            {


                RB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * activeMoveSpeed;

                Animasyon.SetFloat("Hýz", RB.velocity.magnitude);

                if (RB.velocity != Vector2.zero)
                {
                    if (Input.GetAxisRaw("Horizontal") != 0)
                    {
                        SB.sprite = OyuncuYönSprite[1];

                        if (Input.GetAxisRaw("Horizontal") < 0)
                        {
                            SB.flipX = true;
                            VurmaTuþu.SetFloat("dirX", -1f);
                            VurmaTuþu.SetFloat("dirY", 0f);
                        }
                        else
                        {
                            SB.flipX = false;
                            VurmaTuþu.SetFloat("dirX", 1f);
                            VurmaTuþu.SetFloat("dirY", 0f);
                        }
                    }
                    else
                    {
                        if (Input.GetAxisRaw("Vertical") < 0)
                        {
                            SB.sprite = OyuncuYönSprite[0];
                            VurmaTuþu.SetFloat("dirX", 0f);
                            VurmaTuþu.SetFloat("dirY", -1f);
                        }
                        else
                        {
                            SB.sprite = OyuncuYönSprite[2];
                            VurmaTuþu.SetFloat("dirX", -1f);
                            VurmaTuþu.SetFloat("dirY", 0f);
                        }
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    VurmaTuþu.SetTrigger("Vurma");

                    AudioManager.instance.PlaySFX(0);
                }
                if (koþmaCounter <= 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && currentStamina >= dashStamCost)
                    {
                        activeMoveSpeed = koþmaHýzý;
                        koþmaCounter = koþmaUzun;

                        currentStamina -= dashStamCost;
                    }
                }
                else
                {
                    koþmaCounter -= Time.deltaTime;

                    if (koþmaCounter <= 0)
                    {
                        activeMoveSpeed = HareketHýzý;
                    }
                }
                currentStamina += staminaRefillSpeed * Time.deltaTime;
                if (currentStamina > totalStamina)
                {
                    currentStamina = totalStamina;
                }
                UIManager.instance.UpdateEnerji();

            }
            else
            {
                geriatmacounter -= Time.deltaTime;
                RB.velocity = knockDir * geriatmagücü;

                if (geriatmacounter <= 0)
                {
                    geriatma = false;
                }
            }
        }
        else
        {
            RB.velocity = Vector2.zero;
            Animasyon.SetFloat("HareketHýzý",0f);
        }
    }
    public void KnockBack(Vector3 knockPosition)
    {
        geriatmacounter = geriatmazaman;
        geriatma = true;
        knockDir = transform.position - knockPosition;
        knockDir.Normalize();

        Instantiate(HitEffect,transform.position,transform.rotation);
    }

    public void DoAtLevelStart()
    {
        canMove = true;

        respawnPos = transform.position;
    }
    public void UpgradeSword(int newDamage, int newSwordRed)
    {
        swordDmg.hasaroraný = newDamage;
        currentSword = newSwordRed;
        swordSR.sprite = allSwords[newSwordRed];

        SaveManager.instance.activeSave.currentSword = currentSword;
        SaveManager.instance.activeSave.swordDamage = newDamage;
    }
    public void ResetOnRespawn()
    {
        transform.position = respawnPos;

        canMove = false;

        gameObject.SetActive(true);

        currentStamina = totalStamina;

        geriatmacounter = 0f;

        OyuncuCan.instance.can = OyuncuCan.instance.maxcan;
    }


}
