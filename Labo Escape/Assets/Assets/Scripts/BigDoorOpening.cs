using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoorOpening : MonoBehaviour
{
    public GameObject player;
    private PlayerBehavior playerBehavior;
    public Vector3 directionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerBehavior = player.GetComponent<PlayerBehavior>();
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            if (this.transform.position.z - player.transform.position.z <= 20) {
                this.transform.position = new Vector3(this.transform.position.x - ((directionSpeed.x * playerBehavior.overallSpeedRate) * Time.deltaTime), this.transform.position.y - ((directionSpeed.y * playerBehavior.overallSpeedRate) * Time.deltaTime), this.transform.position.z - ((directionSpeed.z * playerBehavior.overallSpeedRate) * Time.deltaTime));
            }
        }
    }
}
