using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerMeshObject;

    private DynamicJoystick joystick;
    private Rigidbody rb;
    private MainPlayer mainPlayer;
    [SerializeField] private float moveSpeed = 5f;

    private void Start()
    {
        moveSpeed = GameManager.instance.playerSpeed;
        rb = GameManager.instance.rb;
        joystick = GameManager.instance.joystick;
        mainPlayer = gameObject.GetComponent<MainPlayer>();
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.isMoveable)
            MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;

        if (movement != Vector3.zero)
        {
            Vector3 moveVector = new Vector3(movement.x, 0f, movement.z) * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + moveVector);
            mainPlayer.playerCurrentState = Player.PlayerState.Walk;
            if (playerMeshObject != null)
            {
                playerMeshObject.transform.forward = moveVector.normalized;
            }
        }
        else
            mainPlayer.playerCurrentState = Player.PlayerState.Idle;
    }
}
