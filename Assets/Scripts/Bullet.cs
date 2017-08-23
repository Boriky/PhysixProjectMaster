using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);

        if (col.gameObject.tag == "Moon")
        {
            Explosion explosion = col.gameObject.GetComponent<Explosion>();
            explosion.Explode(col.gameObject, 15.0f);
        }
    }
}
