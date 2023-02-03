using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float totalTime;
    private int previousSecond = int.MaxValue;

    private TextMeshProUGUI TMP;

    private void Start()
    {
        TMP = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        totalTime -= Time.deltaTime;
        if (totalTime >= 0 && previousSecond != (int)totalTime)
        {
            previousSecond = (int)totalTime;
            TMP.text = ((int)totalTime).ToString();
        }
    }
}
