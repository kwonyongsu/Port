using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    float m_force = 0.0f;
    [SerializeField]
    Vector3 m_offset = Vector3.zero;
    Quaternion m_originRot;

    void Start()
    {
        m_originRot = this.transform.rotation;
    }


    void Update()
    {
       

    }


    IEnumerator ShakeCamera()
    {
        Vector3 t_originEuler = this.transform.eulerAngles;

        while (true)
        {
            float t_rotX = Random.Range(-m_offset.x, m_offset.x);
            float t_rotY = Random.Range(-m_offset.y, m_offset.y);
            float t_rotZ = Random.Range(-m_offset.z, m_offset.z);

            Vector3 t_randaomRot = t_originEuler + new Vector3(t_rotX, t_rotY, t_rotZ);
            Quaternion t_rot = Quaternion.Euler(t_randaomRot);

            while (Quaternion.Angle(transform.rotation, t_rot) > 0.1f)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, t_rot, m_force * Time.deltaTime);
                yield return null;
            
            }
            yield return null;
        }
    }

    IEnumerator Reset()
    {
        while (Quaternion.Angle(transform.rotation, m_originRot) > 0.0f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_originRot, m_force * Time.deltaTime);
            yield return null;
        
        }
    }


    IEnumerator hiteff()
    {
        StartCoroutine(ShakeCamera());
        yield return null;
        StopAllCoroutines();
        StartCoroutine(Reset());
    }

    public void HitEff()
    {
        StartCoroutine(hiteff());
        Debug.Log("흔들림");
    }

}
