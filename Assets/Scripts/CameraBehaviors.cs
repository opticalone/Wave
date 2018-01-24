using UnityEngine;
using System.Collections;

public class CameraBehaviors : MonoBehaviour
{

    public Transform camTransform;
    public Transform target;
    public Rigidbody rb;
    public float cameraDistance;

    public float moveSpeed;

    public float camMoveSpeed;
    public float camRotSpeed;

    public float slerpSpeed;

    public Vector3 cameraOffset;

    public Vector3 vel
    { get { return rb.velocity; } }


    private void Start()
    {
        SetCameraPositionImmediate();
    }
    private void LateUpdate()
    {
        SetCameraPosition();
    }

    void SetCameraPositionImmediate()
    {
        Vector3 pos = target.position;
        Vector3 dir = vel.normalized;
        camTransform.forward = dir;
        camTransform.position = pos;
        camTransform.position += dir * -cameraDistance + cameraOffset;

    }


    void SetCameraPosition()
    {
        Quaternion desiredRot = Quaternion.LookRotation((target.position - camTransform.position).normalized);
        camTransform.rotation = desiredRot;

        Vector3 dir = vel.normalized;

        Vector3 pos = target.position;
        Vector3 desiredPos = pos + dir * -cameraDistance + cameraOffset;
        camTransform.position = Vector3.Slerp(camTransform.position, desiredPos, camMoveSpeed * Time.deltaTime);

    }

}