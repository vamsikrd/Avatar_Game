using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class DefenderState : MonoBehaviour
{
    //Inspector Assigned
    [SerializeField] [Range(1f,10f)] private float attackRange = 5f;
    [SerializeField] private LayerMask rayLayer;
    [SerializeField] [Range(0f, 1f)] float _rotationSpeed = 0.2f;
    [SerializeField] private Transform arrowShootPoint;
    [SerializeField] private float bulletSpeed = 20f;
    
    //Protected
    protected Animator        _animator;
    protected NavMeshAgent    _navAgent;
    protected AudioSource _audioSource;
    protected int _isWalkingHash = Animator.StringToHash("isRunning");
    protected int _isAttackingHash = Animator.StringToHash("isAttacking");

    //Private
    private GameObject currentTarget = null;

    //Public
    public bool goingForAttack = false;
    public AudioClip arrowSound;
    public AudioClip[] gotEnemySound;
    private int totalClips;
    private bool audioPlayed = false;
    private bool dead = false;

    public bool playerisDead
    {
        get { return dead; }
        set { dead = value; }
    }

    private void Awake()
    {
        totalClips = gotEnemySound.Length;
    }

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = FindObjectOfType<AudioManager>().SoundVolume;
    }

    protected virtual void Update()
    {
        if (dead) return;
        ProjectRaycast();
        if (goingForAttack) CanAttack();
        _animator.SetBool(_isWalkingHash, _navAgent.hasPath);
    }


    protected virtual void Movement(RaycastHit hitInfo)
    {
        _navAgent.SetDestination(hitInfo.point);
    }

    protected virtual void ProjectRaycast()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray,out hitInfo,rayLayer))
            {
                if(hitInfo.collider.CompareTag("Enemy"))
                {
                    Attack(hitInfo);
                }
                else
                {
                    Movement(hitInfo);
                    goingForAttack = false;
                }
            }
        }
    }

    protected virtual void Attack(RaycastHit hitInfo)
    {
       GameObject newtarget = hitInfo.collider.gameObject;
       if(currentTarget != newtarget)
        {
            currentTarget = newtarget;
            audioPlayed = false;
        }
        goingForAttack = true;
    }

    protected virtual void CanAttack()
    {
        if(currentTarget == null)
        {
            goingForAttack = false;
            return;
        }
        if(currentTarget && audioPlayed == false)
        {
            _audioSource.PlayOneShot(gotEnemySound[Random.Range(0, totalClips)]);
            audioPlayed = true;
        }
        if ( goingForAttack && Vector3.Distance(transform.position, currentTarget.transform.position) <= attackRange)
        {
            _navAgent.ResetPath();
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction.y = 0f;
            direction = direction.normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1 *_rotationSpeed);
            _animator.SetTrigger(_isAttackingHash);
            _animator.SetBool(_isWalkingHash, false);
        }
        else
        {
            _animator.SetBool(_isWalkingHash, true);
            _navAgent.SetDestination(currentTarget.transform.position);
        }
    }

    //Called by animation event//
    public virtual void ShootArrow(GameObject arrowPrefab)
    {
        if (arrowPrefab != null)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            Vector3 arrowPosition = arrowShootPoint.position;
            arrow.transform.position = arrowPosition;
            arrow.transform.rotation = transform.rotation;
            arrow.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
            _audioSource.PlayOneShot(arrowSound);
            Destroy(arrow, 1f);
        }
    }

    

    public void CurrentTarget(bool isDead)
    {
        if(isDead)
        {
            currentTarget = null;
        }
    }

}//Class
