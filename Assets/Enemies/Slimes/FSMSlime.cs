using System;
using UnityEngine;

public class FSMSlime : StateMachineBehaviour
{
	public SlimeEnemy enemy;
	public SlimeController slimeController;
	public GameObject player;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		slimeController = animator.gameObject.GetComponent<SlimeController>();
		//Debug.Log("Enemy controller " + slimeController);
		enemy = slimeController.SlimeEnemy;
		//Debug.Log("Enemy link " + enemy);
		player = slimeController.player;
	}

	//public AEnemy Enemy => enemy;
}
