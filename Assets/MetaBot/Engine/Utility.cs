using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility{

    static public float getAngle(GameObject center, GameObject obj)
    {
        Vector3 A = new Vector3(1, 0, 0);
        Vector3 b = (obj.transform.position - center.transform.position);
        Vector3 B = (Vector3.ProjectOnPlane(b, new Vector3(0, 1, 0))).normalized;
        float angle = Vector3.Angle(A, B);
        if (center.transform.position.z > obj.transform.position.z)
        {
            return angle;
        }
        return 360 - angle;
    }

    static public float getAngle(Vector3 center, Vector3 obj)
    {
        Vector3 A = new Vector3(1, 0, 0);
        Vector3 b = (obj - center);
        Vector3 B = (Vector3.ProjectOnPlane(b, new Vector3(0, 1, 0))).normalized;
        float angle = Vector3.Angle(A, B);
        if (center.z > obj.z)
        {
            return angle;
        }
        return 360 - angle;
    }

    static public Vector3 vectorFromAngle(float angle)
    {
        float A = (360 - angle) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(A), 0, Mathf.Sin(A)).normalized;
    }
}
