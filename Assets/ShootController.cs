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

    [SerializeField]
    float m_shotgunAngle = 15f;

    private int weaponType = 1;

    void Fire()
    {
        IEnumerator shootingMethod;

        if (weaponType == 1)
            shootingMethod = ShotgunFireAsync(m_bulletCountInOneShot);
        else if (weaponType == 2)
            shootingMethod = CircleFireAsync(m_shootCountInOneFire);
        else
            shootingMethod = ShotgunFireAsync(m_bulletCountInOneShot);

        StartCoroutine(shootingMethod);
    }

    IEnumerator ShotgunFireAsync(int shootCount)
    {
        if (shootCount % 2 == 0) shootCount++;

        for (int i = 0; i < shootCount; i++)
        {
            Quaternion q = m_bulletPrefab.transform.rotation;
            GameObject go = Instantiate(m_bulletPrefab, m_muzzlePoint.position, q);

            if (i > 0)
            {
                if (i % 2 == 1)
                    go.transform.Rotate(new Vector3(0f, 0f, m_shotgunAngle * (i / 2 + 1)));
                else
                    go.transform.Rotate(new Vector3(0f, 0f, -1 * m_shotgunAngle * i / 2));
            }
        }
        yield return null;
    }

    IEnumerator CircleFireAsync(int shootCount)
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
        if (Input.GetKeyUp(KeyCode.Alpha1)) weaponType = 1;

        if (Input.GetKeyUp(KeyCode.Alpha2)) weaponType = 2;

        if (Input.GetButtonDown("Fire1")) Fire();
	}
}
