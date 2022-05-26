using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    public bool negative;
    public Vector3 rotationVector;

    // Update is called once per frame
    void Update()
    {
        var angle = (90 * Time.deltaTime) / 10;
        transform.Rotate(rotationVector, negative ? -angle : angle);
    }
}
