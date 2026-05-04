using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KılıçVur : MonoBehaviour
{

    public int hasaroranı;
    public GameObject hiteffect;

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
        if (other.tag == "Enemy")
        {
            other.GetComponent<DüşmanCan>().hasarvur(hasaroranı);
            Instantiate(hiteffect, transform.position, transform.rotation);

            
        }

        if(other.tag == "Boss")
        {
            other.GetComponent<BossWeakPoint>().DamageBoss(hasaroranı);
            
        }
        
    }
}
