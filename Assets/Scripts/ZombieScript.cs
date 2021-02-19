using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public NavMeshAgent Zombie;
    public GameObject Player;
    public Animator zombieAnim;
    public Rigidbody zombieRb;

    public float ZombieDistanceRun;

    // Start is called before the first frame update
    void Start()
    {
        zombieAnim = GetComponent<Animator>();

        zombieRb = GetComponent<Rigidbody>();

        Zombie = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        //Run Towards Player
        if(distance < ZombieDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;

            Zombie.SetDestination(newPos);

            zombieAnim.SetBool("isRun",true);
        }

        if (distance > ZombieDistanceRun)
        {
            zombieAnim.SetBool("isIdle",true);

            zombieAnim.SetBool("isRun", false);
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

