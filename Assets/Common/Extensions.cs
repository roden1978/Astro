using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
   public static Vector3 DirectionTo(this Vector3 origin, Vector3 target)
   {
      return Vector3.Normalize(target - origin);
   }
}
