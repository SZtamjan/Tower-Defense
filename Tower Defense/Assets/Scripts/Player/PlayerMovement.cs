
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private MyControls input = null;

    private Vector3 playerVector = Vector3.zero;
    private Rigidbody rb;
    private Vector3 newSpeed;
    public float moveSpeed = 10f;
    
    private void Awake()
    {
        input = new MyControls();
        rb = GetComponent<Rigidbody>();
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
    }
    private void FixedUpdate()
    {
        rb.AddForce(playerVector.normalized * moveSpeed, ForceMode.Force);
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        playerVector = value.ReadValue<Vector3>();
        //GameManager.Instance.canvas.GetComponent<UIUpdate>().
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        playerVector = Vector3.zero;
    }
    
}
