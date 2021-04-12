using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrLevelGate : MonoBehaviour
{
    [SerializeField] private float speedRotate;

    void Update()
    {
        //transform.Rotate(0.0f , 0.0f, transform.rotation.z + 1, Space.Self); [OLD CODE]
        transform.Rotate(Vector3.forward * speedRotate * Time.deltaTime);
    }
}
