using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationControl : EventableObject {

    [Header("CameraObject")]
    [SerializeField]
    private Transform mCameraTransform;

    [Header("BooleanData")]
    [SerializeField]
    private bool isStart;

    [Header("Target")]
    [SerializeField]
    private Transform mTargetTransform;

    public Transform TargetTransform
    {
        get
        {
            return mTargetTransform;
        }
        set
        {
            mTargetTransform = value;
        }
    }

    [Header("Rotate Data")]
    [SerializeField]
    private float mRotateTime;
    [SerializeField]
    private float mRotateSpeed;

    [Header("At the same time turn on Event")]
    [SerializeField]
    private EventableObject _SameTimeEvent;

    // Use this for initialization
    void Start()
    {

    }

    public void Update()
    {

    }

    #region IEnumerator
    IEnumerator Rootine()
    {
        float _endTime = mRotateTime;
        float _nowTime = 0f;

        Quaternion targetRotation 
            = Quaternion.LookRotation(mTargetTransform.position - mCameraTransform.position);

        while (_nowTime < _endTime)
        {
            targetRotation
               = Quaternion.LookRotation(mTargetTransform.position - mCameraTransform.position);

            mCameraTransform.rotation = 
                Quaternion.Slerp(mCameraTransform.rotation, targetRotation, 
                (Time.fixedDeltaTime + _nowTime / _endTime) * mRotateSpeed);

            _nowTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        EndEvent();
        yield return null;
    }
    #endregion

    #region override
    public override void Activate()
    {
        if (_SameTimeEvent != null)
        {
            _SameTimeEvent.Activate();
        }

        base.Activate();
    }

    protected override void NextEvent()
    {
        base.NextEvent();
    }

    protected override void EndEvent()
    {
        base.EndEvent();
    }
    #endregion
}
