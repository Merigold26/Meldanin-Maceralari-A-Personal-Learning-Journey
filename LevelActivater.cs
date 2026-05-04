using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActivater : MonoBehaviour
{
    private BoxCollider2D areaBox;
    public GameObject[] allEnemies;
    public List<GameObject> klonlar = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        areaBox = GetComponent<BoxCollider2D>();

        foreach (GameObject enemy in allEnemies)
        {
            enemy.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnEnemies()
    {
        foreach (GameObject enemy in allEnemies)
        {
            GameObject newEnemy = Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
            newEnemy.SetActive(true);
            klonlar.Add(newEnemy);

        }
    }

    private void DespawnEnemies()
    {
        foreach (GameObject enemy in klonlar)
        {
            Destroy(enemy);
        }
        klonlar.Clear();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            KameraKontrolcüsü.instance.areaBox = areaBox;

            SpawnEnemies();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (OyuncuCan.instance.can > 0)
            {
                DespawnEnemies();
            }
        }
    }

}
