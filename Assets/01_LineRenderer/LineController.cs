using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] float lineWidth;

    GameObject lineObj;
    LineRenderer lineRenderer;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _initLine();
        }
        if (Input.GetMouseButton(0))
        {
            _drawLine();
        }
    }

    private void _initLine()
    {
        // line用オブジェクト作成
        lineObj = new GameObject();
        lineObj.name = "Line";
        lineObj.AddComponent<LineRenderer>();
        lineObj.AddComponent<EdgeCollider2D>();
        lineObj.transform.SetParent(transform);
        // lineRendererの作成
        lineRenderer = lineObj.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

    private void _drawLine()
    {
        // 線を描画
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);
        // 当たり判定を追加
        _setEdgeCollider();
    }

    private void _setEdgeCollider()
    {
        List<Vector2> edges = new List<Vector2>();
        for(int point = 0; point<lineRenderer.positionCount; point++)
        {
            Vector3 lineRendererPoint = lineRenderer.GetPosition(point);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        }
        EdgeCollider2D edgeCollider = lineObj.GetComponent<EdgeCollider2D>();
        edgeCollider.SetPoints(edges);
    }
}
