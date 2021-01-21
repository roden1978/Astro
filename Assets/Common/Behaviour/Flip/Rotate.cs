using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : IRotatable
{
	public void CharacterRotate(Transform character, bool movingRight)
	{
		movingRight = !movingRight;
		character.Rotate(0f, 180f, 0f);
	}
}
