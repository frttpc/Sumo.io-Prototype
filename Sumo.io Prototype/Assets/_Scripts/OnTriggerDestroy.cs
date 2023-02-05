using UnityEngine;

public class OnTriggerDestroy : MonoBehaviour
{
    //When an object enter death area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.GameIsEnded(0);
        }
        else
        {
            AIManager.Instance.AIDied(other.gameObject.GetComponentInParent<AIController>());
        }
    }
}
