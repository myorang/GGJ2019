using UnityEngine;
using System.Collections;

public class EventableObject : MonoBehaviour
{
    protected bool isPlaying;
    public bool IsPlaying
    {
        get
        {
            return isPlaying;
        }
    }

    public virtual void Activate()
    {
        StartEvent();
    }

    protected virtual void StartEvent()
    {
        isPlaying = true;
        StartCoroutine("Rootine");
    }

    protected virtual void NextEvent()
    {
    }

    protected virtual void EndEvent()
    {
        isPlaying = false;
        NextEvent();
    }
}
