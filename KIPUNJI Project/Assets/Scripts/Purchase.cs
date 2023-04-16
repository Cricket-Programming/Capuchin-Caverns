using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchase : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enable;
    public GameObject disable;
    public string CosmeticName;
    public int price;

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerPrefs.GetInt("coins") >= price)
        {
            if (PlayerPrefs.GetInt(CosmeticName) != 1)
            {
                int s = PlayerPrefs.GetInt("coins");
                PlayerPrefs.SetInt(CosmeticName, 1);
                s -= price;
                PlayerPrefs.SetInt("coins", s);
            }
            if (PlayerPrefs.GetInt(CosmeticName) == 1)
            {
                enable.SetActive(true);
                disable.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt(CosmeticName) == 1)
        {
            enable.SetActive(true);
            disable.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}