using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AttackType { None,Base_Attack, Medium_Attack, Power_Attack}



public abstract class AvatarBase : MonoBehaviour
{
    //Private
    private AttackType _currentAttackType = AttackType.None;

    //Cache References
    private Animator          _animator;
    private NavMeshAgent      _navAgent;

    //Hashes
    private int _walkingHash        = Animator.StringToHash("isWalking");
    private int _baseAttackHash     = Animator.StringToHash("BaseAttack");
    private int _mediumAttackHash   = Animator.StringToHash("MediumAttack");
    private int _powerAttackHash    = Animator.StringToHash("PowerAttack");
   

    public Animator     animator          { get { return _animator; } }
    public NavMeshAgent navAgent          { get { return _navAgent; } }
    public AttackType   currentAttackType { get { return _currentAttackType; } }
   

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update() { }

    protected virtual void AvatarMovement()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                _navAgent.SetDestination(hit.point);
            }
        }
        if(_navAgent.remainingDistance < 0.2f)
        {
            _animator.SetBool(_walkingHash, false);
        }
        else
        {
            _animator.SetBool(_walkingHash, true);
        }
    }

    protected virtual void AvatarAttackAnimation()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            print("base attak");
            _animator.SetTrigger(_baseAttackHash);
            _currentAttackType = AttackType.Base_Attack;
        }
        else
        if(Input.GetKeyDown(KeyCode.W))
        {
            print("second attack");
            _animator.SetTrigger(_mediumAttackHash);
            _currentAttackType = AttackType.Medium_Attack;
        }
        else
        if(Input.GetKeyDown(KeyCode.E))
        {
            print("Power attack");
            _animator.SetTrigger(_powerAttackHash);
            _currentAttackType = AttackType.Power_Attack;
        }
    }

} //Class
