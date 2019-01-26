using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectControl : EventableObject {

    [Header("Move Object")]
    [SerializeField]
    private GameObject mMoveObject;

    [Header("Move Path")]
    [SerializeField]
    private iTweenPath mPath;

    [Header("Tween Time ")]
    [SerializeField]
    private float mTweenTime;

    [Header("ITween Behavior")]
    [SerializeField]
    private iTweenEvent mBehavior;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region IEnumerator 
    IEnumerator Rootine()
    {
        mBehavior.Play();

        float tempTime = 0;

        while (true)
        {
            tempTime += Time.fixedDeltaTime;
            
            if (tempTime >= mTweenTime)
            {
                break;
            }

            yield return new WaitForFixedUpdate();
        }
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
