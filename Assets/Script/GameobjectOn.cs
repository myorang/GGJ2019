using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectOn : MonoBehaviour {
    public EventTrigger2 obj;
    public void Active()
    {
        obj.ActiveEvent();
    }
}
