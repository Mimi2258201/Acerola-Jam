using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public Transform ObjectToFollow;

    [Range(0, 1)]
    public float lagTime = 0.125f;
    public Vector3 offset;
    public bool objectIsTarget = false;
    public bool smoothFollow = true;
    [Tooltip("If checked the object's position will be updated during Fixed Update, otherwise Update will be used. Can help to avoid jittery looking movement.")]
    public bool fixedUpdate = true;

    Vector3 cameraVelocity = Vector3.zero;

    private void Update()
    {
        if (!fixedUpdate)
        {
            if (objectIsTarget)
            {
                Vector3 targetPosition = ObjectToFollow.position + offset;


                Vector3 desiredPosition = ObjectToFollow.position + offset;
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraVelocity, lagTime);

                if (smoothFollow)
                    transform.position = smoothedPosition;
                else
                    transform.position = desiredPosition;
            }
            else
            {
                Vector3 targetPosition = transform.position + offset;


                Vector3 desiredPosition = transform.position + offset;
                Vector3 smoothedPosition = Vector3.SmoothDamp(ObjectToFollow.position, targetPosition, ref cameraVelocity, lagTime);

                if (smoothFollow)
                    ObjectToFollow.position = smoothedPosition;
                else
                    ObjectToFollow.position = desiredPosition;
            }
        }
    }


    private void FixedUpdate()
    {
        if (fixedUpdate)
        {
            if (objectIsTarget)
            {
                Vector3 targetPosition = ObjectToFollow.position + offset;


                Vector3 desiredPosition = ObjectToFollow.position + offset;
                Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraVelocity, lagTime);

                if (smoothFollow)
                    transform.position = smoothedPosition;
                else
                    transform.position = desiredPosition;
            }
            else
            {
                Vector3 targetPosition = transform.position + offset;


                Vector3 desiredPosition = transform.position + offset;
                Vector3 smoothedPosition = Vector3.SmoothDamp(ObjectToFollow.position, targetPosition, ref cameraVelocity, lagTime);

                if (smoothFollow)
                    ObjectToFollow.position = smoothedPosition;
                else
                    ObjectToFollow.position = desiredPosition;
            }
        }
    }
}
