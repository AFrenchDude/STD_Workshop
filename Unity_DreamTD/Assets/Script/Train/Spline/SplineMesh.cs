using System;
using System.Collections.Generic;
using UnityEngine;

//Original code by CodeMonkey: https://unitycodemonkey.com/video.php?v=7j_BNf9s0jM, Modified by Melinon Remy
public class SplineMesh : MonoBehaviour
{
    [SerializeField] private SplineDone spline;
    [Tooltip("Rails width")]
    [SerializeField] private float meshWidth = 5;
    private Mesh mesh;
    private MeshFilter meshFilter;

    
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Start()
    {
        transform.position = spline.transform.position;
        UpdateMesh();
        spline.OnDirty += Spline_OnDirty;
    }

    private void Spline_OnDirty(object sender, EventArgs e)
    {
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        if (mesh != null)
        {
            mesh.Clear();
            Destroy(mesh);
            mesh = null;
        }
        List<SplineDone.Point> pointList = spline.GetPointList();
        if (pointList.Count > 2)
        {
            SplineDone.Point point = pointList[0];
            SplineDone.Point secondPoint = pointList[1];
            mesh = MeshUtils.CreateLineMesh(point.position - transform.position, secondPoint.position - transform.position, point.normal, meshWidth);

            for (int i = 2; i < pointList.Count; i++) {
                SplineDone.Point thisPoint = pointList[i];
                MeshUtils.AddLinePoint(mesh, thisPoint.position - transform.position, thisPoint.forward, point.normal, meshWidth);
            }
            meshFilter.mesh = mesh;
        }
    }
}
