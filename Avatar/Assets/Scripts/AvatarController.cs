using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AvatarController : MonoBehaviour
{
    private NavMeshAgent avatar_Nav;
    private Animator avatar_Anim;

    public LayerMask canHit;
    RaycastHit hit;

    void Start()
    {
        avatar_Nav = GetComponent<NavMeshAgent>();
        avatar_Anim = GetComponent<Animator>();
    }

    
    void Update()
    { 
        Avatar_Movement(RayHitPoint());
    }

    Vector3 RayHitPoint()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            if (Physics.Raycast(cursorRay, out hit, canHit))
            {
                if (hit.collider.gameObject.tag == "Enmey")
                {
                    //We need to Something related to Attack Enemy
                }
                else
                {
                    return hit.point;
                }
            }
        }

        return hit.point ;
    }

   
    private void Avatar_Movement(Vector3 hitPoint)
    {
        avatar_Nav.SetDestination(hitPoint);
        avatar_Anim.SetBool("isWalking", avatar_Nav.hasPath);
    }






} //Class






























