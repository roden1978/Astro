using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Turret : AEnemy
{
    public GameObject player { get; private set; }

    public Transform turretHead;
    public TurretSettings turretSettings;
    private float upDirection;
    
    public StateMashine StateMashine => GetComponent<StateMashine>();
    private Dictionary<System.Type, BaseState> states;

    /*private void Awake()
    {
        
    }*/

   private void Start()
    {
        InitializeStateMashine();
        upDirection = Random.Range(-1, 1);
        if (UpDirection == 0) UpDirection = 1;
        OnDamage.AddListener(TakeDamage);
        health = turretSettings.MaxHealth;
    }


    private void InitializeStateMashine()
    {
        states = new Dictionary<System.Type, BaseState>()
        {
            {typeof(TurretPatrolState), new TurretPatrolState(this)},
            {typeof(ChangeRotateDirectionState), new ChangeRotateDirectionState(this)}
        };
        
        GetComponent<StateMashine>().SetStates(states);
    }

    public void SetTarget(GameObject target)
    {
        player = target;
    }

    public float UpDirection
    {
        get => upDirection;
        set => upDirection = value;
    }

    public override void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Tag = {gameObject.tag} Health = {health}");
        if(health <= 0) Destroy(gameObject);

    }

    private void OnDestroy()
    {
        OnDamage.RemoveListener(TakeDamage);
    }
}
