using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnControl : EventableObject {

    [Header("Audio Source")]
    [SerializeField]
    private AudioSource mAudioSource;

    [Header("Stop Sound")]
    [SerializeField]
    private AudioSource mStopSound;

    [Header("Next Event")]
    [SerializeField]
    private EventableObject mNextEvent;

    [Header("Icon Groups")]
    [SerializeField]
    private GameObject[] Icons;

    [Header("LookAt Target")]
    [SerializeField]
    private Transform mTarget;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region IEnumerator
    IEnumerator Rootine()
    {
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
        if (Icons.Length > 0)
        {
            for (int index = 0; index < Icons.Length; index++)
            {
                //LooatAt(Icons[index]);
                Icons[index].SetActive(true);
            }
        }
        base.EndEvent();
    }
    #endregion

    private void LooatAt(GameObject iconObj)
    {
        iconObj.transform.LookAt(mTarget);
    }
}
