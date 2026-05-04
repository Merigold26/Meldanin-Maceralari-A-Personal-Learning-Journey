using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D RB;

    public Animator Animasyon;

    public float moveSpeed;
    
    public float waitTime,moveTime;
    private float waitCounter, moveCounter;

    private Vector2 moveDir;

    public BoxCollider2D area;

    public bool kovalamalýmý;

    private bool kovala;

    public float kovalahýz, kovalarange, vurbekle;

    public int hasaroraný = 10;

    private bool geriatma;
    public float geriatmazaman, geriatmagücü,geriatbekle;
    private float geriatmacounter,geribeklecounter;
    private Vector2 knockDir;

    public bool shouldShoot;
    public GameObject Mermi;
    public float timeBetweenShots;
    private float shotCounter;
    public Transform shotPoint;



    

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = Random.Range(waitTime * .75f,waitTime * 1.25f);

        shotCounter = timeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (!geriatma)
        {
            if (!kovala)
            {

                if (waitCounter > 0)
                {
                    waitCounter = waitCounter - Time.deltaTime;

                    RB.velocity = Vector2.zero;

                    if (waitCounter <= 0)
                    {
                        moveCounter = Random.Range(moveTime * .75f, moveTime * 1.25f);

                        Animasyon.SetBool("moving", true);

                        moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                        moveDir.Normalize();
                    }
                }
                else
                {
                    moveCounter -= Time.deltaTime;

                    RB.velocity = moveDir * moveSpeed;

                    if (moveCounter <= 0)
                    {
                        waitCounter = Random.Range(waitTime * .75f, waitTime * 1.25f);

                        Animasyon.SetBool("moving", false);
                    }
                    if (kovalamalýmý && OyuncuHareketi.instance.gameObject.activeInHierarchy)
                    {
                        if (Vector3.Distance(transform.position, OyuncuHareketi.instance.transform.position) < kovalarange)
                        {
                            kovala = true;
                        }
                    }

                }
                if(shouldShoot) 
                { 
                    shotCounter -= Time.deltaTime;

                    if(shotCounter <= 0)
                    {
                        shotCounter = timeBetweenShots;

                        Instantiate(Mermi,shotPoint.position,shotPoint.rotation);
                    }
                }
            }
            else
            {

                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;

                    RB.velocity = Vector2.zero;

                    if (waitCounter < 0)
                    {
                        Animasyon.SetBool("moving", true);
                    }
                }
                else
                {
                    moveDir = OyuncuHareketi.instance.transform.position - transform.position;
                    moveDir.Normalize();

                    RB.velocity = moveDir * kovalahýz;
                }
                if (Vector3.Distance(transform.position, OyuncuHareketi.instance.transform.position) > kovalarange || !OyuncuHareketi.instance.gameObject.activeInHierarchy)
                {
                    kovala = false;

                    waitCounter = waitTime;

                    Animasyon.SetBool("moving", false);
                }
            }
        }
        else
        {
            if(geriatmacounter > 0)
            {
                geriatmacounter -= Time.deltaTime;
                RB.velocity = knockDir * geriatmagücü;

                if(geriatmacounter <= 0)
                {
                    geribeklecounter = geriatbekle;
                }
            }else
            {
                geriatmacounter -= Time.deltaTime;

                RB.velocity = Vector2.zero;

                if(geriatmacounter <= 9)
                {
                    geriatma = false;
                }
            }
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,area.bounds.min.x + 1f,area.bounds.max.x - 1f),
            Mathf.Clamp(transform.position.y, area.bounds.min.y + 1f, area.bounds.max.y - 1f),transform.position.z);
    }

    public void KnockBack(Vector3 knockPosition)
    {
        geriatmacounter = geriatmazaman;
        geriatma = true;
        knockDir = transform.position - knockPosition;
        knockDir.Normalize();

        
    }









    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.gameObject.tag == "Player")
        {
            if (kovala)
            {
                waitCounter = vurbekle;
                Animasyon.SetBool("moving", false);

                OyuncuHareketi.instance.KnockBack(transform.localPosition);

                OyuncuCan.instance.OyuncuHasar(hasaroraný);
            }
        }
    }

}


