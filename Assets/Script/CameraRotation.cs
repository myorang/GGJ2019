using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    private Gyroscope mGyroscope;
    private bool bIsSupportGyroscope;
    private bool bIsActiveGyroscope;

	// Use this for initialization
	void Start () {

        StartGyroscope();
        ActiveGyroscope(bIsSupportGyroscope);
	}
	
	// Update is called once per frame
	void Update () {

		if (bIsSupportGyroscope)
        {
            transform.rotation = mGyroscope.attitude;
        }

	}

    void StartGyroscope()
    {
        bIsSupportGyroscope = SystemInfo.supportsGyroscope;

        if (bIsSupportGyroscope)
        {
            mGyroscope = Input.gyro;
        }
    }

    void ActiveGyroscope(bool active)
    {
        mGyroscope.enabled = active;
        bIsSupportGyroscope = active;
    }
}
