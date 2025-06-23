using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Core.Scripts
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] float bulletSpeed;
        [SerializeField] int bulletAmount;
        [SerializeField] Transform shootingPoint;
        [SerializeField] ShipController shipController;
        int currBulletAmount;

        private void Start()
        {
            BulletPool.Instance.InitializePool(bulletAmount);
            currBulletAmount = bulletAmount;
            shipController.OnShoot += HandleShooting;
        }

        private void OnDisable()
        {
            shipController.OnShoot -= HandleShooting;
        }

        void HandleShooting()
        {
            if (currBulletAmount == 0)
            {
                StartCoroutine(ReloadBullet());
                return;
            }
            GameObject bullet = BulletPool.Instance.GetBullet();
            if (bullet == null) return;

            bullet.transform.position = shootingPoint.position;
            bullet.transform.rotation = shootingPoint.rotation;
            bullet.SetActive(true);
            currBulletAmount--;

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = shootingPoint.forward * bulletSpeed;
            }
        }
        IEnumerator ReloadBullet() 
        {
            yield return new WaitForSeconds(2f);
            BulletPool.Instance.ReturnBullet();
            currBulletAmount = bulletAmount;
        }
    }
}