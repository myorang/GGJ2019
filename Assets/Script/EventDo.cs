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

    public virtual void Activate(string enmberatorName)
    {
        StartEvent(enmberatorName);
    }

    protected virtual void StartEvent(string enmberatorName)
    {
        isPlaying = true;
        StartCoroutine("Rootine");
    }

    protected virtual void EndEvent()
    {
        isPlaying = false;
    }
}
