using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject Target;
    public Vector3 Offset;
    public float LagConstant;

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.position - (Target.transform.position + Offset)) * Time.deltaTime * LagConstant;
    }
}
