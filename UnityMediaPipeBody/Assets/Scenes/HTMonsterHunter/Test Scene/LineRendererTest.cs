using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTest : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject landmark1;
    public GameObject landmark2;
    public GameObject landmark3;
    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        setUp();
    }

    private void setUp()
    {
        line.positionCount = 4;
        line.SetPosition(0, Position(landmark1));
        line.SetPosition(1, Position(landmark2));
        line.SetPosition(2, Position(landmark3));
        line.SetPosition(3, Position(landmark1));
    }

    private Vector3 Position(GameObject obj)
    {
        return obj.transform.position;
    }
}
