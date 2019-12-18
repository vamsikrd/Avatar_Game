using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIState : MonoBehaviour
{
    private Animator _anim;
    private NavMeshAgent _navAgent;

    [SerializeField] private Transform targetTower = null;
    [SerializeField] private Transform defender = null;

    public int currentTowerIndex;
    public int totalTowers;
    public List<Transform> _towers = new List<Transform>();
    public GameObject[] towers;
    public bool towerSelected = false;
    public bool canAttack = false;
    [Range(0f, 5f)] public float _attackRange = 2f;

    private void Awake()
    {
        towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject tower in towers)
        {
            if (!_towers.Contains(tower.transform))
            {
                _towers.Add(tower.transform);
            }
        }
        defender = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!towerSelected)
              SelectingATower();

        else
        if(towerSelected)
        {
            if(canAttack)
            {
                Attack(targetTower);
            }
        }
    }

    private void SelectingATower()
    {
        if (canAttack) return;
        totalTowers = _towers.Count;
        currentTowerIndex = Random.Range(0, totalTowers);
        if(currentTowerIndex < totalTowers)
        {
            targetTower = _towers[currentTowerIndex];
            towerSelected = true;
            canAttack = true;
            print("!");
        }
    }

    private void Attack(Transform currentTarget)
    {
        if(currentTarget != null)
        {
            if(Vector3.Distance(transform.position,currentTarget.position) <= _attackRange)
            {
                _navAgent.ResetPath();
                print("Attack");
            }
            else
            {
                _navAgent.SetDestination(currentTarget.position);
            }
        }
        else
        if(currentTarget == null)
        {
            _towers.Remove(targetTower);
            canAttack = false;
            SelectingATower();
        }
    }

   
}
