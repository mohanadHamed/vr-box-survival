using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingWallCollisionWithBall : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ball") {

            var ballRigidBody = collision.gameObject.GetComponent<Rigidbody>();

            GameManager.Instance.BallPunchYLevel = Random.Range(0, 10) < 5 ? BallPunchYLevel.Upper : BallPunchYLevel.Lower;

            var targetPosition = new Vector3(GameManager.Instance.LocalPlayerGoalWallPos.x, GameManager.Instance.TargetBallYPos, GameManager.Instance.LocalPlayerGoalWallPos.z);

            var dir = (targetPosition - collision.transform.position).normalized;

            GameManager.Instance.TrainingWallCurrentPower = Mathf.Clamp(GameManager.Instance.TrainingWallCurrentPower + 1, 1, 22);

            GameManager.Instance.BallMoveDirection = dir;
            GameManager.Instance.IsBallMovingTowardsLocalPlayer = true;
           // var force = dir * trainingLevel;

           // ballRigidBody.AddForce(dir * ballRigidBody.velocity.magnitude, ForceMode.VelocityChange);

           // ballRigidBody.AddForce(force, ForceMode.Force);
        }
    }
}
