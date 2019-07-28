using UnityEngine;

public static class MathFunctions
{
    public static Vector2 RotateVector(float angle, Vector2 v)
    {
        float cs = Mathf.Cos(angle);
        float sn = Mathf.Sin(angle);
        float rx = v.x * cs - v.y * sn;
        float ry = v.x * sn + v.y * cs;
        v = new Vector2(rx, ry);
        return v;
    }
}
