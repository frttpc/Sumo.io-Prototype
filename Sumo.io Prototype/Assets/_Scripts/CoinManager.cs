using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int maxCoinNumber;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float spawnFrequency;
    [SerializeField] private int coinValue;
    [SerializeField] private bool usePooling;

    private List<GameObject> coins = new();

    public static CoinManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void CoinTaken(GameObject takenCoin)
    {
        coins.Remove(takenCoin);
        UIManager.Instance.IncreasePoints(coinValue);
    }
}
