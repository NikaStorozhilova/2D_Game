using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooteratthePlayerController : MonoBehaviour
{
    public GameObject bullet;


    void Start()
    {
        InvokeRepeating("Spawn", 1, 3);
    }

    void Update()
    {
    }

    void Spawn()
    {
        GameObject bullet_new = Instantiate(bullet, transform.position - new Vector3(2, 0), transform.rotation);
        Destroy(bullet_new, 2);
    }
}