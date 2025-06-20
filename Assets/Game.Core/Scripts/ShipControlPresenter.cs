using System;
using UnityEngine;

namespace Game.Core.Scripts
{
    public class ShipControlPresenter : MonoBehaviour
    {
        [SerializeField]
        private SpaceShipStat spaceShipStat;
        [SerializeField]
        private ShipController shipController;
        private Rigidbody _rb;
        
        [SerializeField,Range(-1f,1f)]
        private float thrustAmount;
        [SerializeField,Range(-1f,1f)]
        private float pitchAmount;
        [SerializeField,Range(-1f,1f)]
        private float yawAmount;
        [SerializeField,Range(-1f,1f)]
        private float rollAmount;
        
        [SerializeField]
        private float thrustPower = 0f;
        [SerializeField]
        private float brakePower = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _rb = GetComponent<Rigidbody>();

            shipController.OnThrustBrake += OnShipThrustBrake;
            shipController.OnThrustBrakeCanceled += OnShipThrustBrakeCanceled;
            shipController.OnBoost += OnShipBoost;
            shipController.OnBoostCanceled += OnShipBoostCanceled;
            shipController.OnPitchYaw += OnShipPitchYaw;
            shipController.OnRoll += OnShipRoll;
        }

        private void Update()
        {
            HandleThrustBrake();
            //HandleBraking();
            
        }

        private void FixedUpdate()
        {
            if (!Mathf.Approximately(0, pitchAmount))
            {
                _rb.AddRelativeTorque(transform.right * spaceShipStat.pitchForce * pitchAmount * Time.fixedDeltaTime * -1);
            }

            if (!Mathf.Approximately(0, yawAmount))
            {
                _rb.AddTorque(transform.up * spaceShipStat.yawForce * yawAmount * Time.fixedDeltaTime);
            }

            if (!Mathf.Approximately(0, rollAmount))
            {
                _rb.AddTorque(transform.forward * spaceShipStat.rollForce * rollAmount * Time.fixedDeltaTime);
            }

            switch (thrustAmount)
            {
                case > 0f:
                    _rb.linearDamping = 0f;
                    _rb.AddRelativeForce(transform.forward * thrustPower * thrustAmount * Time.fixedDeltaTime);
                    break;
                case < 0f:
                    _rb.linearDamping = brakePower;
                    break;
                default:
                    _rb.AddRelativeForce(-transform.forward * thrustPower * Time.fixedDeltaTime);
                    break;
            }
        }

        private void OnDisable()
        {
            shipController.OnThrustBrake -= OnShipThrustBrake;
            shipController.OnThrustBrakeCanceled -= OnShipThrustBrakeCanceled;
            shipController.OnBoost -= OnShipBoost;
            shipController.OnBoostCanceled -= OnShipBoostCanceled;
            shipController.OnPitchYaw -= OnShipPitchYaw;
            shipController.OnRoll -= OnShipRoll;
        }
        
        private void OnShipThrustBrake(float value)
        {
            thrustAmount = value;
        }

        private void OnShipThrustBrakeCanceled()
        {
            thrustAmount = 0f;
        }

        private void OnShipBoost()
        {
            
        }

        private void OnShipBoostCanceled()
        {
            
        }

        private void OnShipPitchYaw(Vector2 value)
        {
            pitchAmount = value.y;//Mathf.Clamp(value.x, -1f, 1f);
            yawAmount = value.x; //Mathf.Clamp(value.y, -1f, 1f);
        }

        private void OnShipRoll(float value)
        {
            rollAmount = value;
        }

        private void HandleThrustBrake()
        {
            switch (thrustAmount)
            {
                case > 0:
                    thrustPower += spaceShipStat.accelerationRate * Time.deltaTime;
                    thrustPower = Mathf.Min(thrustPower, spaceShipStat.thrustForce);
                    break;
                case < 0:
                    brakePower = thrustPower;
                    thrustPower -= spaceShipStat.decelerationRate * spaceShipStat.brakeForce * Time.deltaTime;
                    thrustPower = Mathf.Max(thrustPower, 0f);
                    break;
                default:
                    thrustPower -= spaceShipStat.decelerationRate * Time.deltaTime;
                    thrustPower = Mathf.Max(thrustPower, 0f);
                    break;
            }
        }

        private void HandlePitchYaw()
        {
            
        }
        
    }
}