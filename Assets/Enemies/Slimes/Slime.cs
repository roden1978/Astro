using System;
using System.Collections.Generic;
using UnityEngine;

public class Slime : AEnemy
{
    
    private bool movingRight = true;
    public GameObject player { get; private set; }
    public SlimeData SlimeData;
    public Collider2D groundCollider;
    public Vector3 startPosition;

    public StateMashine StateMashine => GetComponent<StateMashine>();
    private Dictionary<System.Type, BaseState> states;
    private Animator animator;

    /*private void Awake()
    {
        
    }*/

    private void Start()
    {
        InitializeStateMashine();
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        health = SlimeData.MAXHealth;
        
        OnDamage.AddListener(TakeDamage);
    }

    private void Update()
    {
        animator.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPosition, new Vector3(startPosition.x, startPosition.y + 3, 0));
    }

    private void InitializeStateMashine()
    {
         states = new Dictionary<System.Type, BaseState>()
        {
            {typeof(PatrolState), new PatrolState(this)},
            {typeof(ChaseState), new ChaseState(this)},
            {typeof(FlipState), new FlipState(this)},
            {typeof(AttackState), new AttackState(this)}
        };
        
         StateMashine.SetStates(states);
    }

    public void SetTarget(GameObject target)
    {
        player = target;
    }

    public bool MovingRight
    {
        get => movingRight;
        set => movingRight = value;
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Name = {gameObject.name} Health = {health}");
        if(health <= 0) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDamage.RemoveListener(TakeDamage);
    }
}
