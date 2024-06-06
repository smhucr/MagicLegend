using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerMeshObject;

    private DynamicJoystick joystick;
    private Rigidbody rb;
    private MainPlayer mainPlayer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float raycastDistance = 1f; // Raycast mesafesi

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
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(joystick.Horizontal, 0f, joystick.Vertical).normalized;
        Vector3 moveVector = movement * moveSpeed * Time.fixedDeltaTime;

        if (movement != Vector3.zero)
        {
            // Hareket y�n�nde Raycast yaparak �arp��may� kontrol et (isTrigger olanlar� yok sayarak)
            if (!Physics.Raycast(transform.position, movement, raycastDistance, ~0, QueryTriggerInteraction.Ignore))
            {
                rb.MovePosition(transform.position + moveVector);
                mainPlayer.playerCurrentState = Player.PlayerState.Walk;

                if (playerMeshObject != null)
                {
                    playerMeshObject.transform.forward = movement; // moveVector.normalized yerine movement kullan�ld�
                }
            }
        }
        else
        {
            mainPlayer.playerCurrentState = Player.PlayerState.Idle;
        }
    }
}
