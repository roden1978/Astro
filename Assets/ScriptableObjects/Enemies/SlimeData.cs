using UnityEngine;
[CreateAssetMenu(fileName = "New Slime Data", menuName = "Enemies/Data/Slimes")]
public class SlimeData : ScriptableObject
{
    [SerializeField]
    [Tooltip("Скорость при патрулировании")]
    private float speed;
    
    [SerializeField]
    [Tooltip("Скорость при погоне за игроком")]
    private float attackSpeed;
	
    [SerializeField]
    [Tooltip("Дистанция патрулирования")]
    private float patrolDistance;
	
    [SerializeField]
    [Tooltip("Дистанция при которой начинается атака")]
    private float attackDistance;
	
    [SerializeField]
    [Tooltip("Дистанция при которой завершается погоня")]
    private float stopAngryDistance;
	
    [SerializeField]
    [Tooltip("Максимальное ускорение во время прыжка")]
    private float maxJumpVelocity;
	
    [SerializeField]
    [Tooltip("Максимальное ускорение при отскоке после атаки")]
    private float maxBackVelocity;
	
    [SerializeField]
    [Tooltip("Сила прыжка")]
    private float jumpForce;
}
