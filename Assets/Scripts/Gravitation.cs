using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;
public class Gravitation : MonoBehaviour
{
    public static List<Gravitation> other0bj;
    public Rigidbody _rb;
    const float G = 0.00667f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (other0bj == null)
        {
            other0bj = new List<Gravitation>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Gravitation obj in other0bj)
        {
            if (obj != this)
            {
                Attract(obj);
            }

        }
    }

    void Attract(Gravitation other)
    {
        Rigidbody otherRB = other._rb;
        Vector3 direction = _rb.position - otherRB.position;

        float distance = direction.magnitude;
        if (distance == 0f) return;

        float forceMagenitude = G * (_rb.mass  * otherRB.mass) / Mathf.Pow(distance, 2);
        Vector3 gravityForce = forceMagenitude * direction.normalized;
        otherRB.AddForce(gravityForce);
    }
}
