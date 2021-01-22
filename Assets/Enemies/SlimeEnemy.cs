﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Slime Enemy", menuName = "Enemies/Enemy/Slime")]
public class SlimeEnemy : AEnemy
{
    [SerializeField]
    [Tooltip("Живой")]
	private bool isAlive;
	
	[SerializeField]
	[Tooltip("Игрок")]
	private GameObject player;
	
	//private SlimeController enemysObject;
	
	private Collider2D collider;
	
	

	
	private void OnEnable()
	{
		attackBehaviour = new SlimeAttack();
		patrolBehaviour = new SlimePatrol();
		hautBehaviour = new SlimeChase();
		rotateBehaviour = new Rotate();
		groundCheckBehaviour = new Ground();
	    
		//collider = enemysObject.GetComponent<BoxCollider2D>();
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

    public override void Chase()
    {
	    hautBehaviour.Haunting();
    }

    
	public override void Flip(){
		//rotateBehaviour.CharacterRotate(enemysObject.transform, enemysObject.MovingRight);
	}
	
	public override bool GroundCheck ()
	{
		return groundCheckBehaviour.CheckGround(collider, 1 << LayerMask.NameToLayer("Ground"));
	}

	public GameObject Player => player;
}