using UnityEngine;
[CreateAssetMenu(fileName = "New Slime Data", menuName = "Enemies/Data/Slimes")]
public class SlimeData : ScriptableObject
{
    [SerializeField]
    [Tooltip("Сила прикладываемая к телу при патрулировании")]
    public float force;
    
    [SerializeField]
    [Tooltip("Дистанция патрулирования")]
    public float patrolDistance;
	
    [SerializeField]
    [Tooltip("Дистанция при которой начинается атака")]
    public float attackDistance;
	
    [SerializeField]
    [Tooltip("Дистанция при которой завершается погоня")]
    public float stopChaseDistance;
	
    [SerializeField]
    [Tooltip("Сила прыжка")]
    public float jumpForce;
}
