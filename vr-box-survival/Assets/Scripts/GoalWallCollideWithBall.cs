using UnityEngine;

public class GoalWallCollideWithBall : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ball") {

            GameManager.Instance.EndTraining();
        }
    }
}
