using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.collider != null)
            {
                print(hit.transform.name);
                EventTrigger _event = hit.transform.gameObject.GetComponent<EventTrigger>();
                if (_event != null)
                    _event.ActiveEvent();
            }
        }
    }
}
