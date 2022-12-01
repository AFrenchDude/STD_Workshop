using System;
using System.Collections.Generic;
using UnityEngine;

//Original code by CodeMonkey: https://unitycodemonkey.com/video.php?v=7j_BNf9s0jM, Modified by Melinon Remy
public class SplineDone : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform dots = null;
    [SerializeField] private Vector3 normal = new Vector3(0, 0, -1);
    [SerializeField] private List<Anchor> anchorList;

    private float pointAmountInCurve;
    private float pointAmountPerUnitInCurve = 2f;

    private List<Point> pointList;
    private float splineLength;
    public event EventHandler OnDirty;

    [Serializable]
    public class Point
    {
        public float t;
        public Vector3 position;
        public Vector3 forward;
        public Vector3 normal;
    }

    [Serializable]
    public class Anchor
    {
        public Vector3 position;
        public Vector3 handleAPosition;
        public Vector3 handleBPosition;
    }

    private void Awake()
    {
        var enleverbugunity = dots;
        splineLength = GetSplineLength();
        SetupPointList();
    }

    private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, t);
    }

    private Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 abc = QuadraticLerp(a, b, c, t);
        Vector3 bcd = QuadraticLerp(b, c, d, t);

        return Vector3.Lerp(abc, bcd, t);
    }

    public Vector3 GetPositionAt(float t)
    {
        if (t == 1) 
        {
            // Full position, special case
            Anchor anchorA, anchorB;
            anchorA = anchorList[anchorList.Count - 1];
            anchorB = anchorList[0];
            return transform.position + CubicLerp(anchorA.position, anchorA.handleBPosition, anchorB.handleAPosition, anchorB.position, t);
        } 
        else 
        {
            float tFull = t * (anchorList.Count);
            int anchorIndex = Mathf.FloorToInt(tFull);
            float tAnchor = tFull - anchorIndex;

            Anchor anchorA, anchorB;
            if (anchorIndex >= 0 && anchorIndex < anchorList.Count - 1) 
            {
                anchorA = anchorList[anchorIndex];
                anchorB = anchorList[anchorIndex + 1];
            } 
            else 
            {
                anchorA = anchorList[anchorList.Count - 1];
                anchorB = anchorList[0];
            }
            return transform.position + CubicLerp(anchorA.position, anchorA.handleBPosition, anchorB.handleAPosition, anchorB.position, tAnchor);
        }
    }

    public Vector3 GetForwardAt(float t)
    {
        Point pointA = GetPreviousPoint(t);

        int pointBIndex;

        pointBIndex = (pointList.IndexOf(pointA) + 1) % pointList.Count;
        Point pointB = pointList[pointBIndex];

        return Vector3.Lerp(pointA.forward, pointB.forward, (t - pointA.t) / Mathf.Abs(pointA.t - pointB.t));
    }

    public Point GetPreviousPoint(float t)
    {
        int previousIndex = 0;
        for (int i=1; i<pointList.Count; i++)
        {
            Point point = pointList[i];
            if (t < point.t)
            {
                return pointList[previousIndex];
            }
            else
            {
                previousIndex++;
            }
        }
        return pointList[previousIndex];
    }

    public Point GetClosestPoint(float t)
    {
        Point closestPoint = pointList[0];
        foreach (Point point in pointList)
        {
            if (Mathf.Abs(t - point.t) < Mathf.Abs(t - closestPoint.t))
            {
                Vector3 testPos = pointList[0].position;
                closestPoint = point;
            }
        }
        return closestPoint;
    }
    public Point GetClosestPoint(Vector3 callerPosition)
    {
        Point closestPoint = pointList[0];
        foreach (Point point in pointList)
        {
            if ((callerPosition - point.position).sqrMagnitude < (callerPosition - closestPoint.position).sqrMagnitude)
            {
                Vector3 testPos = pointList[0].position;
                closestPoint = point;
            }
        }
        return closestPoint;
    }


    public Vector3 GetPositionAtUnits(float unitDistance, float stepSize = .01f)
    {
        float splineUnitDistance = 0f;

        Vector3 lastPosition = GetPositionAt(0f);

        float incrementAmount = stepSize;
        for (float t = 0; t < 1f; t += incrementAmount)
        {
            splineUnitDistance += Vector3.Distance(lastPosition, GetPositionAt(t));

            lastPosition = GetPositionAt(t);

            if (splineUnitDistance >= unitDistance)
            {
                Vector3 direction = (GetPositionAt(t) - GetPositionAt(t - incrementAmount)).normalized;
                return GetPositionAt(t) + direction * (unitDistance - splineUnitDistance);
            }
        }
        // Default
        Anchor anchorA = anchorList[0];
        Anchor anchorB = anchorList[1];
        return CubicLerp(anchorA.position, anchorA.handleBPosition, anchorB.handleAPosition, anchorB.position, unitDistance / splineLength);
    }

    public Vector3 GetForwardAtUnits(float unitDistance, float stepSize = .01f)
    {
        float splineUnitDistance = 0f;

        Vector3 lastPosition = GetPositionAt(0f);

        float incrementAmount = stepSize;
        float lastDistance = 0f;

        for (float t = 0; t < 1f; t += incrementAmount)
        {
            lastDistance = Vector3.Distance(lastPosition, GetPositionAt(t));
            splineUnitDistance += lastDistance;

            lastPosition = GetPositionAt(t);

            if (splineUnitDistance >= unitDistance)
            {
                float remainingDistance = splineUnitDistance - unitDistance;
                return GetForwardAt(t - ((remainingDistance / lastDistance) * incrementAmount));
            }

        }

        // Default
        Anchor anchorA = anchorList[0];
        Anchor anchorB = anchorList[1];
        return CubicLerp(anchorA.position, anchorA.handleBPosition, anchorB.handleAPosition, anchorB.position, unitDistance / splineLength);
    }

    private void SetupPointList()
    {
        pointList = new List<Point>();
        pointAmountInCurve = pointAmountPerUnitInCurve * splineLength;
        for (float t = 0; t < 1f; t += 1f / pointAmountInCurve)
        {
            pointList.Add(new Point
            {
                t = t,
                position = GetPositionAt(t),
                normal = normal,
            });
        }

        pointList.Add(new Point
        {
            t = 1f,
            position = GetPositionAt(1f),
        });

        UpdateForwardVectors();
    }

    private void UpdatePointList()
    {
        if (pointList == null) return;

        foreach (Point point in pointList)
        {
            point.position = GetPositionAt(point.t);
        }
        
        UpdateForwardVectors();
    }

    private void UpdateForwardVectors()
    {
        // Set forward vectors
        for (int i = 0; i < pointList.Count - 1; i++)
        {
            pointList[i].forward = (pointList[i + 1].position - pointList[i].position).normalized;
        }
        // Set final forward vector
        pointList[pointList.Count - 1].forward = pointList[0].forward;
    }

    public float GetSplineLength(float stepSize = .01f)
    {
        float splineLength = 0f;

        Vector3 lastPosition = GetPositionAt(0f);

        for (float t = 0; t < 1f; t += stepSize)
        {
            splineLength += Vector3.Distance(lastPosition, GetPositionAt(t));

            lastPosition = GetPositionAt(t);
        }

        splineLength += Vector3.Distance(lastPosition, GetPositionAt(1f));

        return splineLength;
    }

    public List<Anchor> GetAnchorList()
    {
        return anchorList;
    }

    public void AddAnchor()
    {
        if (anchorList == null) anchorList = new List<Anchor>();

        Anchor lastAnchor = anchorList[anchorList.Count - 1];
        anchorList.Add(new Anchor
        {
            position = lastAnchor.position + new Vector3(1, 1, 0),
            handleAPosition = lastAnchor.handleAPosition + new Vector3(1, 1, 0),
            handleBPosition = lastAnchor.handleBPosition + new Vector3(1, 1, 0),
        });
    }

    public void RemoveLastAnchor()
    {
        if (anchorList == null) anchorList = new List<Anchor>();

        anchorList.RemoveAt(anchorList.Count - 1);
    }


    public List<Point> GetPointList()
    {
        return pointList;
    }

    public void SetDirty()
    {
        splineLength = GetSplineLength();

        UpdatePointList();

        OnDirty?.Invoke(this, EventArgs.Empty);
    }
}
