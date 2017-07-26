using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float m_pullRadius = 2;
    public float m_pullForce = 1;
    public ParticleSystem m_explosion = null;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, m_pullRadius))
        {
            // calculate direction from target to me
            Vector3 forceDirection = transform.position - collider.transform.position;

            // apply force on target towards me
            collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * m_pullForce * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Instantiate(m_explosion, col.transform.position, col.transform.rotation);
        ParticleSystem.EmissionModule emission = m_explosion.emission;
        emission.enabled = true;

        if (col.gameObject.tag == "Player")
        {
            Camera.main.transform.parent = null;
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Planet")
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);

            if (gameObject.tag == "Moon")
            {
                Destroy(gameObject);
            }
        }
    }
}