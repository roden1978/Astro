using UnityEngine;

public abstract class AEnemy: ScriptableObject
{
    protected IAttackable attackBehaviour;
    protected IPatrolable patrolBehaviour;
    protected IRotatable rotateBehaviour;
    protected IGroundable groundCheckBehaviour;
    protected IHauntingable hauntBehaviour;
    
    public abstract void Attack();
    public abstract void Patrol();
    public virtual void Chase(){}
    public virtual void Flip(){}
    
    public virtual void Haunt(){}

    public virtual bool GroundCheck()
    {
        return false;
    }
}
