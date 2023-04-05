using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
[Serializable]
public class ExplosiveFodder : FireFodder
{
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] protected float ExplosiveForce;
    [SerializeField] protected float ExplosiveRadius;

    private void Update()
    {
        Heat(.5f * Time.deltaTime);
    }

    // POLYMORPHISM
    // on ignition, ExplosiveFodder will explode
    protected override void Ignite()
    {
        // ABSTRACTION
        Explode();
        //GameObject.Find("Cube").GetComponent<Rigidbody>().AddExplosionForce(ExplosiveForce, transform.position, ExplosiveRadius, 0f, ForceMode.Impulse);
        Destroy(gameObject);
        
    }
    // ABSTRACTION
    public void Explode()
    {
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, ExplosiveRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(ExplosiveForce, explosionPosition, ExplosiveRadius, 3.0F, ForceMode.Impulse);
        }
    }
}
