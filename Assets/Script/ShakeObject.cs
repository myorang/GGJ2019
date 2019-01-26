using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : EventableObject {

    [Header("Reference Object")]
    [SerializeField]
    private GameObject mShakeObject;

    [Header("Shake Data")]
    [SerializeField]
    private float mShakeAmount;
    [SerializeField]
    private float mShakeDuringTime;

    #region Public Function
    public IEnumerator Rootine()
    {
        iTween.ShakePosition(mShakeObject, Vector3.one * mShakeAmount, mShakeDuringTime);
        EndEvent();
        yield return null;
    }
    #endregion

    #region override
    public override void Activate()
    {
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
