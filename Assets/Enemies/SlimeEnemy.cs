using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slime Enemy", menuName = "Enemies/Enemy/Slime")]
public class SlimeEnemy : AEnemy
{
    [SerializeField]
    [Tooltip("Живой")]
	private bool isAlive;
	
	[SerializeField]
	[Tooltip("Игрок")]
	private GameObject player;
	
	private SlimeController enemysObject;
	private Collider2D collider;
	
	private IRotatable rotateBehaviour;
	private IGroundable groundCheckBehaviour;
    
	public SlimeEnemy(SlimeController enemysObject)
    {
	    attackBehaviour = new SlimeAttack();
	    patrolBehaviour = new SlimePatrol();
	    rotateBehaviour = new Rotate();
	    groundCheckBehaviour = new Ground();
	    
	    enemysObject = enemysObject;
	    
	    collider = enemysObject.GetComponent<BoxCollider2D>();
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
    
	public void Flip(){
		rotateBehaviour.CharacterRotate(enemysObject.transform, enemysObject.MovingRight);
	}
	
	public bool GetGround ()
	{
		return groundCheckBehaviour.CheckGround(collider, 1 << LayerMask.NameToLayer("Ground"));
	}
    
}
