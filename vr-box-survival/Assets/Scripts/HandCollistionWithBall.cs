using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollistionWithBall : MonoBehaviour
{

    [SerializeField]
    Transform trainingWall;

    [SerializeField]
    VelocityCalculator velocityCalculator;

    [SerializeField]
    UI_InteractionController uiInteractionController;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ball")
        {
            //var ballRigidBody = collision.gameObject.GetComponent<Rigidbody>();

            var targetPos = new Vector3(trainingWall.position.x, GameManager.Instance.TargetBallYPos, trainingWall.position.z);

            var dir = (targetPos - collision.transform.position).normalized;
            var zVelocity = Mathf.Max(0, velocityCalculator.CalculatedVelocity.z);

            if(Mathf.Abs(GameManager.Instance.MainCamera.transform.position.y - collision.transform.position.y) > 0.2f
                || zVelocity < (GameManager.Instance.CurrentAppliedBallPower * 0.2f)) {
                GameManager.Instance.EndTraining();
                return;
            }

            GameManager.Instance.BallMoveDirection = dir;
            GameManager.Instance.LocalPlayerCurrentPower = 2 + zVelocity * 2;
            GameManager.Instance.IsBallMovingTowardsLocalPlayer = false;
            GameManager.Instance.BallRotation = velocityCalculator.CalculatedVelocity;

            GameManager.Instance.IncreaseTrainingScoreValueBy(1);

            //if (false && 10 * zVelocity < ballRigidBody.velocity.magnitude) {

            //    GameManager.Instance.EndTraining();

            //}
            //else {
            //    var force = dir * (30 + zVelocity * 100);

            //    ballRigidBody.AddForce(dir * ballRigidBody.velocity.magnitude, ForceMode.Force);

            //    ballRigidBody.AddForce(force, ForceMode.Force);
            //}
        }
    }
}
