using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingControl : EventableObject {

    [Header("PostProcessing Control")]
    [SerializeField]
    private PostProcessingProfile mPostProcessing;

    [Header("Depth Data")]
    [SerializeField]
    private float mGoalDepth;
    [SerializeField]
    private float mDepthTime;
    [SerializeField]
    private bool bIsUpper;
    [SerializeField]
    private bool bIsDowner;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region IEnumerator
    IEnumerator Rootine()
    {
        float tempTime = 0;
        DepthOfFieldModel.Settings tempDepth = new DepthOfFieldModel.Settings();
        tempDepth = mPostProcessing.depthOfField.settings;
        float originfocalLength = tempDepth.focalLength;

        if (bIsUpper)
        {
            while (tempTime >= 1)
            {
                tempTime += Time.fixedDeltaTime;
                tempDepth.focalLength = Mathf.Lerp(originfocalLength, mGoalDepth, tempTime / mDepthTime);

                mPostProcessing.depthOfField.settings = tempDepth;
                yield return new WaitForFixedUpdate();
            }
        }

        if (bIsDowner)
        {
            while (tempTime >= 1)
            {
                tempTime += Time.fixedDeltaTime;
                tempDepth.focalLength = Mathf.Lerp(mGoalDepth, originfocalLength, tempTime / mDepthTime);
                mPostProcessing.depthOfField.settings = tempDepth;
                yield return new WaitForFixedUpdate();
            }
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
