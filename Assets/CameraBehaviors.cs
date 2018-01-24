using UnityEngine;
using System.Collections;

public class CameraBehaviors : MonoBehaviour {

    //public float damping = 4f;
    //public Transform target;
    //public float distance = 9f;
    //public float height = 3f;

    //private Vector3 cameraPosition;
    //private Vector3 newFollowPosition;

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
    // Use this for initialization
    //void Start () {
    //       SetCameraPositionImmediate();

    //}

    //// Update is called once per frame
    //void Update () {
    //       if (!target)
    //           return;
    //       /*Slerp distance vector3 
    //        * transform.position + forward * -distance
    //        * 
    //        */
    //       cameraPosition = new Vector3(0, -height, distance);
    //       newFollowPosition = target.position - cameraPosition;
    //       transform.Rotate.
    //       transform.position = Vector3.Slerp(transform.position, newFollowPosition, damping);
    //}
}
