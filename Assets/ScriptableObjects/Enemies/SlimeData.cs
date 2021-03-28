using UnityEngine;
[CreateAssetMenu(fileName = "New Slime Data", menuName = "Enemies/Data/Slimes")]
public class SlimeData : ScriptableObject
{
    [SerializeField]
    [Tooltip("Сила прикладываемая к телу при патрулировании")]
    private float force;
    
    [SerializeField]
    [Tooltip("Дистанция патрулирования")]
    private float patrolDistance;
	
    [SerializeField]
    [Tooltip("Дистанция при которой начинается атака")]
    private float attackDistance;
	
    [SerializeField]
    [Tooltip("Дистанция при которой завершается погоня")]
    private float stopChaseDistance;
	
    [SerializeField]
    [Tooltip("Сила прыжка")]
    private float jumpForce;
    
    [SerializeField]
    [Tooltip("Максимальное ускорение движения")]
    private float maxVelocity; [SerializeField]
    
    [Tooltip("Максимальное значение здоровья")]
    private int maxHealth;

    public float Force
    {
	    get => force;
	    set => force = value;
    }

    public float PatrolDistance
    {
	    get => patrolDistance;
	    set => patrolDistance = value;
    }

    public float AttackDistance
    {
	    get => attackDistance;
	    set => attackDistance = value;
    }

    public float StopChaseDistance
    {
	    get => stopChaseDistance;
	    set => stopChaseDistance = value;
    }

    public float JumpForce
    {
	    get => jumpForce;
	    set => jumpForce = value;
    }

    public float GetMaxVelocity
    {
	    get => maxVelocity;
	    set => maxVelocity = value;
    }

    public int MAXHealth => maxHealth;
}
