using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressButton : MonoBehaviour
{
    public LayerMask RaycastLayerMask;
    [Tooltip("If left unchecked, cursor position will be used. When true, the centre of the Camera with the 'main' tag will be used")]
    public bool UseCameraDirection;
    public UnityEvent OnHit;

    Renderer R;

    private void Start()
    {
        R = GetComponent<Renderer>();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray;
        if (UseCameraDirection)
        {
            //Using Camera position
            ray = new Ray(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward));
        }
        else
        {
            //Using Cursor position
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            //fix this so it only fires once
            if (objectHit.gameObject == gameObject && Input.GetAxis("Fire1") != 0)
            {
                hitted();
            }
        }
    }

    void hitted()
    {
        //R.enabled = false;
        OnHit.Invoke();
    }
}
