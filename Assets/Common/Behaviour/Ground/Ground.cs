using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : IGroundable
{
	public bool CheckGround(Collider2D collider, int layerMask)
	{
		return collider.IsTouchingLayers(layerMask);
	}
}
