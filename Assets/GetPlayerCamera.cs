using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using PlayerMovementScript;


public class GetPlayerCamera : MonoBehaviour
{
    [SerializeField] Transform playerCameraRoot;




    private void Start()
    {
        NetworkObject thisObject = GetComponent<NetworkObject>();

        if (thisObject.HasStateAuthority)
        {
            GameObject virtualCamera = GameObject.Find("PlayerFollowCamera");
            virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerCameraRoot;

             GetComponent<PlayerMovementPH>().enabled = true;
        }
    }
}
