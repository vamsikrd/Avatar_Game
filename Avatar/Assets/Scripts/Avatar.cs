using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : AvatarBase
{
    //Inspector Assin
    [SerializeField] [Range(0f, 100f)] private float _health        = 100f; 
    [SerializeField] [Range(0f, 10f)]  private float _speed         = 10f;

    [SerializeField] private float _baseAttackRange = 5f;
    [SerializeField] private float _mediumAttackRange = 8f;
    [SerializeField] private float _powerAttackRange = 10f;

    [SerializeField] [Range(0f, 100f)] private float _baseAttackDamage = 10f;
    [SerializeField] [Range(0f, 100f)] private float _mediumAttackDamage = 15f;
    [SerializeField] [Range(0f, 100f)] private float _powerAttackDamage = 20f;
    

    protected override void Update()
    {
        AvatarMovement();
        AvatarAttackAnimation();
        
    }

    public void AvatarAttack()
    {
        if(currentAttackType == AttackType.Base_Attack)
        {
            print("range for the base attack is set here");
        }
        else
        if(currentAttackType == AttackType.Medium_Attack)
        {
            print("range for the medium attack is set here");
        }
        else
        if(currentAttackType == AttackType.Power_Attack)
        {
            print("range for the power attack is set here");
        }
    }

   
} //Class
