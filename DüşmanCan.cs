using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DüşmanCan : MonoBehaviour
{
    public int can1;
    public GameObject ölümefekti;
    private EnemyController EC;
    public GameObject healthDüşme, paraDüşme;
    public float healthŞans,paraŞans;
    // Start is called before the first frame update
    void Start()
    {
        EC = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void hasarvur(int hasaroranı)
    {
        can1 -= hasaroranı;
        if(can1 <= 0) 
        {
            if(ölümefekti != null)
            {
                Instantiate(ölümefekti,transform.position,transform.rotation);

            }
            Destroy(gameObject);

            if(Random.Range(0f,100f) < healthŞans && healthDüşme != null)
            {
                Instantiate(healthDüşme, transform.position, transform.rotation);
            }
            if (Random.Range(0f, 100f) < paraŞans && paraDüşme != null)
            {
                Instantiate(paraDüşme, transform.position, transform.rotation);
            }
            AudioManager.instance.PlaySFX(4);
        }
        AudioManager.instance.PlaySFX(7);
        EC.KnockBack(OyuncuHareketi.instance.transform.position);
    }
}
