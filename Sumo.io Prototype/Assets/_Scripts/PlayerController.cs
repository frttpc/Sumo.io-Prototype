using UnityEngine;

public class PlayerController : Wrestler
{
    [SerializeField] private float rotationSpeed;

    private Touch touch;
    private Rigidbody playerRB;
    private Animator playerAnim;

    private Vector2 firstTouchPos;
    private Vector2 currentTouchPos;
    private Quaternion rotateAmount;

    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
            touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
            firstTouchPos = touch.position;
        else
            currentTouchPos = touch.position;

        Vector3 swipeVector = new Vector3(currentTouchPos.x - firstTouchPos.x, 0, currentTouchPos.y - firstTouchPos.y).normalized;
        float angle = Vector3.SignedAngle(Vector3.forward, swipeVector, new Vector3(0, 1, 0));
        rotateAmount = Quaternion.Euler(new Vector3(0, angle - 90f, 0));

        playerAnim.SetBool("isPushing", false);
    }

    private void FixedUpdate()
    {
        playerRB.MoveRotation(Quaternion.Lerp(transform.rotation, rotateAmount.normalized, rotationSpeed * Time.fixedDeltaTime));

        playerRB.AddForce(playerRB.transform.forward * acceleration, ForceMode.Force);
        if (playerRB.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            playerRB.AddForce(-acceleration * playerRB.velocity, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            return;

        Vector3 dir = (collision.gameObject.transform.position - transform.position).normalized;

        if (collision.gameObject.CompareTag("AI"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * pushAmount, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("WeakPoint"))
        {
            Debug.Log("Crit!");
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * weakPointPushAmount, ForceMode.Impulse);
        }

        playerRB.AddForce(dir * playerRB.velocity.magnitude, ForceMode.Impulse);
        playerAnim.SetBool("isPushing", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
            CoinManager.Instance.CoinTaken(other.gameObject);
    }

    public override void GivePoints(int amount)
    {
        base.GivePoints(amount);
        UIManager.Instance.IncreasePoints(amount);
    }
}
