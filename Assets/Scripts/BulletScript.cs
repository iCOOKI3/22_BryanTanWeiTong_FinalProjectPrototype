using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Zombie")
        {
            Destroy(gameObject);
        }
    }
}
