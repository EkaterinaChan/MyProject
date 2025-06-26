using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZLock : MonoBehaviour
{
    [SerializeField] private float zPosition = -10f;
    private CinemachineVirtualCamera vcam;

    private void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        if (vcam != null)
        {
            CinemachineFramingTransposer transposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
            if (transposer != null)
            {
                transposer.m_CameraDistance = Mathf.Abs(zPosition);
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
    }
}