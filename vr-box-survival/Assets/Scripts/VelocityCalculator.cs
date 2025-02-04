using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VelocityCalculator : MonoBehaviour
{

    public Vector3 CalculatedVelocity => getMaxelocity();

    [SerializeField]
    bool isLeft;

    [SerializeField]
    TextMeshProUGUI velocityTmPro;

    [SerializeField]
    Rigidbody rBody;

    List<Vector3> velocities;

    Vector3 lastPos;

    private void Awake() {
        velocities = new List<Vector3>();
    }
    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocities.Add((transform.position - lastPos) / Time.deltaTime);
        if(velocities.Count > 30) {
            velocities.RemoveAt(0);
        }

        //var leftRightString = isLeft ? "Left" : "Right";
        //var calculatedVelocity = getAverageVelocity();

       // velocityTmPro.text = string.Format("{0} vel = {1}, {2}, {3}", leftRightString, calculatedVelocity.x, calculatedVelocity.y, calculatedVelocity.z);

        lastPos = transform.position;
    }

    Vector3 getAverageVelocity() {
        if(velocities.Count == 0) {
            return Vector3.zero;
        }


        var sum = Vector3.zero;

        for(int i = 0; i < velocities.Count; i++) {
            sum += velocities[i];
        }

        return sum / velocities.Count;
    }

    Vector3 getMaxelocity() {
        if (velocities.Count == 0) {
            return Vector3.zero;
        }


        var velocityMax = velocities[0];
        
        for (int i = 1; i < velocities.Count; i++) {
            if(velocities[i].magnitude > velocityMax.magnitude) {
                velocityMax = velocities[i];
            }
        }

        return velocityMax;
    }
}
