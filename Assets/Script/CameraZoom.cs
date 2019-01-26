using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : EventableObject
{
    public Camera m_MainCamera;
    public float m_MaxTime;
    public float m_NowTime;
    public float m_Ratio;

    public float m_BasePOV;
    public float m_ToPOV = 30f;

    private void Awake()
    {
        m_MainCamera = Camera.main;
    }

    public bool isStart = false;

#if UNITY_EDITOR
    public void Update()
    {
        if (isStart)
        {
            Activate();
            isStart = false;
        }
    }
#endif

    public override void Activate()
    {
        m_BasePOV = m_MainCamera.fieldOfView;

        base.Activate();
    }

    public IEnumerator Rootine()
    {
        while (m_NowTime < m_MaxTime)
        {
            m_MainCamera.fieldOfView = Mathf.Lerp(m_MainCamera.fieldOfView, m_ToPOV
                , (Time.fixedDeltaTime + m_NowTime / m_MaxTime) * m_Ratio);

            m_NowTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }

    protected override void EndEvent()
    {
        m_MainCamera.fieldOfView = m_ToPOV;
        base.EndEvent();
    }
}
