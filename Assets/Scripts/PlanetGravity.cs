using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    [Header("Gameplay values")]
    [SerializeField]
    float m_pullRadius = 2;
    [SerializeField]
    float m_pullForce = 1;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, m_pullRadius))
        {
            // calculate direction from target to me
            Vector3 forceDirection = transform.position - collider.transform.position;

            // apply force on target towards me
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(forceDirection.normalized * m_pullForce * Time.fixedDeltaTime);
            }
        }
    }
}

