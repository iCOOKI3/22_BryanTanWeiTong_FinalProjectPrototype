using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuScript : MonoBehaviour
{

    public static bool GameIsPausedShop = false;

    public GameObject shopMenuUI;

    public GameObject ShopCoinText;

    public AudioSource audioSource;
    public AudioClip[] AudioClipArr;

    private void Start()
    {
        ShopCoinText.GetComponent<Text>().text = ": " + PlayerController.CoinCollected;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPausedShop)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        shopMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPausedShop = false;
        AudioListener.pause = false;
    }

    void PauseGame()
    {
        shopMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPausedShop = true;
        AudioListener.pause = false;

        ShopCoinText.GetComponent<Text>().text = ": " + PlayerController.CoinCollected;
    }

    public void Ammo()
    {

        if(PlayerController.CoinCollected >= 5)
        {
            PlayerController.CoinCollected -= 5;
            Debug.Log("Purchasing Ammo...");
            audioSource.PlayOneShot(AudioClipArr[0], 1.25f);
            ShopCoinText.GetComponent<Text>().text = ": " + PlayerController.CoinCollected;
        }
        else
        {
            Debug.Log("Insufficient coins");
            audioSource.PlayOneShot(AudioClipArr[2], 0.5f);
        }
        
    }

    public void HealthPack()
    {
        if(PlayerController.CoinCollected >= 5)
        {
            PlayerController.CoinCollected -= 5;
            Debug.Log("Purchasing Health Pack...");
            audioSource.PlayOneShot(AudioClipArr[1], 1.25f);
            ShopCoinText.GetComponent<Text>().text = ": " + PlayerController.CoinCollected;
        }
        else
        {
            Debug.Log("Insufficient coins");
            audioSource.PlayOneShot(AudioClipArr[2], 0.5f);
        }
    }
}
