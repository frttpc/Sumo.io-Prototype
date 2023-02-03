using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDestroy : MonoBehaviour
{
    [SerializeField] private bool destroySelf;

    private void OnTriggerEnter(Collider other)
    {
        if(destroySelf)
            Destroy(gameObject);
        else
            Destroy(other.gameObject);
    }
}
