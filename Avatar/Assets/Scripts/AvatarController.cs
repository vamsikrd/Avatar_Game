using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AvatarController : MonoBehaviour
{
    //Cache References//
    private NavMeshAgent _nav;
    private Animator _anim;

    //Private Variables//
    

    //Public Variables//
    public LayerMask ground;
    

    void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        ClickMovement();
    }

    private void ClickMovement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                _nav.SetDestination(hit.point);
            }
        }
        _anim.SetBool("isRunning", _nav.hasPath);
    }
} //Class






























