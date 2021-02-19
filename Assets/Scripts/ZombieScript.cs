using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{

    public Transform Player;
    public Animator zombieAnim;
    public Rigidbody zombieRb;

    public float moveSpeed;
    public float MaxDist;
    public float MinDist;

    // Start is called before the first frame update
    void Start()
    {
        zombieAnim = GetComponent<Animator>();

        zombieRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            zombieAnim.SetBool("isRun", true);
        }

        if (Vector3.Distance(transform.position, Player.position) >= MaxDist)
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            zombieAnim.SetTrigger("trigDeath");

            zombieAnim.SetBool("isRun", false);

            Destroy(this);
        }
    }
}

