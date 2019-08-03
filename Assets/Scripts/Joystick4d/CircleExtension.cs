using UnityEngine;

public static class CircleExtension {
    public static void DrawCircle(this GameObject gameObject, float radius, float lineWidth) {
        var segments = 360;
        var line = gameObject.GetComponent<LineRenderer>();
        if (line == null) return;
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;
        var pointCount = segments + 1;
        var points = new Vector3[pointCount];
        for (int pointIndex = 0; pointIndex < pointCount; pointIndex++) {
            var rad = Mathf.Deg2Rad * (pointIndex * 360f / segments);
            points[pointIndex] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
        }
        line.SetPositions(points);
    }
}