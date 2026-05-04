using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomActivator : MonoBehaviour
{
    public GameObject[] allEnemies;
    public List<GameObject> klonlar = new List<GameObject>();
    public bool lockDoors;
    public GameObject[] doors;
    private bool doorsLocked, dontSpawnEnemies;

    public bool isBossRoom;
    public Transform bossCamPointLwr, bossCamPointUpr;
    public GameObject theBoss;
    private bool dontReactiveteBoss;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemy in allEnemies)
        {
            enemy.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(doorsLocked)
        {
            bool enemyFound = false;

            for(int i = 0; i < klonlar.Count; i++)
            {
                if (klonlar[i] != null)
                {
                    enemyFound = true;
                }
            }

            if(!enemyFound) 
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);
                }

                doorsLocked = false;
                lockDoors = false;
                dontSpawnEnemies = true;
            }
        }
    }
    private void SpawnEnemies()
    {
        if (!dontSpawnEnemies)
        {

            foreach (GameObject enemy in allEnemies)
            {
                GameObject newEnemy = Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
                newEnemy.SetActive(true);
                klonlar.Add(newEnemy);

            }
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
        if(other.tag == "Player")
        {
            DungeonCameraController.instance.targetPoint = new Vector3(transform.position.x, transform.position.y, DungeonCameraController.instance.targetPoint.z);
            SpawnEnemies();

            if(lockDoors)
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true);
                }
                doorsLocked = true;
            }
            if(isBossRoom)
            {
                DungeonCameraController.instance.ActivateBossRoom(bossCamPointUpr.position,bossCamPointLwr.position);
                if(!dontReactiveteBoss) 
                { 
                    theBoss.SetActive(true); 
                    dontReactiveteBoss = true;
                }

            }
            else
            {
                DungeonCameraController.instance.inBossRoom = false;
            }
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
