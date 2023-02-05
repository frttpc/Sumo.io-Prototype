using UnityEngine;

public class AIController : Wrestler
{
    [SerializeField] private GameObject target;

    [SerializeField] private int decisiveness;
    private int decisivenessCoeff;
    private float hunterCoeff;
    private float gathererCoeff;
    private Wrestler lastTouched = null;
    
    private Rigidbody AIRB;
    private Animator AIAnim;

    private void Awake()
    {
        AIRB = GetComponent<Rigidbody>();
        AIAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        decisivenessCoeff = Random.Range(10, decisiveness);
        hunterCoeff = Random.Range(1,5);
        gathererCoeff = Random.Range(1,5);
    }

    private void Update()
    {
        if (target == null)
            AquireTarget();
        if (Random.Range(1, decisivenessCoeff) == 1)
            AquireTarget();

        AIAnim.SetBool("isPushing", false);
    }

    private void FixedUpdate()
    {


        //AIRB.AddForce(AIRB.transform.forward * acceleration, ForceMode.Force);
        if (AIRB.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            AIRB.AddForce(-acceleration * AIRB.velocity, ForceMode.Force);
    }

    private void AquireTarget()
    {
        Collider[] objects = Physics.OverlapSphere(AIRB.transform.position, 9);

        float closestEnemyDist = float.MaxValue;
        float closestCoinDist = float.MaxValue;
        GameObject closestEnemy = null;
        GameObject closestCoin = null;

        for (int i = 0; i < objects.Length; i++)
        {
            GameObject selectedObject = objects[i].gameObject;
            if(!selectedObject.transform.IsChildOf(gameObject.transform))
            {
                float dist = (selectedObject.transform.position - AIRB.transform.position).sqrMagnitude;

                if (selectedObject.CompareTag("AI") || selectedObject.CompareTag("Player"))
                {
                    if (dist < closestEnemyDist)
                    {
                        closestEnemy = selectedObject;
                        closestEnemyDist = dist;
                    }
                }
                else if (selectedObject.CompareTag("Coin") && dist < closestCoinDist)
                {
                    closestCoin = selectedObject;
                    closestCoinDist = dist;
                }
            }
        }

        if (closestCoinDist / gathererCoeff > closestEnemyDist / hunterCoeff)
            target = closestEnemy;
        else
            target = closestCoin;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            return;

        Vector3 dir = (collision.gameObject.transform.position - transform.position).normalized;

        if (collision.gameObject.CompareTag("AI"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * pushAmount, ForceMode.Impulse);
            lastTouched = collision.gameObject.GetComponentInParent<Wrestler>();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * pushAmount, ForceMode.Impulse);
            lastTouched = collision.gameObject.GetComponentInParent<Wrestler>();
        }
        else if (collision.gameObject.CompareTag("WeakPoint"))
        {
            collision.gameObject.GetComponentInParent<Rigidbody>().AddForce(dir * weakPointPushAmount, ForceMode.Impulse);
            lastTouched = collision.gameObject.GetComponentInParent<Wrestler>();
        }

        AIRB.AddForce(0.5f * pushAmount * -dir, ForceMode.Impulse);
        AIAnim.SetBool("isPushing", true);
    }

    public Wrestler GetLastTouched()
    {
        return lastTouched;
    }
}
