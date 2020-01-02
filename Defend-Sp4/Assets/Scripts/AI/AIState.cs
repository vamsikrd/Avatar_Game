using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIState : MonoBehaviour
{
    private Animator _anim;
    private NavMeshAgent _navAgent;
    private int hitTimes = 3;

    [SerializeField] private Transform targetTower = null;
    [SerializeField] private Transform defender = null;

    public GameObject attackBallPrefab;
    public float ballSpeed = 10f;
    public Transform firePoint;
    public Texture2D cursorTexture;

    public int currentTowerIndex;
    public int totalTowers;
    public List<Transform> _towers = new List<Transform>();
    public GameObject[] towers;
    public bool towerSelected = false;
    public bool canAttack = false;
    [Range(0f, 10f)] public float _attackRange = 2f;
    [Range(5, 10)] public float attackOffset = 6f;
    public bool isDead;

    private int _isAttacking = Animator.StringToHash("isAttacking");
    private int _isRunning = Animator.StringToHash("isRunning");
    private int _isDead = Animator.StringToHash("isDead");
    private int _isHurt = Animator.StringToHash("isHurt");

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
        isDead = false;
    }

    private void Update()
    {
        if (isDead) return;

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

       if(_navAgent)
        {
            _anim.SetBool(_isRunning, _navAgent.hasPath);
        }
    }

    private void SelectingATower()
    {
        if (isDead) return;

        if (canAttack) return;
        totalTowers = _towers.Count;
        currentTowerIndex = Random.Range(0, totalTowers);
        if(currentTowerIndex < totalTowers)
        {
            targetTower = _towers[currentTowerIndex];
            towerSelected = true;
            canAttack = true;
        }
    }

    private void Attack(Transform currentTarget)
    {
        if (isDead) return;

        if(currentTarget != null)
        {
            if(Vector3.Distance(transform.position,currentTarget.position) <= _attackRange)
            {
                _navAgent.ResetPath();
                if(!_navAgent.hasPath)
                {
                    _anim.SetBool(_isAttacking, true);
                }
                else
                {
                    _anim.SetBool(_isAttacking, false);
                }
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
            if(_towers.Count == 0)
            {
                canAttack = true;
                targetTower = defender;
            }
            SelectingATower();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("PlayerBullet"))
        {
            hitTimes--;
            _anim.SetTrigger(_isHurt);
            if (hitTimes <= 0 && isDead == false)
            {
                FindObjectOfType<LevelManager>().RemoveDeadEnemy(this);
                isDead = true;
                FindObjectOfType<Erika_Defender>().CurrentTarget(isDead);
                _anim.SetTrigger(_isDead);
                _navAgent.ResetPath();
                Destroy(this.gameObject,2f);
            }

        }
    }

    //called by the animation event
    public void AttackEvent()
    {
        if (isDead) return;

        if(attackBallPrefab != null)
        {
            GameObject ball = Instantiate(attackBallPrefab);
            ball.transform.position = firePoint.position;
            Rigidbody ballRB = ball.GetComponent<Rigidbody>();
            if (targetTower != defender)
            {
                Vector3 throwDirection = new Vector3(targetTower.transform.position.x, targetTower.transform.position.y +
                                         attackOffset, targetTower.transform.position.z) - transform.position;
                if (ballRB)
                {
                    ballRB.AddForce(throwDirection.normalized * ballSpeed, ForceMode.Impulse);
                }
            }
            else
            if (targetTower == defender)
            {
                if (ballRB)
                {
                    Vector3 throwDirection = defender.transform.position - transform.position;
                    ballRB.AddForce(throwDirection.normalized * ballSpeed, ForceMode.Impulse);
                }
            }
        }    
    }
} //Class
