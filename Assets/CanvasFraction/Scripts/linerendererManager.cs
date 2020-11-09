using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class linerendererManager : MonoBehaviour
{
    // Start is called before the first frame update

    LineRenderer lineRenderer;
    public bool dragging;
    public Vector3 start;
    public Vector3 end;
    public Camera cam;

    public Material lineMaterial;
    private void OnEnable()
    {
        Camera.onPostRender += PostRenderDrawLine;
    }

    private void OnDisable()
    {
        Camera.onPostRender -= PostRenderDrawLine;
    }
    void Start()
    {
        

    }
    public void drawline(Vector3 _start,Vector3 _end)
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(0.001f, 0.001f);
        lineRenderer.SetVertexCount(2);
        //dragging = true;
        start = _start;
        end = _end;
        cam = Camera.main;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }


    private void PostRenderDrawLine(Camera cam)
    {
        if (dragging && lineMaterial)
        {
            //Debug.Log("PostRenderDrawLine");
            GL.PushMatrix();
            lineMaterial.SetPass(0);
            GL.LoadOrtho();
            GL.Begin(GL.LINES);
            GL.Color(Color.black);
            GL.Vertex(start);
            GL.Vertex(end);
            GL.End();
            GL.PopMatrix();
        }
    }
}
