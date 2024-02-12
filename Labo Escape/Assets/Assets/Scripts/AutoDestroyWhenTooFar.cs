using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyWhenTooFar : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetPlayer(GameObject player) {
        this.player = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            if (player.transform.position.z - this.transform.position.z >= 60) {
                Destroy(gameObject);
            }
        }
    }
}
