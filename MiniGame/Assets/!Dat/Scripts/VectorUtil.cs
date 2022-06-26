using UnityEngine;

public static class VectorUtil
{
    public static bool IsGreater(this Vector3 source, Vector3 compare)
    {
        return source.sqrMagnitude > compare.sqrMagnitude;
    }
}
