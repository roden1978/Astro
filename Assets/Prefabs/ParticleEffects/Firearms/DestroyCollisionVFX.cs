﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollisionVFX : MonoBehaviour
{
    [SerializeField] private float delay;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

    public float Delay => delay;
}
