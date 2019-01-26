using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : EventableObject {

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
    private const string IEnumberatorName = "StartFade";

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

    #region Public Fade Function
    public IEnumerator StartFade()
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
    }
    #endregion

    #region override
    public override void Activate(string enmberatorName)
    {
        base.Activate(enmberatorName);
    }

    protected override void EndEvent()
    {
        base.EndEvent();
    }
    #endregion
}
