using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Touch touch;
    private Rigidbody playerRB;

    private bool isMoving = false;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
                isMoving = true;
            else
                isMoving = false;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector3 moveVector = new Vector3(touch.deltaPosition.x, 0, touch.deltaPosition.y);
            Debug.Log(moveVector.normalized);
            playerRB.AddForce(moveVector.normalized * moveSpeed, ForceMode.Force);
        }
    }
}
