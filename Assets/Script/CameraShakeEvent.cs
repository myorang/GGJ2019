using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeEvent : MonoBehaviour {

    [Header("Reference Object")]
    [SerializeField]
    private Transform mCameraObject;

    [Header("Shake Detail Data")]
    [SerializeField]
    private float mShakeDuringTime;
    [SerializeField]
    private float mMagnitued;
    [SerializeField]
    private float mShakeDeltaTime;

    public void StartShake()
    {

    }

  
}
