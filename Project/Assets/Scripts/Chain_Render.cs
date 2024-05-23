using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain_Render : MonoBehaviour
{
    LineRenderer line;
    public Transform entrypoint;
    public Transform exitpoint;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        line.SetPosition(0, entrypoint.position);
        line.SetPosition(1, exitpoint.position);
    }
}
