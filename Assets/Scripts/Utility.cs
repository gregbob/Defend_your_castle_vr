using UnityEngine;
using System.Collections;

public class Utility  {

    public static  bool Approximately(Vector3 a, Vector3 b, float threshold)
    {
        if (FastApproximately(a.x, b.x, threshold) &&
            FastApproximately(a.y, b.y, threshold) &&
            FastApproximately(a.z, b.z, threshold))
        {
            return true;
        }
        return false;
    }

    public static bool FastApproximately(float a, float b, float threshold)
    {
        return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= threshold;
    }


    public static Vector4 GetRandomColor()
    {
        var x = Random.Range(0, 2);
        var y = Random.Range(0, 2);
        var z = Random.Range(0, 2);
        var w = Random.Range(0, 2);

        return new Vector4(x, y, z, w);
    }
}
