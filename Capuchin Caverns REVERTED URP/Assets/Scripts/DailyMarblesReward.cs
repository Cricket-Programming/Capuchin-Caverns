using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// When the user presses this object, they will get 100 Marbles. They can press the button only once a day for daily rewards, else the button will destroy itself.
public class DailyMarblesReward : MonoBehaviour
{
    [Tooltip("IE: Claimed!")]
    [SerializeField] private GameObject showAfter;
    [SerializeField] private int howMuchADay = 100;
    [Tooltip("The sound to play when the user claims the currency.")]
    [SerializeField] private AudioSource soundToPlay;
    private string todayDate;
    private bool hasEntered = false; // Prevents user pressing button super fast and being able to get extra marbles.

    private void Start()
    {
        todayDate = DateTime.Today.ToString("yyyy-MM-dd");

        // It's the same day so deactivate the button.
        if (PlayerPrefs.GetString("previousDate").Equals(todayDate))
        {
            RemovePurchaseButton();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("HandTag")) return;
        if (hasEntered) return;

        hasEntered = true;
        CurrencyManager.Instance.AddPlayFabCurrency(howMuchADay);
        PlayerPrefs.SetString("previousDate", todayDate);

        if (soundToPlay != null)
        {
            // Hide the button temporarily so that it does not show, but scripts still work.
            Renderer renderer = GetComponent<Renderer>();
            renderer.enabled = false;
            foreach (Transform child in transform)
            {
                // Hide the child GameObject
                child.GetComponent<Renderer>().enabled = false;
            }

            soundToPlay.Play();
            Invoke("RemovePurchaseButton", soundToPlay.clip.length);
        }
        else
        {
            RemovePurchaseButton();
        }

    }

    private void RemovePurchaseButton()
    {
        showAfter?.SetActive(true);
        Destroy(gameObject);
    }
}

