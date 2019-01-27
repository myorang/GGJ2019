using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTrigger : EventableObject {

    [Header("Reference Obejct")]
    [SerializeField]
    private Image mFade_Object;

    [Header("Fade Control Time")]
    [SerializeField]
    private float mFadeDeltaTime;
    [SerializeField]
    private float mFadeInTime;
    [SerializeField]
    private float mFadeOutTime;
    [SerializeField]
    private float mStayFadeInTime;
    [SerializeField]
    private int mRefeatNumber;

    [Header("Boolean Data")]
    [SerializeField]
    private bool bOnFadeIn;
    [SerializeField]
    private bool bOnFadeOut;
    [SerializeField]
    private bool bOnStayFade;

    [Header("enable Obejct")]
    [SerializeField]
    private GameObject mDisableObject;
    [SerializeField]
    private GameObject mDisableFadeObject;
    [SerializeField]
    private GameObject mEnableFadeObject;

    [Header("Move Position")]
    [SerializeField]
    private Transform mMoveTarget;

    [Header("Next Event")]
    [SerializeField]
    private EventableObject _NextEvent;

    [Header("CameraTarget")]
    [SerializeField]
    private Transform mCamera;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Private Fade In/Out
    void StartFadeIn(float fadeInTime, float fixedTime)
    {
        Color tempColor = mFade_Object.color;

        mFadeDeltaTime += fixedTime;
        tempColor.a = Mathf.Lerp(0, 1, mFadeDeltaTime / fadeInTime);
        mFade_Object.color = tempColor;

        if (mFadeDeltaTime >= fadeInTime)
        {
            bOnFadeIn = false;
            bOnStayFade = true;
            mFadeDeltaTime = 0.0f;
        }
    }

    void StartFadeOut(float fadeOutTime, float fixedTime)
    {
        Color tempColor = mFade_Object.color;

        mFadeDeltaTime += fixedTime;
        tempColor.a = Mathf.Lerp(1, 0, mFadeDeltaTime / fadeOutTime);
        mFade_Object.color = tempColor;

        if (mFadeDeltaTime >= fadeOutTime)
        {
            mFadeDeltaTime = 0.0f;
            bOnFadeOut = false;
        }
    }

    void StayFade(float stayFadeTime, float fixedTime)
    {
        mFadeDeltaTime += fixedTime;

        if (mFadeDeltaTime >= stayFadeTime)
        {
            mFadeDeltaTime = 0.0f;
            bOnStayFade = false;
            bOnFadeOut = true;
        }
    }
    #endregion

    IEnumerator Rootine()
    {
        int tempRefeat = 0;

        if (mDisableObject != null)
        {
            mDisableObject.SetActive(false);
        }

        while (tempRefeat < mRefeatNumber)
        {
            bOnFadeIn = true;

            while (bOnFadeIn)
            {
                StartFadeIn(mFadeInTime, Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            if (mDisableFadeObject != null)
            {
                mDisableFadeObject.SetActive(false);
            }

            while (bOnStayFade)
            {
                StayFade(mStayFadeInTime, Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            if (mEnableFadeObject != null)
            {
                mEnableFadeObject.SetActive(true);
            }

            if (mMoveTarget != null)
            {
                mCamera.position = mMoveTarget.position;
                mCamera.GetComponent<CameraRotation>().bIsTurn = true;
            }

            while (bOnFadeOut)
            {
                StartFadeOut(mFadeOutTime, Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            tempRefeat++;
        }

        EndEvent();
    }

    #region override
    public override void Activate()
    {
        base.Activate();
    }

    protected override void NextEvent()
    {
        if (_NextEvent != null)
        {
            _NextEvent.Activate();
        }

        base.NextEvent();
    }

    protected override void EndEvent()
    {
        base.EndEvent();
    }
    #endregion
}
