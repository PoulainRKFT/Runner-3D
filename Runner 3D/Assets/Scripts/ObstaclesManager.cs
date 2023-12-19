using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public float spawnRange = 2f;
    public float timeBetweenSpawn = 0f;

    bool spawnable = false;
    float spawnableDelay = 3f;

    public GameObject smallBarrier;
    public GameObject highBarrier;
    public GameObject largeBarrier;

    public ScrollingBehavior scrollingScript;

    string[] spawningSlots;

    // Start is called before the first frame update
    void Start()
    {
        spawningSlots = new string[3];
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnableDelay > 0f) {
            spawnableDelay -= Time.deltaTime;
            spawnable = false;
        } else {
            spawnable = true;
        }

        if (spawnable) {
            timeBetweenSpawn -= Time.deltaTime;

            if (spawnRange > 0.5f) {
                spawnRange -= 0.01f * Time.deltaTime;
            }

            if (timeBetweenSpawn <= 0) {
                FillSpawnSlotRandomly();
                SpawnObstaclesRandomlyFromSlots();
                timeBetweenSpawn = Random.Range(spawnRange, spawnRange * 2);
            }
        }
    }

    void SpawnObstaclesRandomlyFromSlots() {
        // Spawn large obstacles randomly
        if (spawningSlots[0] == "1" && spawningSlots[1] == "1") {
            if (Random.Range(0, 2) == 1) {
                spawningSlots[0] = "Large Barrier Left";
                spawningSlots[1] = "Large Barrier Right";
                GameObject newObstacle = Instantiate(largeBarrier, new Vector3(-1.5f, 0f, 110f + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
                newObstacle.transform.Rotate(0f, 90f, 0f, Space.Self);
                scrollingScript.scrollingList.Add(newObstacle);
            }
        }
        else if (spawningSlots[1] == "1" && spawningSlots[2] == "1") {
            if (Random.Range(0, 2) == 1) {
                spawningSlots[1] = "Large Barrier Left";
                spawningSlots[2] = "Large Barrier Right";
                GameObject newObstacle = Instantiate(largeBarrier, new Vector3(1.5f, 0f, 110f + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
                newObstacle.transform.Rotate(0f, 90f, 0f, Space.Self);
                scrollingScript.scrollingList.Add(newObstacle);
            }
        }

        for (int i = 0; i < 3; i++) {
            if (spawningSlots[i] == "0") {
                if (Random.Range(0, 2) == 1) {
                    spawningSlots[i] = "Small Barrier";
                    GameObject newObstacle = Instantiate(smallBarrier, new Vector3(-3 + (i * 3), 0f, 110f + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
                    newObstacle.transform.Rotate(0f, 90f, 0f, Space.Self);
                    scrollingScript.scrollingList.Add(newObstacle);
                } else {
                    spawningSlots[i] = "Nothing";
                }
            }
            else if (spawningSlots[i] == "1") {
                spawningSlots[i] = "High Barrier";
                GameObject newObstacle = Instantiate(highBarrier, new Vector3(-3 + (i * 3), 0f, 110f + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
                newObstacle.transform.Rotate(0f, 90f, 0f, Space.Self);
                scrollingScript.scrollingList.Add(newObstacle);
            }
        }
    }

    void FillSpawnSlotRandomly() {
        for (int i = 0; i < 3; i++) {
            spawningSlots[i] = null;
        }
        
        spawningSlots[Random.Range(0, 3)] = "0";

        for (int i = 0; i < 3; i++) {
            if (spawningSlots[i] == null) {
                spawningSlots[i] = "" + Random.Range(0, 2);
            }
        }
    }
}
