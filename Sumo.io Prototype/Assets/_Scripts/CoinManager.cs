using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int maxCoinNumber;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float spawnFrequency;
    [SerializeField] private bool usePooling;

    private List<GameObject> coins = new();

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
