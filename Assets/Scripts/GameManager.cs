using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] GameObject spaceShipPrefab;
    SpaceShip currShip;
    void Start()
    {
        SpawnSpaceShip();
    }

    void SpawnSpaceShip()
    {
        GameObject newShip = Instantiate(spaceShipPrefab, Vector3.zero, Quaternion.identity);

        SpaceShip shipScript = newShip.GetComponent<SpaceShip>();
        if (shipScript == null || shipScript.targetCamera == null) return;
        currShip = shipScript;
        cinemachineCamera.Follow = currShip.targetCamera;
        cinemachineCamera.LookAt = currShip.targetCamera;
    }
}
