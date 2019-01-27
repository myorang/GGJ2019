using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetToMoveControl : EventableObject{

    [SerializeField]
    private Transform mMoveObject;

    [SerializeField]
    private Transform mTarget;

    [SerializeField]
    private float mNowTime;
    [SerializeField]
    private float mEndTime;

    [SerializeField]
    private EventableObject _NextObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Rootine()
    {
        mNowTime = 0;
        bool bNext = false;

        while(mNowTime < 1)
        {
            mNowTime += Time.fixedDeltaTime;

            mMoveObject.transform.position = Vector3.Lerp(mMoveObject.position, mTarget.position, mNowTime / mEndTime);
            yield return new WaitForFixedUpdate();

            if (mNowTime > 0.85f && !bNext)
            {
                bNext = true;
                _NextObject.Activate();
            }
        }

        mMoveObject.gameObject.SetActive(false);

        EndEvent();
    }

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
