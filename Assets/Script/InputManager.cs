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
                EventTrigger2 _event = hit.transform.gameObject.GetComponent<EventTrigger2>();
                if (_event != null)
                    _event.ActiveEvent();

                GameobjectOn _on = hit.transform.gameObject.GetComponent<GameobjectOn>();
                if (_on != null)
                    _on.Active();

            }
        }
    }
}
