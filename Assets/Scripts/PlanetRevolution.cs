using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRevolution : MonoBehaviour {

    public GameObject m_sun = null;
    public float m_revolutionVelocity = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // Rotates the transform about axis passing through point in world coordinates by angle degrees.
        transform.RotateAround(m_sun.transform.position, Vector3.up, m_revolutionVelocity * Time.deltaTime);
	}
}
