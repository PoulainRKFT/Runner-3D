using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.Rotate(rotation * UnityEngine.Random.Range(0, 180));
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotation * Time.deltaTime);
    }
}
