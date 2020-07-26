using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CameraRecticle : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    [SerializeField]
    internal Camera c;
    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
        //ensure that the reticle is centered
        transform.position = c.transform.position + new Vector3(0,0,2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //This method is used to create the custom reticle shaped mesh.
    void CreateShape()
    {
        vertices = new Vector3[]
        {
            new Vector3(0,0,0),

            new Vector3(-.5f,0,1),
            new Vector3(.5f,0,1),

            new Vector3(-.5f,0,-1),
            new Vector3(.5f,0,-1),

            new Vector3(-1,0,.5f),
            new Vector3(-1,0,-.5f),

            new Vector3(1,0,.5f),
            new Vector3(1,0,-.5f),

        };

        triangles = new int[]
        {
            0,1,2,
            0,3,4,
            0,5,6,
            0,7,8
        };
    }

    //This method renders the mesh.
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

}