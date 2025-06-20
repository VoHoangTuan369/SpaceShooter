using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShipMoverment : MonoBehaviour
{
    [SerializeField] float yawTorque = 500f;
    [SerializeField] float pitchTorque = 1000f;
    [SerializeField] float rollTorque = 1000f;
    [SerializeField] float thrust = 1000f;
    [SerializeField] float upThrust = 1000f;
    [SerializeField] float trafeThrust = 1000f;

    [SerializeField] float maxBoostAmount = 2f;
    [SerializeField] float boostDeprecationRate = 0.25f;
    [SerializeField] float boosRechargeRate = 0.5f;
    [SerializeField] float boostMultiplier = 5f;
    public bool boosting = false;
    public float currBoostAmount;

    [SerializeField, Range(0.001f, 0.999f)] float thrustGlideReduction = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)] float upDownGlideReduction = 0.111f;
    [SerializeField, Range(0.001f, 0.999f)] float leftRightGlideReduction = 0.111f;
    float glide, verticalGlide, horizontaGlide = 0f;

    Rigidbody rb;
    float thrust1D;
    float strafe1D;
    float upDown1D;
    float roll1D;
    Vector2 pitchYaw;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currBoostAmount = maxBoostAmount;
    }
    private void FixedUpdate()
    {
        HandleBoosting();
        HandleMoverment();
    }
    void HandleBoosting() 
    {
        if (boosting && currBoostAmount > 0f)
        {
            currBoostAmount -= boostDeprecationRate;
            if (currBoostAmount <= 0f) boosting = false;
        }
        else 
        {
            if (currBoostAmount < maxBoostAmount) currBoostAmount += boosRechargeRate;
        }
    }
    void HandleMoverment()
    {
        //Roll
        rb.AddRelativeTorque(Vector3.back * roll1D * rollTorque * Time.deltaTime);
        //Pitch
        rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(-pitchYaw.y, -1f, 1f) * pitchTorque * Time.deltaTime);
        //Yaw
        rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(pitchYaw.x, -1f, 1f) * yawTorque * Time.deltaTime);
        //Thrust
        if (thrust1D > 0.1f || thrust1D < -0.1f)
        {
            float currThrust;
            if (boosting) currThrust = thrust * currBoostAmount;
            else currThrust = thrust;
            rb.AddRelativeForce(Vector3.forward * thrust1D * currThrust * Time.deltaTime);
            glide = thrust;
        }
        else 
        {
            rb.AddRelativeForce(Vector3.forward * glide * Time.deltaTime);
            glide *= thrustGlideReduction;
        }
        //Up/Down
        if (upDown1D > 0.1f || upDown1D < -0.1f)
        {
            rb.AddRelativeForce(Vector3.up * upDown1D * upThrust * Time.deltaTime);
            verticalGlide = upDown1D * upThrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.up * verticalGlide * Time.deltaTime);
            verticalGlide *= upDownGlideReduction;
        }
        //Strafing
        if (strafe1D > 0.1f || strafe1D < -0.1f)
        {
            rb.AddRelativeForce(Vector3.right * strafe1D * upThrust * Time.deltaTime);
            horizontaGlide = strafe1D * trafeThrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.right * horizontaGlide * Time.deltaTime);
            horizontaGlide *= leftRightGlideReduction;
        }
    }
    #region Input Methods
    public void OnThrust(InputAction.CallbackContext context)
    {
        thrust1D = context.ReadValue<float>();
    }

    public void OnStrafe(InputAction.CallbackContext context)
    {
        strafe1D = context.ReadValue<float>();
    }

    public void OnUpDown(InputAction.CallbackContext context)
    {
        upDown1D = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        roll1D = context.ReadValue<float>();
    }

    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        pitchYaw = context.ReadValue<Vector2>();
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        boosting = context.performed;
    }
    #endregion
}
