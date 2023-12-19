using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBehavior : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject previousGround;
    public GameObject ground;

    public ScrollingBehavior scrollingScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ground.transform.position.z <= -360f) {
            GameObject newGround = Instantiate(groundPrefab, new Vector3(0f, 0f, 640f), Quaternion.identity);
            scrollingScript.scrollingList.Add(newGround);

            if (previousGround != null) {
                scrollingScript.scrollingList.Remove(previousGround);
                Destroy(previousGround);
            }

            previousGround = ground;
            ground = newGround;
        }
    }
}
