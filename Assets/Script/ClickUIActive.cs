using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickUIActive : EventableObject {

    [SerializeField]
    private GameObject mClickUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Rootine()
    {
        mClickUI.SetActive(true);
        yield return null;
    }

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
