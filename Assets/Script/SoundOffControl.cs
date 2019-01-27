﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOffControl : EventableObject {

    [Header("Audio Source")]
    [SerializeField]
    private AudioSource mAudioSource;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region IEnumerator
    IEnumerator Rootine()
    {
        if (mAudioSource != null)
        {
            if (mAudioSource.isPlaying)
            {
                mAudioSource.Stop();
            }
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
        base.NextEvent();
    }

    protected override void EndEvent()
    {
        base.EndEvent();
    }
    #endregion
}