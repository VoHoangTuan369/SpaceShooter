using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Core.Scripts
{
    public class ShipController : MonoBehaviour
    {
        [SerializeField]
        private InputController _inputController;

        public Action<float> OnThrustBrake;
        public Action OnThrustBrakeCanceled;
        public Action OnBoost;
        public Action OnBoostCanceled;
        public Action<Vector2> OnPitchYaw;
        public Action<float> OnRoll;

        private void Awake()
        {
            _inputController = new InputController();
            _inputController.Enable();
        }

        private void Start()
        {
            _inputController.SpaceShip.ThrustBrake.performed += ThrustBrake_performed;
            _inputController.SpaceShip.ThrustBrake.canceled += ThrustBrake_canceled;
            _inputController.SpaceShip.Boost.performed += Boost_performed;
            _inputController.SpaceShip.Boost.canceled += Boost_canceled;
            _inputController.SpaceShip.PitchYaw.performed += PitchYaw_performed;
            _inputController.SpaceShip.Roll.performed += Roll_performed;
        }

        private void OnDisable()
        {
            _inputController.SpaceShip.ThrustBrake.performed -= ThrustBrake_performed;
            _inputController.SpaceShip.ThrustBrake.canceled -= ThrustBrake_canceled;
            _inputController.SpaceShip.Boost.performed -= Boost_performed;
            _inputController.SpaceShip.Boost.canceled -= Boost_canceled;
            _inputController.SpaceShip.PitchYaw.performed -= PitchYaw_performed;
            _inputController.SpaceShip.Roll.performed -= Roll_performed;
            _inputController.Disable();
        }
        
        private void ThrustBrake_performed(InputAction.CallbackContext obj)
        {
            OnThrustBrake?.Invoke(obj.ReadValue<float>());
        }

        private void ThrustBrake_canceled(InputAction.CallbackContext obj)
        {
            OnThrustBrakeCanceled?.Invoke();
        }

        private void Boost_performed(InputAction.CallbackContext obj)
        {
            OnBoost?.Invoke();
        }

        private void Boost_canceled(InputAction.CallbackContext obj)
        {
            OnBoostCanceled?.Invoke();
        }
        
        private void PitchYaw_performed(InputAction.CallbackContext obj)
        {
            OnPitchYaw?.Invoke(obj.ReadValue<Vector2>());
        }

        private void Roll_performed(InputAction.CallbackContext obj)
        {
            OnRoll?.Invoke(obj.ReadValue<float>());
        }
    }
}
