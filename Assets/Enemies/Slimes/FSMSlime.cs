using System;
using UnityEngine;

public class FSMSlime : StateMachineBehaviour
{
	private AEnemy enemy;
	public SlimeController slimeController;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		slimeController = animator.gameObject.GetComponent<SlimeController>();
		enemy = animator.gameObject.GetComponent<SlimeController>().SlimeEnemy;
	}

	public AEnemy Enemy => enemy;
}
