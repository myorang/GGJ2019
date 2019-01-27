using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnControl : EventableObject {

    [Header("Audio Source")]
    [SerializeField]
    private AudioSource mAudioSource;

    [Header("Boolean Data")]
    [SerializeField]
    private bool bIsLoof;

    [Header("Stop Sound")]
    [SerializeField]
    private AudioSource mStopSound;

    [Header("Next Event")]
    [SerializeField]
    private EventableObject mNextEvent;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region IEnumerator
    IEnumerator Rootine()
    {
        mAudioSource.loop = bIsLoof;

        if (mStopSound != null)
        {
            mStopSound.Stop();
        }

        if (mAudioSource != null)
        {
            mAudioSource.Play();
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
        if (mNextEvent != null)
        {
            mNextEvent.Activate();
        }

        base.NextEvent();
    }

    protected override void EndEvent()
    {
        base.EndEvent();
    }
    #endregion
}
