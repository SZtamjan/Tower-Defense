using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private MyControls input = null;

    private Vector3 playerVector = Vector3.zero;
    private Rigidbody rb;
    private Vector3 newSpeed;
    public float moveSpeed = 10f;
    public float playerHeight;

    
    //limit movement
    private Transform gO;
    private float playerPosX;
    private float playerPosZ;
    private Vector3 limitedPos;
    private Vector3 currentVelocity;
    
    //Limit Transforms
    public Transform oFrontRight;
    public Transform oBackLeft;
    //Positions From Objects
    private float xFront;
    private float xBack;
    private float zRight;
    private float zLeft;

    private void Awake()
    {
        limitedPos.y = playerHeight;
        input = new MyControls();
        gO = gameObject.transform;
        rb = GetComponent<Rigidbody>();
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
    }

    private void Start()
    {
        xFront = oFrontRight.position.x;
        xBack = oBackLeft.position.x;
        zLeft = oBackLeft.position.z;
        zRight = oFrontRight.position.z;
    }

    private void Update()
    {
        playerPosX = gO.position.x;
        playerPosZ = gO.position.z;
        limitedPos.x = Mathf.Clamp(playerPosX, xBack, xFront);
        limitedPos.z = Mathf.Clamp(playerPosZ, zLeft, zRight);

        gO.position = limitedPos;
    }

    private void FixedUpdate() //https://answers.unity.com/questions/1508244/my-player-shakes-when-mathfclamp-stop-it-from-goin.html
    {
        Vector3 targetVelocity = new Vector3(playerVector.x, 0, playerVector.z) * moveSpeed;
        Vector3 smoothVelocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, 0.3f);
        
        rb.velocity = smoothVelocity;
        //Debug.Log(rb.velocity);
        // rb.AddForce(playerVector.normalized * moveSpeed, ForceMode.Force);
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        //playerVector = transform.TransformVector(value.ReadValue<Vector3>()).normalized; //Travel in correct way when rotated
        playerVector = value.ReadValue<Vector3>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        playerVector = Vector3.zero;
    }
    
}
