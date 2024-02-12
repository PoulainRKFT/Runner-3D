using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisionBehavior : MonoBehaviour
{
    PlayerBehavior playerBehavior;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 7) {
            playerBehavior.Die();
        } else if (collider.gameObject.layer == 8) {
            if (collider.name == "Blue Document") {
                playerBehavior.CollectFile(true);
                Destroy(collider.gameObject);
            } else {
                playerBehavior.CollectFile(false);
                Destroy(collider.gameObject);
            }
            
        } else if (collider.gameObject.layer == 9) {
            if (collider.name == "Player Go Up") {
                playerBehavior.GoUp();
                Destroy(collider.gameObject.transform.parent.gameObject);
            } else if (collider.name == "Player Go Down") {
                playerBehavior.GoDown();
                Destroy(collider.gameObject.transform.parent.gameObject);
            } else {
                playerBehavior.Die();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerBehavior = this.GetComponentInParent<PlayerBehavior>();
    }
}
