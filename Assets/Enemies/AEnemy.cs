using UnityEngine;
using UnityEngine.Events;

public abstract class AEnemy : MonoBehaviour
{
   protected int health;
   public UnityEvent<int> onDamage;

   protected virtual void TakeDamage(int damage)
   {
      health -= damage;
      Debug.Log($"Tag = {gameObject.tag} Health = {health}");
      if(health <= 0) Destroy(gameObject);
   }

   private void Awake()
   {
      onDamage = new UnityEvent<int>();
      onDamage.AddListener(TakeDamage);
   }
   
   private void OnDestroy()
   {
      onDamage.RemoveListener(TakeDamage);
   }
}
