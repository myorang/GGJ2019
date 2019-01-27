using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : EventableObject {

    [Header("Reference Obejct")]
    [SerializeField]
    private Image mFade_Object;
    [SerializeField]
    private GameObject mTitleObject;

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
    [SerializeField]
    private bool isStart;
    [SerializeField]
    private bool bEndOfPause;
    [SerializeField]
    private bool bCheckClick = false;
    
    [Header("Next Event")]
    [SerializeField]
    private EventableObject mCameraMove;

    [Header("Active Object")]
    [SerializeField]
    private GameObject[] mEventButtonGroup;

    [Header("Target Transform")]
    [SerializeField]
    private Transform mClockTarget;
    [SerializeField]
    private Transform mFlowerTarget;
    [SerializeField]
    private Transform mWindowTarget;

    enum ButtonState
    {
        Clock, Flower, Window
    }

    private void Update()
    {
        if (isStart == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                bEndOfPause = false;
                ActiveTitle(false);
            }
        }

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

    void ActiveTitle(bool active)
    {
        if (mTitleObject != null)
        {
            mTitleObject.SetActive(active);
        }

        if (active == false)
        {
            if (mEventButtonGroup.Length > 0)
            {
                for (int index = 0; index < mEventButtonGroup.Length; index++)
                {
                    mEventButtonGroup[index].SetActive(true);
                }
            }
        }

    }
    #endregion

    #region Public Fade Function
    public IEnumerator Rootine()
    {
        int tempRefeat = 0;

        while (tempRefeat < mRefeatNumber)
        {
            bOnFadeIn = true;

            while (bOnFadeIn)
            {
                StartFadeIn(mFadeInTime, Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            while (bOnStayFade)
            {
                StayFade(mStayFadeInTime, Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            while (bOnFadeOut)
            {
                StartFadeOut(mFadeOutTime, Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            tempRefeat++;

            if (tempRefeat == 3)
            {
                ActiveTitle(true);
            }
        }

        EndEvent();
    }
    #endregion

    #region override
    public override void Activate()
    {
        isStart = true;
        base.Activate();
    }

    protected override void NextEvent()
    {
        if (!bEndOfPause)
        {
            if (mCameraMove != null)
            {
                mCameraMove.Activate();
            }
        }

        base.NextEvent();
    }

    protected override void EndEvent()
    {
        isStart = false;
        base.EndEvent();
    }
    #endregion

    #region Button Function
    public void ClockButton()
    {
        if (mClockTarget != null)
        {
            TargetObjectGroup(false);
            mCameraMove.GetComponent<CameraRotationControl>().TargetTransform = mClockTarget;
            NextEvent();
        }
    }

    public void FlowerButton()
    {
        if (mFlowerTarget != null)
        {
            TargetObjectGroup(false);
            mCameraMove.GetComponent<CameraRotationControl>().TargetTransform = mFlowerTarget;
            NextEvent();
        }
    }

    public void WindowButton()
    {
        if (mWindowTarget != null)
        {
            TargetObjectGroup(false);
            mCameraMove.GetComponent<CameraRotationControl>().TargetTransform = mWindowTarget;
            NextEvent();
        }
    } 

    private void TargetObjectGroup(bool _b)
    {
        for (int index = 0; index < mEventButtonGroup.Length; index++)
        {
            mEventButtonGroup[index].SetActive(_b);
        }
    }
    #endregion
}
