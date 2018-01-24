using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
    }

public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public Rigidbody rb;
    
    public WheelFrictionCurve normal;
    WheelFrictionCurve forwardNormal;
    public WheelFrictionCurve Drift;
    WheelFrictionCurve forwardDrift;
    private void Start()
    {
        
        normal = axleInfos[0].leftWheel.sidewaysFriction;
        forwardNormal = axleInfos[0].leftWheel.forwardFriction;
        Drift.asymptoteSlip = 1;
        Drift.asymptoteValue = 5;
        Drift.extremumSlip = 4;
        Drift.extremumValue = 1;
        
        Drift.stiffness = 1;

        forwardDrift.asymptoteSlip = 0;
        forwardDrift.asymptoteValue = 0;
        forwardDrift.extremumSlip = 0;
        forwardDrift.extremumValue = 0;
        forwardDrift.stiffness = 0;

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
        if (Input.GetKey(KeyCode.Space))
        {

            
            axleInfos[1].leftWheel.sidewaysFriction = Drift;
            axleInfos[1].rightWheel.sidewaysFriction = Drift;
            axleInfos[1].leftWheel.forwardFriction = forwardDrift;
            axleInfos[1].rightWheel.forwardFriction = forwardDrift;
            rb.drag = 0.75f;
            rb.angularDrag = 0.2f;
            axleInfos[1].leftWheel.motorTorque = 0 ;
            axleInfos[1].rightWheel.motorTorque = 0 ;
        }
        else
        {
            //axleInfos[1].leftWheel.brakeTorque = 0;
            //axleInfos[1].rightWheel.brakeTorque = 0;
            axleInfos[1].leftWheel.forwardFriction = forwardNormal;
            axleInfos[1].rightWheel.forwardFriction = forwardNormal;
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