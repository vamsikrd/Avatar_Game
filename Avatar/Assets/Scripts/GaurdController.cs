using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class GaurdController : MonoBehaviour
{
    //Cache Variables//
    private NavMeshAgent _nav;
    private Animator _anim;

    //Private Variables//
    [SerializeField] private List<Tomb> tombs = new List<Tomb>();
    [SerializeField] private Transform target;
    private bool gotNearest = false;
    private float nearestTomb;
    //Public Variables//

    private void Awake()
    {
       
    }


    private void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        nearestTomb = Vector2.Distance(transform.position, FindObjectOfType<Tomb>().transform.position);
       
    }


    private void Update()
    {
        GettingTheNearestTomb();
        _nav.SetDestination(transform.position);
        
    }

    private void GettingTheNearestTomb()
    {
        if (!gotNearest)
        {
            foreach (Tomb tomb in tombs)
            {
                if (nearestTomb >= Vector2.Distance(transform.position, tomb.transform.position))
                {
                    target = tomb.transform;
                }
            }
            gotNearest = true;
        }
    }
}
