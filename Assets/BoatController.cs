using UnityEngine;

public class BoatController : MonoBehaviour
{
    Rigidbody rigidBody;
    SailboatControls sailboatControls;
    Vector2 moveInput;

    public float force = 2f;
    public float torque = 0.5f;

    //todo Engine class
    private bool _isRunning = false;

    void Awake()
    {
        sailboatControls = new SailboatControls();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        sailboatControls.Player.Enable();
    }

    private void Update()
    {
        if (sailboatControls.Player.Engine.triggered)
        {
            _isRunning = !_isRunning;
            Debug.Log(_isRunning);
        }
    }

    void FixedUpdate()
    {
        moveInput = sailboatControls.Player.Move.ReadValue<Vector2>();
      
        //todo lerp
        //rigidBody.AddRelativeForce(Vector3.Slerp(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, moveInput.y), Time.deltaTime) * force, ForceMode.Acceleration);
        rigidBody.AddRelativeForce(new Vector3(0f, 0f, moveInput.y) * force, ForceMode.Acceleration);
        
        if (Mathf.Abs(moveInput.y) > 0)
            rigidBody.AddRelativeTorque(new Vector3(0f, Mathf.Sign(moveInput.y) * moveInput.x, 0f) * torque, ForceMode.Acceleration);
    }
}
