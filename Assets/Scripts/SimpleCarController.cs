using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]


public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;

    public GameObject visualLeft;
    public GameObject visualRight;


    public bool motor;
    public bool steering;
}



public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public Rigidbody rb;
    public GameObject player;
    public Collider Finish;
    public Transform trans;
    Vector3 StartPos;
    

    public Text speedText;

  
    public WheelFrictionCurve normal;

    public WheelFrictionCurve Drift;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        normal = axleInfos[0].leftWheel.sidewaysFriction;
        Drift.asymptoteSlip = 5.6f;
        Drift.asymptoteValue = 5.76f;
        Drift.extremumSlip = 5.24f;
        Drift.extremumValue = 5.44f;
        StartPos.x = 671f;
        StartPos.y = 41f;
        StartPos.z = 104f;
        player.transform.position = StartPos;

        Drift.stiffness = 1;
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {

            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }



    public void Update()
    {
        var mph = rb.velocity.magnitude * 2.237;
        speedText.text = (int)mph + " Miles Per Hour";
        
        
        
            
        
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("Michael");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            axleInfos[1].leftWheel.sidewaysFriction = Drift;
            axleInfos[1].rightWheel.sidewaysFriction = Drift;
            rb.drag = 0.75f;
            rb.angularDrag = 0.1f;
        }
        else
        {
            axleInfos[1].leftWheel.sidewaysFriction = normal;
            axleInfos[1].rightWheel.sidewaysFriction = normal;
            rb.drag = 0.01f;
            rb.angularDrag = 0.05f;
        }
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");




        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {

                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);

        }
    }
}