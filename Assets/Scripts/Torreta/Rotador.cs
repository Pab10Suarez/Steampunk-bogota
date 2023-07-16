using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotador : MonoBehaviour
{
    private float _rotationSpeedZ = 50f;
    private Transform _cannon;

    void Update()
    {
        _cannon = this.gameObject.transform;

        float additionalRotationZ = _rotationSpeedZ * Time.deltaTime;
        transform.Rotate(0f, 0f, additionalRotationZ);

        float cannonRotationZ = _cannon.eulerAngles.z;

        if (cannonRotationZ > 45f && cannonRotationZ < 180f)
        {
            _rotationSpeedZ = -50f;
        }
        else if (cannonRotationZ < 315f && cannonRotationZ >= 180f)
        {
            _rotationSpeedZ = 50f;
        }
    }
}
