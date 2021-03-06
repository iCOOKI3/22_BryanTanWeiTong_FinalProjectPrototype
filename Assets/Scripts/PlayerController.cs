﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab; //Set bulletPrefab GameObject
    public GameObject bulletSpawn; //Set bulletSpawn GameObject
    public GameObject FirstPersonViewCam; //Set FirstPersonViewCam GameObject
    public GameObject ThirdPersonViewCam; //Set ThirdPersonViewCam GameObject
    public GameObject DeathViewCam; //Set DeathViewCam GameObject
    public GameObject AmmoText; //Set Ammo Text
    public GameObject HealthBarText; //Set HealthBar Text
    public GameObject zKilledText; //Set zKilled text
    public GameObject CoinText; //Set Coin Collected Text
    public GameObject TimerBarText; //Set TimerBar Text
    public GameObject healthpackText; //Set HealthPack Text

    public Animator playerAnim; //Set Animator of Player
    public Rigidbody playerRb; //Set Rigidbody of Player

    public AudioSource audioSource; //Set AudioSource
    public AudioClip[] AudioClipArr; //Set AudioClip Array

    public HealthBarScript healthBar; //Set Reference from other Script

    public TimerBarScript timerbar; //Set Reference from other Scripts

    public Light Flashlight; //Set reference for Light component

    public ParticleSystem MuzzleFlash; //Set reference for ParticleSystem

    [SerializeField] Text countdownText; //Set Countdown Timer to start counting and show text

    public float moveSpeed; //Player Movement Speed Value
    public float rotateSpeed; //Player Rotate Speed Value
    public float jumpStrength; //Player Jump Strength Value
    public float MaxTime = 150; //Timer
    public static float CoinCollected = 0; //Amount of Coins the player current have
    private float gravity = 850f; //Set Gravity

    public int Ammo = 15; //Set Ammo Value
    public int Health = 100; //Set Player Health
    public int MaxHealth = 5; //Set MaxHeath Value
    public int currentHealth; //Set currentHealth GameObject
    public static int zKilled = 0; //Set reference to other scripts
    public static int healthPack = 0; //Set reference to other scripts
    private int damage = 10; //Set Damage Value


    public static bool purchasedAmmo = false; //Set reference to other scripts
    public static bool purchaseHealth = false; //Set reference to other scripts
    private bool startTimer = true; //Set initial boolean value
    private bool isOutOfAmmo = false; //Set initial boolean value
    private bool playerDead = false; //Set initial boolean value
    private bool FlashLightOn = false; //Set initial boolean value

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        playerDead = false;
         
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

        healthpackText.GetComponent<Text>().text = "Health Kit: " + healthPack;

        currentHealth = MaxHealth;

        healthBar.SetMaxHealth(MaxHealth);

        audioSource = GetComponent<AudioSource>();

        zKilled = 0;

        CoinCollected = 0;

        healthPack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        zKilledText.GetComponent<Text>().text = "Zombies Killed: " + zKilled;

        playerRb.AddForce(Vector3.down * Time.deltaTime * gravity);

        CoinText.GetComponent<Text>().text = "Coin Collected: " + CoinCollected;

        healthpackText.GetComponent<Text>().text = "Health Kit: " + healthPack;

        healthBar.SetHealth(currentHealth);

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
            //Use Health Kit
            if(Input.GetKeyDown(KeyCode.E) && purchaseHealth == true)
            {
                Health = 100;
                healthPack -= 1;
                currentHealth = 100;
                healthBar.SetHealth(currentHealth);
                HealthBarText.GetComponent<Text>().text = "Health: " + Health;
                playerAnim.SetTrigger("trigHeal");
                audioSource.PlayOneShot(AudioClipArr[4], 3.5f);
            }
            else if(Input.GetKeyUp(KeyCode.E) && purchaseHealth == true)
            {
                playerAnim.SetBool("isIdle", true);
                purchaseHealth = false;
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
            if (Input.GetKeyDown(KeyCode.DownArrow) && purchasedAmmo == true)
            {
                audioSource.PlayOneShot(AudioClipArr[0], 0.5f);
                playerAnim.SetTrigger("trigReloading");
                isOutOfAmmo = false;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) && purchasedAmmo == true)
            {
                Ammo = 15;
                AmmoText.GetComponent<Text>().text = "Ammo: " + Ammo;
                playerAnim.SetBool("isIdle", true);
                purchasedAmmo = false;
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

                Health -= 10;

                HealthBarText.GetComponent<Text>().text = "Health: " + Health;

                if (currentHealth <= 0)
                {

                    HealthBarText.GetComponent<Text>().text = "Health: 0";

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
