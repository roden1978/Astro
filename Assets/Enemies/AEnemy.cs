using UnityEngine;

public abstract class AEnemy : ScriptableObject
{
    protected IAttackable attackBehaviour;
    protected IPatrolable patrolBehaviour;
    
    public abstract void Attack();
    public abstract void Patrol();
}
