using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy")]
public class BaseEnemy : AEnemy
{
    [SerializeField]
    [Tooltip("Живой")]
    private bool isAlive;
    
    public BaseEnemy()
    {
        attackBehaviour = new BaseAttack();
        patrolBehaviour = new BasePatrol();

        isAlive = true;
    }

    public void ChangeAttackBehaviour(IAttackable newAttackBehaviour)
    {
        attackBehaviour = newAttackBehaviour;
    }

    public void ChangePatrolBehaviour(IPatrolable newPatrolBehaviour)
    {
        patrolBehaviour = newPatrolBehaviour;
    }
    
    public override void Attack()
    {
        attackBehaviour.Attack();
    }

    public override void Patrol()
    {
        patrolBehaviour.Patrol();
    }
}
