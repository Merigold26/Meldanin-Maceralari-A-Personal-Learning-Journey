using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canİtem : MonoBehaviour
{
    public int healthToRestore;

    public float lifetime;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            OyuncuCan.instance.RestoreHealth(healthToRestore);

            Destroy(gameObject);

            AudioManager.instance.PlaySFX(6);
        }
    }
}
