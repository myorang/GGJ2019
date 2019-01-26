using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class EventTrigger : MonoBehaviour
{
    public List<EventableObject> m_eventlist;
    public List<GameObject> m_eventlists;

    private int m_NowPlayIdx;

    public void ActiveEvent()
    {
        print("touch!");

        m_eventlist[0].Activate();
        m_NowPlayIdx = 0;
    }

    public void NextEvent()
    {
        m_NowPlayIdx++;
        if (m_eventlist.Count > m_NowPlayIdx)
        {
            m_eventlist[m_NowPlayIdx].Activate();
        }
    }
}
