using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public float Coins { get; set; }

    void Awake()
    {
        Coins = LoadCoins();
        if (Instance != this)
        {
            Instance = this;
        }
    }

    public void AddCoins(float amount) { LoadCoins(); Coins += amount; SaveCoins(); }
    public void RemoveCoins(float amount) { LoadCoins(); Coins -= amount; SaveCoins(); }

    private void SaveCoins() { PlayerPrefs.SetFloat("COINS", Coins); }
    private float LoadCoins() { return PlayerPrefs.GetFloat("COINS", 0f); }

}
