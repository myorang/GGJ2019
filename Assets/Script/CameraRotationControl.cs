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
    public EventableObject[] _SameTimeEvent;

    [Header("Next Event")]
    [SerializeField]
    public EventableObject _NaxtEvent;

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

        while (_nowTime/_endTime < 1f)
        {
            if (Quaternion.Angle(mCameraTransform.rotation, targetRotation) <= 0.2f)
            {
                break;
            }

            targetRotation
               = Quaternion.LookRotation(mTargetTransform.position - mCameraTransform.position);
            _nowTime += Time.fixedDeltaTime;
            mCameraTransform.rotation = 
                Quaternion.Slerp(mCameraTransform.rotation, targetRotation, 
                (_nowTime / _endTime) * mRotateSpeed);

            
            _nowTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        mCameraTransform.rotation = targetRotation;

        EndEvent();
        yield return null;
    }
    #endregion

    #region override
    public override void Activate()
    {
        if (_SameTimeEvent.Length > 0)
        {
            for (int index = 0; index < _SameTimeEvent.Length; index++)
            {
                _SameTimeEvent[index].Activate();
            }
        }

        base.Activate();
    }

    protected override void NextEvent()
    {
        if (_NaxtEvent != null)
        {
            _NaxtEvent.Activate();
        }

        base.NextEvent();
    }

    protected override void EndEvent()
    {
        base.EndEvent();
    }
    #endregion
}
