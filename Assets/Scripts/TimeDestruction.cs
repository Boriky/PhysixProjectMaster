using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestruction : MonoBehaviour {

    [Header("Gameplay values")]
    [SerializeField]
    float m_destroyAfter = 15.0f;

    private void Awake()
    {
        StartCoroutine(TimedDestruction());
    }

    public IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(m_destroyAfter);
        Destroy(gameObject);
    }
}
