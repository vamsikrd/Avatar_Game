using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject _target;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - _target.transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(_target.transform.position.x + offset.x,
                                         _target.transform.position.y + offset.y,
                                         _target.transform.position.z + offset.z);
    }

} //Class
