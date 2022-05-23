using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRacket : MonoBehaviour
{
    public GameObject _racket;
    public Vector3 _racket_spawn_pos;
    public Rigidbody rb;

    void Start()
    {
        _racket = GameObject.Find("Racket");
        _racket_spawn_pos = _racket.transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            rb.isKinematic = false;
            _racket.transform.position = _racket_spawn_pos;
            _racket.transform.rotation = Quaternion.identity;
            rb.isKinematic = true;
        }
    }

    void Update()
    {

    }
}
