using UnityEngine;

public static class Extensions
{
   public static Vector3 DirectionTo(this Vector3 origin, Vector3 target)
   {
      return Vector3.Normalize(target - origin);
   }

   public static bool PlayerSideDetect(this Slime slime, Transform transform)
   {
      return slime.player.transform.position.x > transform.position.x;
   }
}
