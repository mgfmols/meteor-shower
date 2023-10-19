using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody rb;
    void Start()
    {
        rb.AddForce(new Vector3(-5, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
