using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerDo : MonoBehaviour
{
    public LayerMask layerMask;

    public UnityEvent Triggered;
    public UnityEvent Untriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
            Do();
    }

    private void OnTriggerExit(Collider other)
    {
        if (layerMask == (layerMask | (1 << other.gameObject.layer)))
            Undo();
    }

    private void Do()
    {
        Triggered.Invoke();
    }

    void Undo()
    {
        Untriggered.Invoke();
    }
}
