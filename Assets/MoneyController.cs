using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    GameSession Gold;
    float t;
    private void Awake()
    {
        Gold = FindObjectOfType<GameSession>();
    }
    void Start()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetFloat("Money", 0f);
            t = PlayerPrefs.GetFloat("Money");
        }
        else
        {
            t = PlayerPrefs.GetFloat("Money");
        }
        float temp = t + Gold.GetCoins();
        Debug.Log(Gold.GetCoins());
        PlayerPrefs.SetFloat("Money", temp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
