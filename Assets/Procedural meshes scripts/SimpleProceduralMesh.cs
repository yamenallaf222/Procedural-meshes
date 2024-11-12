using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SimpleProceduralMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        var mesh = new Mesh{ name = "Procedural Mesh"};

        mesh.vertices = new Vector3[]
        {
            Vector3.zero,
            Vector3.right,
            Vector3.up
        };

        mesh.triangles = new int[]
        {
            0, 2, 1
        };

        GetComponent<MeshFilter>().mesh = mesh;
    }
}
