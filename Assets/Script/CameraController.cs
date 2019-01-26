using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera m_MainCamera;
    public CameraRotation m_Gyro;

    public GameObject m_target;

    public float m_RotateSpeed = 0.1f;

    public bool isStart = false;

    public void Update()
    {
        if(isStart)
        {
            StartRotation(10f, m_target);
            isStart = false;
        }
    }

    public void StartRotation(float time, GameObject target)
    {
        StartCoroutine(CameraRotating(time, target));
    }

    IEnumerator CameraRotating(float time, GameObject target)
    {
        if (m_Gyro != null)
            m_Gyro.ActiveGyroscope(false);

        float _endTime = time, _nowTime = 0f;

        Quaternion targetRotation 
            = Quaternion.LookRotation(target.transform.position - m_MainCamera.transform.position);

        while (_nowTime < _endTime)
        {
            targetRotation
               = Quaternion.LookRotation(target.transform.position - m_MainCamera.transform.position);

            m_MainCamera.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (Time.fixedDeltaTime + _nowTime/_endTime) * m_RotateSpeed);
            _nowTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        if (m_Gyro != null)
            m_Gyro.ActiveGyroscope(true);

        yield return null;
    }
}
