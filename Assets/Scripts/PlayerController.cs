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
    public GameObject bulletPrefab;
    public GameObject bulletSpawn;

    public Animator playerAnim; //Set Animator of Player
    public Rigidbody playerRb; //Set Rigidbody of Player

    public GameObject FirstPersonViewCam;
    public GameObject ThirdPersonViewCam;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>(); //Get Animator Component

        playerRb = GetComponent<Rigidbody>(); //Get Rigidbody Component

        ThirdPersonViewCam.SetActive(true); //Set Game to start in Third Person

        FirstPersonViewCam.SetActive(false); //Set Game to not start in First Person
    }

    // Update is called once per frame
    void Update()
    {
        //Move Front with Animation
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

            playerAnim.SetBool("isRun",true);
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

            playerAnim.SetBool("isLeft",true);
            playerAnim.SetBool("isIdle", false);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.SetBool("isIdle", true);
            playerAnim.SetBool("isLeft",false);
        }
        //Move Front with Animation
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);

            playerAnim.SetBool("isRight",true);
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
        }
        else if(Input.GetKey(KeyCode.Period))
        {
            FirstPersonViewCam.SetActive(true);

            ThirdPersonViewCam.SetActive(false);
        }
        //Frontflip
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetTrigger("trigFlip");
        }
        //Shoot 
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerAnim.SetTrigger("trigShooting");

            Instantiate(bulletPrefab, bulletSpawn.transform.position, transform.rotation);
        }
        else if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            playerAnim.SetBool("isIdle",true);
        }
        //Reloading
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerAnim.SetTrigger("trigReloading");
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
    }
}
