using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Touch touch;
    private Rigidbody playerRB;
    private Animator animator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushAmount;
    [SerializeField] private float weakPointPushAmount;

    private Vector2 firstTouchPos;
    private Vector2 currentTouchPos;

    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
            touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
            firstTouchPos = touch.position;
        else
            currentTouchPos = touch.position;
    }

    private void FixedUpdate()
    {
        Vector3 swipeVector = new Vector3(currentTouchPos.x - firstTouchPos.x, 0, currentTouchPos.y - firstTouchPos.y).normalized;

        if (swipeVector.sqrMagnitude > 0)
        {
            Debug.Log(Vector3.SignedAngle(Vector3.right, swipeVector, new Vector3(0,1,0)));
        }

        //playerRB.AddForce(Vector3.forward * moveSpeed, ForceMode.Force);
        float angle = Vector3.SignedAngle(Vector3.forward, swipeVector, new Vector3(0, 1, 0));
        Quaternion rotateAmount = Quaternion.Euler(new Vector3(0, angle, 0));
        playerRB.MoveRotation(rotateAmount);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Vector3 dir = collision.gameObject.transform.position - transform.position;

    //    if (collision.gameObject.CompareTag("AI"))
    //        collision.gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * pushAmount, ForceMode.Force);

    //    else if(collision.gameObject.CompareTag("WeakPoint"))
    //        collision.gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * weakPointPushAmount, ForceMode.Force);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
            CoinManager.Instance.CoinTaken(other.gameObject);
    }
}
