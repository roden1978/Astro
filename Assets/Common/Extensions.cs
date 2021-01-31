using UnityEngine;

public static class Extensions
{
   public static Vector3 DirectionTo(this Vector3 origin, Vector3 target)
   {
      return Vector3.Normalize(target - origin);
   }

   public static void VelocityControl(this Rigidbody2D rb, float maxVelocity)
   {
      rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
   }

   public static bool PlayerSideDetect(this Slime slime, Transform transform)
   {
      return slime.player.transform.position.x > transform.position.x;
   }
}
