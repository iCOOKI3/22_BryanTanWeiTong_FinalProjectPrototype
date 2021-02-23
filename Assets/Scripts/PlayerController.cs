using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed; //Player Movement Speed Value
    public float rotateSpeed; //Player Rotate Speed Value
    public float jumpStrength; //Player Jump Strength Value
    public GameObject bulletPrefab; //Set bulletPrefab GameObject
    public GameObject bulletSpawn; //Set bulletSpawn GameObject

    public Animator playerAnim; //Set Animator of Player
    public Rigidbody playerRb; //Set Rigidbody of Player

    public GameObject FirstPersonViewCam; //Set FirstPersonViewCam GameObject
    public GameObject ThirdPersonViewCam; //Set FirstPersonViewCam GameObject
    public GameObject DeathViewCam;

    public AudioSource audioSource;
    public AudioClip[] AudioClipArr;

    public int Ammo = 15; //Set Ammo Value

    public int Health = 10;

    public static int zKilled = 0;

    public GameObject AmmoText; //Set Ammo Text

    public GameObject HealthBarText;

    public  GameObject zKilledText;

    public GameObject CoinText;

    public GameObject TimerBarText;

    public static float CoinCollected = 0;

    public int MaxHealth = 5; //Set MaxHeath Value

    public int currentHealth; //Set currentHealth GameObject

    public HealthBarScript healthBar; //Set Reference from other Script

    public float MaxTime = 15;

    public TimerBarScript timerbar;

    private bool startTimer = true;

    private bool isOutOfAmmo = false; //Set initial boolean value
    
    private int damage = 1; //Set Damage Value

    private bool playerDead = false;

    private float gravity = 850f;

    public Light Flashlight;
    private bool FlashLightOn = false;

    public ParticleSystem MuzzleFlash;

    [SerializeField] Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>(); //Get Animator Component

        playerRb = GetComponent<Rigidbody>(); //Get Rigidbody Component

        ThirdPersonViewCam.SetActive(true); //Set Game to start in Third Person

        FirstPersonViewCam.SetActive(false); //Set Game to not start in First Person

        DeathViewCam.SetActive(false); //Set Game to not start in DeathView Cam

        AmmoText.GetComponent<Text>().text = "Ammo: " + Ammo;

        HealthBarText.GetComponent<Text>().text = "Health: " + Health;

        zKilledText.GetComponent<Text>().text = "Zombies Killed: " + zKilled;

        CoinText.GetComponent<Text>().text = "Coin Collected: " + CoinCollected;

        TimerBarText.GetComponent<Text>().text = "Time Left: " + MaxTime;

        currentHealth = MaxHealth;

        healthBar.SetMaxHealth(MaxHealth);

        audioSource = GetComponent<AudioSource>();

        zKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        zKilledText.GetComponent<Text>().text = "Zombies Killed: " + zKilled;

        playerRb.AddForce(Vector3.down * Time.deltaTime * gravity);

        CoinText.GetComponent<Text>().text = "Coin Collected: " + CoinCollected;

        if (playerDead == false && startTimer == true)
        {

            timerbar.SetTimer((int)MaxTime);
            MaxTime -= 1 * Time.deltaTime;
            TimerBarText.GetComponent<Text>().text = "Time Left: " + Mathf.Round(MaxTime);

            //Move Front with Animation
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

                playerAnim.SetBool("isRun", true);
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                playerAnim.SetBool("isRun", false);
            }
            //Move Back with Animation
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * -moveSpeed);

                playerAnim.SetBool("isRun", true);

            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                playerAnim.SetBool("isRun", false);
            }
            //Move Front with Animation
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);

                playerAnim.SetBool("isLeft", true);
                playerAnim.SetBool("isIdle", false);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                playerAnim.SetBool("isIdle", true);
                playerAnim.SetBool("isLeft", false);
            }
            //Move Front with Animation
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);

                playerAnim.SetBool("isRight", true);
                playerAnim.SetBool("isIdle", false);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                playerAnim.SetBool("isIdle", true);
                playerAnim.SetBool("isRight", false);
            }
            //Change Camera
            if (Input.GetKey(KeyCode.Comma))
            {
                FirstPersonViewCam.SetActive(false);

                ThirdPersonViewCam.SetActive(true);

                DeathViewCam.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.Period))
            {
                FirstPersonViewCam.SetActive(true);

                ThirdPersonViewCam.SetActive(false);

                DeathViewCam.SetActive(false);
            }
            //Frontflip
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAnim.SetTrigger("trigFlip");
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                playerAnim.SetBool("isIdle", true);
            }
            //Prevent shooting when 0 Ammo
            if (isOutOfAmmo == false)
            {
                //Shoot 
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    playerAnim.SetTrigger("trigShooting");

                    Instantiate(bulletPrefab, bulletSpawn.transform.position, transform.rotation);

                    Ammo -= 1;

                    AmmoText.GetComponent<Text>().text = "Ammo: " + Ammo;

                    audioSource.PlayOneShot(AudioClipArr[1], 0.5f);

                    MuzzleFlash.Play();

                    if (Input.GetKeyDown(KeyCode.UpArrow) && Ammo == 0)
                    {
                        isOutOfAmmo = true;

                        audioSource.PlayOneShot(AudioClipArr[3], 0.5f);
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                playerAnim.SetBool("isIdle", true);
            }
            //Reloading
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                audioSource.PlayOneShot(AudioClipArr[0], 0.5f);
                playerAnim.SetTrigger("trigReloading");
                isOutOfAmmo = false;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                Ammo = 15;
                AmmoText.GetComponent<Text>().text = "Ammo: " + Ammo;
                playerAnim.SetBool("isIdle", true);
            }
            //Rotate Left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(new Vector3(0, Time.deltaTime * -rotateSpeed, 0));
            }
            //Rotate Right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0, Time.deltaTime * rotateSpeed, 0));
            }
            //Print Health Text
            if (playerDead == true)
            {
                HealthBarText.GetComponent<Text>().text = "Health: 0";
            }
            //Winning Condition
            if (zKilled == 15)
            {
                SceneManager.LoadScene("WinScene");
            }
            if (Input.GetKeyDown(KeyCode.C) && FlashLightOn == true)
            {
                Flashlight.enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.C) && FlashLightOn == false)
            {
                Flashlight.enabled = false;
            }
            else
            {
                FlashLightOn = !FlashLightOn;
            }
            if(MaxTime <= 0)
            {
                TimerBarText.GetComponent<Text>().text = "Time Left: 0";

                StartCoroutine(PlayDeathAnim());

                FirstPersonViewCam.SetActive(false);

                ThirdPersonViewCam.SetActive(false);

                DeathViewCam.SetActive(true);

                playerDead = true;

                startTimer = false;
            }
        }

        if(playerDead == true)
        {
            PlayDeathAnim();
        }
    }
        

    private void OnCollisionEnter(Collision collision)
    {
        if(playerDead == false)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                currentHealth -= damage;

                healthBar.SetHealth(currentHealth);

                Health -= 1;

                HealthBarText.GetComponent<Text>().text = "Health: " + Health;

                if (currentHealth == 0)
                {
                    StartCoroutine(PlayDeathAnim());

                    FirstPersonViewCam.SetActive(false);

                    ThirdPersonViewCam.SetActive(false);

                    DeathViewCam.SetActive(true);

                    playerDead = true;
                }
            }
        }  
        
        if(collision.gameObject.tag == "Coin")
        {
            CoinCollected++;

            CoinText.GetComponent<Text>().text = "Coin Collected: " + CoinCollected;

            audioSource.PlayOneShot(AudioClipArr[2], 0.5f);

            Destroy(collision.collider.gameObject);
        }
    }

    private IEnumerator PlayDeathAnim()
    {
        playerAnim.SetTrigger("trigDeath");

        playerAnim.SetTrigger("trigExit");

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("LoseScene");

    }
}
