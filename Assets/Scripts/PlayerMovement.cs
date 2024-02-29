using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float resistance;

    CharacterController characterController;
    Vector3 velocity = Vector3.zero;
    Vector3 previousVelocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movementDir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementDir += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDir += Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementDir += Vector3.left;
        }
        // WASD movement

        Vector3 movement = movementDir.normalized * speed * Time.deltaTime;
        velocity += movement;

        // Reduce speed over time
        if (velocity.magnitude < 0.01f && movementDir.magnitude == 0f)        
            velocity = Vector3.zero;
        // If close to zero then stop
        
        else        
            velocity -= velocity.normalized * resistance * Time.deltaTime;
        

        velocity = ClampVector3(velocity, -maxSpeed, maxSpeed);
        characterController.Move(velocity);
        previousVelocity = velocity;
    }

    Vector3 ClampVector3(Vector3 vector, float min, float max)
    {
        return new Vector3(Mathf.Clamp(vector.x, min, max), Mathf.Clamp(vector.y, min, max), Mathf.Clamp(vector.z, min, max));
    }
}
