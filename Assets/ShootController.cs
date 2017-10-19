using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField]
    GameObject m_bulletPrefab;

    [SerializeField]
    Transform m_muzzlePoint;

    [SerializeField]
    int m_bulletCountInOneShot = 10;

    [SerializeField]
    float m_shootRadius = 1.0f;

    [SerializeField]
    int m_shootCountInOneFire = 5;

    [SerializeField]
    float m_shootingInterval = 0.1f;

    void Fire()
    {
        StartCoroutine(FireAsync(m_shootCountInOneFire));
    }

    IEnumerator FireAsync(int shootCount)
    {
        for (int j = 0; j < shootCount; j++)
        {
            for (int i = 0; i < m_bulletCountInOneShot; i++)
            {
                Vector3 pos = new Vector3(m_shootRadius * Mathf.Sin(Mathf.Deg2Rad * i * 360.0f / (float)m_bulletCountInOneShot),
                    m_shootRadius * Mathf.Cos(Mathf.Deg2Rad * i * 360.0f / (float)m_bulletCountInOneShot),
                    0);
                Instantiate(m_bulletPrefab, pos, m_bulletPrefab.transform.rotation);
            }
            yield return new WaitForSeconds(m_shootingInterval);
        }
    }

	void Update () {
		if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1.");
            Fire();
        }
	}
}
