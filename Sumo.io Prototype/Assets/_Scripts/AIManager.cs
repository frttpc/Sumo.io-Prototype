using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private List<AIController> AIs = new();

    [SerializeField] private GameObject AIPrefab;
    [SerializeField] private Transform AIParent;
    [SerializeField] private int maxAIAmount;
    [SerializeField] private int AIValue;

    public static AIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        SpawnAIs();
    }

    //Spawn AIs around center evenly
    private void SpawnAIs()
    {
        for (int i = 1; i <= maxAIAmount; i++)
        {
            float radians = 2 * Mathf.PI / (maxAIAmount + 1) * i;
            float vertical = Mathf.Sin(radians);
            float horizontal = Mathf.Cos(radians);
            Vector3 spawnDir = new Vector3(horizontal, 0, vertical);

            GameObject newAI = Instantiate(AIPrefab, spawnDir * 8.5f, Quaternion.identity, AIParent);
            newAI.transform.LookAt(Vector3.zero);
            newAI.name = "AI " + i;
            AIs.Add(newAI.GetComponent<AIController>());
        }
    }

    public int GetAICount()
    {
        return AIs.Count;
    }

    //When an AI dies edit give points to the killer and edit count
    //if no one left game is ended by gameManager
    public void AIDied(AIController deadAI)
    {
        if(deadAI.GetLastTouched() != null)
            deadAI.GetLastTouched().GivePoints(AIValue);

        AIs.Remove(deadAI);
        Destroy(deadAI.gameObject);

        UIManager.Instance.DecreaseEnemyCountText(AIs.Count);

        if (AIs.Count == 0)
            GameManager.Instance.GameIsEnded(1);
    }

    //get the most point of AIs
    public int GetMostPointsOfAI()
    {
        int points = 0;

        foreach (AIController aI in AIs)
        {
            if (aI.GetPoints() > points)
                points = aI.GetPoints();
        }

        return points;
    }
}
