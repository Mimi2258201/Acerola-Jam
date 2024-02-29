using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallFunctionFromGameobject : MonoBehaviour
{
    [SerializeField]
    UnityEvent FunctionToCall = new UnityEvent();

    // Start is called before the first frame update
    //void Start()
    //{
    //    FunctionToCall.AddListener(On_change);
    //}

    //void On_change()
    //{

    //}

    public void callFunction()
    {
        FunctionToCall.Invoke();
    }
}
