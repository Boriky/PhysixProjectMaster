using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    MenuManager m_menuManager = null;

    [Header("Explosion Parameters")]
    [SerializeField]
    float m_force = 40.0f;
    [SerializeField]
    float m_radius = 100.0f;
    [SerializeField]
    float m_upwardsModifier = 2.0f;
    [SerializeField]
    float m_debrisExplosionSize = 2.0f;
    [SerializeField]
    float m_planetExplosionSize = 2.0f;
    [SerializeField]
    float m_playerExplosionSize = 2.0f;

    [Header("Explosion Effects")]
    [SerializeField]
    GameObject m_explosionPrefab = null;
    [SerializeField]
    GameObject m_debrisPrefab = null;

    public void Explode(GameObject obj, float size)
    {
        GameObject explosion = Instantiate(m_explosionPrefab, GetComponent<Transform>().position, GetComponent<Transform>().rotation);
        explosion.GetComponent<Transform>().localScale = new Vector3(size, size, size);
        ParticleSystem explosionParticles = explosion.GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule explosionEmitter = explosionParticles.emission;
        explosionEmitter.enabled = true;
        AudioSource explosionAudio = explosion.GetComponent<AudioSource>();
        explosionAudio.Play();

        Vector3 position = GetComponent<Transform>().position;
        Quaternion rotation = GetComponent<Transform>().rotation;

        Destroy(obj);

        GameObject debris = Instantiate(m_debrisPrefab, position, rotation);
        Vector3 explosionPos = position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, m_radius);

        foreach (Collider hit in colliders)
        {
            // Add an explosion force to in range rigidbodies
            Rigidbody hittedRb = hit.GetComponent<Rigidbody>();
            if (hittedRb != null)
            {
                hittedRb.AddExplosionForce(m_force, explosionPos, m_radius, m_upwardsModifier, ForceMode.Impulse);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Camera.main.transform.parent = null;
            Explode(col.gameObject, m_playerExplosionSize);
            m_menuManager.RestartGame(false);
        }

        if (col.gameObject.tag == "Planet")
        {
            Explode(col.gameObject, m_planetExplosionSize);
        }

        if (col.gameObject.tag == "Sun")
        {
            Explode(gameObject, m_planetExplosionSize);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Debris")
        {
            Destroy(col.gameObject);
        }
    }
}