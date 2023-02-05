using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Transform coinsParent;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int maxCoinAmount;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float spawnFrequency;
    private float coinTimer;
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
        coinTimer = spawnFrequency;
    }

    private void Update()
    {
        coinTimer -= Time.deltaTime;
        if (coins.Count < maxCoinAmount && coinTimer < Time.deltaTime)
        {
            RespawnCoin();
            coinTimer = spawnFrequency;
        }
    }

    public void CoinTaken(GameObject takenCoin)
    {
        UIManager.Instance.IncreasePoints(coinValue);
        coins.Remove(takenCoin);
        Destroy(takenCoin);
    }

    private void RespawnCoin()
    {
        Vector2 pos = Random.insideUnitCircle * spawnRadius;
        GameObject newCoin = Instantiate(coinPrefab, Vector3.zero, Quaternion.identity, coinsParent);
        newCoin.transform.position = new Vector3(pos.x, 0, pos.y);
        coins.Add(newCoin);
    }
}
