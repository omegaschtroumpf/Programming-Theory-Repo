using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
[Serializable]
public abstract class FireFodder : MonoBehaviour
{
    // ENCAPSULATION - in order to see properties in Unity Editor to set values for children prefabs,
    // cannot use built-in get/set functionality
    [SerializeField] protected float HeatToIgnite;
    [SerializeField] protected bool HasIgnited;

    // INHERITANCE
    public virtual void Heat(float heatAmount)
    {
        if (!HasIgnited)
        {
            HeatToIgnite -= heatAmount;
            if (HeatToIgnite <= 0)
            {
                // ABSTRACTION
                Ignite();
            }
        }
    }
    // POLYMORPHISM
    protected virtual void Ignite()
    {
        HasIgnited = true;
    }
}
