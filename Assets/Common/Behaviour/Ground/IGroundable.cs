﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGroundable
{
	bool CheckGround(Collider2D collider, int layerMask);
}
