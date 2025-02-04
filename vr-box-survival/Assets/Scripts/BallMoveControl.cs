using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoveControl : MonoBehaviour
{
    Transform ballParentTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        ballParentTransform = transform.parent;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(GameManager.Instance.BallRotation);

        if (GameManager.Instance.FreezeBallMovement) return;

        if (GameManager.Instance.BallMoveDirection.magnitude > Mathf.Epsilon) {
            ballParentTransform.position += GameManager.Instance.BallMoveDirection * GameManager.Instance.CurrentAppliedBallPower * Time.deltaTime;
        }
    }
}
