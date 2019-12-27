using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    Vector3 zoomUpdate = Vector3.zero;
    Vector3 cameraPosition;
    float zoom;
    [SerializeField] float zoomSpeed = 10f;

    public LayerMask groundLayer;

    private void Start()
    {
        cameraPosition = transform.position;
    }

    private void Update()
    {
        //CameraZoom();
    }

    private void MousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray,out hitInfo,groundLayer))
        {

        }
    }

    private void CameraZoom()
    {
        zoom = Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        zoomUpdate.y = zoom;
        cameraPosition.y -= zoomUpdate.y;
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, 10f, 20f);
        transform.position = cameraPosition;
    }
} //Class
