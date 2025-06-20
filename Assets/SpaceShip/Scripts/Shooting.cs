using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Core.Scripts
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] float bulletSpeed;
        [SerializeField] Transform shootingPoint;
        [SerializeField] ShipController shipController;
        [SerializeField] int bulletAmount;
        private List<GameObject> bulletPool = new List<GameObject>();
        //bool shooting = false;
        private void Start()
        {
            CreateBulletPool();
            shipController.OnShoot += HandleShooting;
        }
        private void OnDisable()
        {
            shipController.OnShoot -= HandleShooting;
        }
        //private void FixedUpdate()
        //{
        //    HandleShooting();
        //}
        void HandleShooting()
        {
            foreach (GameObject bullet in bulletPool)
            {
                if (!bullet.activeSelf)
                {                     
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = shootingPoint.forward * bulletSpeed;
                    }
                    bullet.SetActive(true);
                }
            }
        }
        void CreateBulletPool() 
        {
            for (int i = 0; i < bulletAmount; i++) 
            {
                GameObject newBullet = Instantiate(bulletPrefab);
                bulletPool.Add(newBullet);
                newBullet.SetActive(false);
            }
        }
        //#region Input Methods
        //public void OnShoot(InputAction.CallbackContext context)
        //{
        //    shooting = context.performed;
        //}
        //#endregion
    }
}