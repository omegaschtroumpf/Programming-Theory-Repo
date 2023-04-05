using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
[Serializable]
public class BurnFodder : FireFodder
{
    [SerializeField] ParticleSystem flameParticle;
    // ENCAPSULATION
    [SerializeField] float m_TimeToBurn;
    protected Vector3 m_BurnRate;
    // ENCAPSULATION
    public float TimeLeftToBurn { get; protected set; }
    public float BurnRate { get; protected set; }
    [SerializeField] protected Color heatColor;
    [SerializeField] protected Color burnColor;
    private Color currentColor;
    private float[] colorIncrements = new float[4];
    private Renderer rendererObject;

    public void Start()
    {
        rendererObject = GetComponent<Renderer>();
        currentColor = rendererObject.material.color;
        // ABSTRACTION
        SetColorIncrements(currentColor, heatColor, HeatToIgnite);
    }
    public void Update()
    {
        // for demo
        Heat(.5f * Time.deltaTime);
    }

    // POLYMORPHISM
    public override void Heat(float heatAmount)
    {
        if (HasIgnited)
        {
            // ABSTRACTION
            Burn();
        }
        else
        {
            // INHERITANCE
            base.Heat(heatAmount);
            // ABSTRACTION
            UpdateColor();
        }
    }

    // POLYMORPHISM
    // on ignition, ExplosiveFodder will explode
    protected override void Ignite()
    {
        HasIgnited = true;
        flameParticle.Play();
        m_BurnRate = transform.localScale / m_TimeToBurn;
        // ABSTRACTION
        SetColorIncrements(currentColor, burnColor, m_TimeToBurn);
    }
    // ABSTRACTION
    protected void Burn()
    {
        transform.localScale -= m_BurnRate * Time.deltaTime;
        // ABSTRACTION
        UpdateColor();
        if (transform.localScale.x < 0) // destroy before scale goes inverse
        {
            Destroy(gameObject);
        }  
    }
    // ABSTRACTION
    protected void SetColorIncrements(Color startColor, Color endColor, float scale)
    {
        for (int i = 0; i < colorIncrements.Length; i++)
        {
            colorIncrements[i] = (endColor[i] - startColor[i]) / scale;
        }
    }
    // ABSTRACTION
    protected void UpdateColor() 
    {
        // colorIncrements are scaled already for the amount of heat, or burn time
        // just update by the increments scaled by time between frames
        // we don't have to worry about going past our targets because ignition or destruction will occur
        for (int i = 0; i < 4; i++)
        {
            currentColor[i] += colorIncrements[i] * Time.deltaTime;
        }
        // update the material color
        rendererObject.material.SetColor("_Color", currentColor);
    }
}
