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

    public AudioSource audioSource;
    public AudioClip[] AudioClipArr;

    public float ZombieDistanceRun;

    private bool zDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        zombieAnim = GetComponent<Animator>();

        zombieRb = GetComponent<Rigidbody>();

        Zombie = GetComponent<NavMeshAgent>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        //Run Towards Player
        if (distance < ZombieDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;

            Zombie.SetDestination(newPos);

            zombieAnim.SetBool("isRun",true);
        }

        if (distance > ZombieDistanceRun)
        {
            zombieAnim.SetBool("isIdle", true);

            zombieAnim.SetBool("isRun", false);
        }

        if(zDeath == true)
        {
            PlayerController.zKilled += 1;

            zDeath = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && zDeath == false)
        {
            zDeath = true;

            zombieAnim.SetTrigger("trigDeath");

            zombieAnim.SetBool("isRun", false);

            Destroy(gameObject,1.0f);

            audioSource.PlayOneShot(AudioClipArr[0], 1.0f);
        }
    }
}

