using UnityEngine;

public static class Extensions
{
   public static Vector3 DirectionTo(this Vector3 origin, Vector3 target)
   {
      return Vector3.Normalize(target - origin);
   }

   public static bool PlayerSideDetect(this Slime slime, Transform transform) //bad
   {
      return slime.player.transform.position.x > transform.position.x;
   }

   public static bool GroundCheck(this Collider2D collider2D, string layerName)
   {
      return collider2D.IsTouchingLayers(1 << LayerMask.NameToLayer(layerName));
   }
}
