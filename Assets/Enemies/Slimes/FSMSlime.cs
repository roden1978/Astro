﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMSlime : StateMachineBehaviour
{
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetBool("isSliming", true);
	}
}
