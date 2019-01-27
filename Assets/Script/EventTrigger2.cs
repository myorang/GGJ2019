using UnityEngine;
using System.Collections.Generic;

public class EventTrigger2 : MonoBehaviour
{
    public List<EventableObject> m_eventlist;
    public List<GameObject> m_eventlists;
    public Collider col;

    private int m_NowPlayIdx;

    public void ActiveEvent()
    {
        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);

        col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        print("touch!");

        m_eventlist[0].Activate();
        m_NowPlayIdx = 0;
    }

    public void NextEvent()
    {
        print("next!");

        m_NowPlayIdx++;

        if (m_eventlist.Count > m_NowPlayIdx)
        {
            m_eventlist[m_NowPlayIdx].Activate();
        }
        else
        {
            if (m_IsDestroy)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public bool m_IsDestroy = false;
}
