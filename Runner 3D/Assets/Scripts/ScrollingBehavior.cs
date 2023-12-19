using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBehavior : MonoBehaviour
{
    public List<GameObject> scrollingList;
    public float speed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (speed < 25) {
            speed += 0.05f;
        }

        foreach(GameObject objet in scrollingList)
        {
            objet.transform.position = new UnityEngine.Vector3(objet.transform.position.x, objet.transform.position.y, objet.transform.position.z - (speed * Time.deltaTime));
        }
    }
}
