using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrestler : MonoBehaviour
{
    [SerializeField] protected float acceleration;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float pushAmount;
    [SerializeField] protected float weakPointPushAmount;
    [SerializeField] protected int points;

    public virtual void GivePoints(int givenPoints)
    {
        points = givenPoints;
    }

    public int GetPoints()
    {
        return points;
    }
}
