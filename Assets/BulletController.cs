using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField]
    float m_speed = 10f;

    [SerializeField]
    float m_distance = 10f;

    Vector3 m_startPosition;

    private void Start()
    {
        m_startPosition = transform.position;
    }

    void FixedUpdate () {
        if (Vector3.Distance(transform.position, m_startPosition) > m_distance) Destroy(gameObject);
        transform.position = transform.position + transform.up.normalized * m_speed;
	}
}
