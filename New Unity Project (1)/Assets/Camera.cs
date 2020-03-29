using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform PlayerTransform;
    internal Rect rect;
    Vector3 range;

    void Awake()
    {
        range = transform.position - PlayerTransform.position;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(range.x + PlayerTransform.position.x, transform.position.y, range.z + PlayerTransform.position.z);
    }
}
