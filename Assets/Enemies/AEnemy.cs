using UnityEngine;

public abstract class AEnemy: ScriptableObject
{
    protected IAttackable attackBehaviour;
    protected IPatrolable patrolBehaviour;
    protected IRotatable rotateBehaviour;
    protected IGroundable groundCheckBehaviour;
    protected IHauntingable hautBehaviour;
    
    public abstract void Attack();
    public abstract void Patrol();
    public abstract void Chase();
    public abstract void Flip();

    public abstract bool GroundCheck();
}
