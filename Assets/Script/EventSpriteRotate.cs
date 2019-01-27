using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpriteRotate : EventableObject
{
    public EventTrigger trigger;
    public List<EventableObject> m_ActivateWith;

    public float m_NowTime;
    public float m_EndTime;

    public RectTransform m_Target;

    public bool m_IsChangePos = false;
    public Vector2 m_BasePos;
    public Vector2 m_ToPos;

    public bool m_IsChangeAngle = false;
    public Vector3 m_BaseAngle;
    public Vector3 m_ToAngle;

    public bool m_IsChangeSize = false;
    public Vector2 m_Size;
    public Vector2 m_ToSize;

    public bool m_IsLoop = false;
    public int m_LoopCount = 0;
    public int m_LoopMaxCount = 3;

    public override void Activate()
    {
        trigger = GetComponentInParent<EventTrigger>();
        m_BasePos = m_Target.anchoredPosition;
        m_BaseAngle = m_Target.rotation.eulerAngles;
        base.Activate();

        for (int i = 0; i < m_ActivateWith.Count; i++)
        {
            print(i);
            m_ActivateWith[i].Activate();
        }
    }

    public IEnumerator Rootine()
    {
        Quaternion _toAngle = Quaternion.Euler(m_ToAngle);
        Quaternion _fromAngle = Quaternion.Euler(m_BaseAngle);
        float m_loopMaxTime = m_LoopMaxCount * m_EndTime;
        float m_loopTime = 0f;

        m_NowTime = 0;

        while (true)
        {
            if (m_IsLoop)
            {
                if (m_IsChangePos)
                    m_Target.anchoredPosition = Vector2.Lerp(m_BasePos, m_ToPos, m_NowTime / m_EndTime);

                if (m_IsChangeAngle)
                    m_Target.rotation = Quaternion.Slerp(_fromAngle, _toAngle, m_NowTime / m_EndTime);

                if (m_IsChangeSize)
                    m_Target.localScale = Vector2.Lerp(m_Size, m_ToSize, m_NowTime / m_EndTime);

                yield return new WaitForEndOfFrame();

                m_NowTime = Mathf.PingPong(m_loopTime, m_EndTime);
                m_loopTime += Time.deltaTime;

                if (m_loopTime >= m_loopMaxTime)
                    break;
                    
            }
            else
            {
                if (m_IsChangePos)
                    m_Target.anchoredPosition = Vector2.Lerp(m_BasePos, m_ToPos, m_NowTime / m_EndTime);

                if (m_IsChangeAngle)
                    m_Target.rotation = Quaternion.Slerp(_fromAngle, _toAngle, m_NowTime / m_EndTime);

                if (m_IsChangeSize)
                    m_Target.localScale = Vector2.Lerp(m_Size, m_ToSize, m_NowTime / m_EndTime);

                yield return new WaitForEndOfFrame();

                m_NowTime += Time.deltaTime;
                if (m_NowTime / m_EndTime >= 1.0f)
                    break;

            }

        }

        EndEvent();
        yield break;
    }

    protected override void EndEvent()
    {
        base.EndEvent();
    }

    protected override void NextEvent()
    {
        if (trigger != null)
            trigger.NextEvent();
        base.NextEvent();
    }

    protected override void StartEvent()
    {
        base.StartEvent();
    }
}
