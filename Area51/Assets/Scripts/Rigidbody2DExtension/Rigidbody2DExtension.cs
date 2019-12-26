using UnityEngine;
using UnityEditor;

public static class Rigidbody2DExtension
{
    public static void AddExplosionForce(Rigidbody2D body, Vector3 explosionPosition, float explosionForce, float explosionBouncePower)
    {
        Vector3 bodyCenterPos = body.transform.position;

        float widthAbs = Mathf.Abs(bodyCenterPos.x - explosionPosition.x);
        float heightAbs = Mathf.Abs(bodyCenterPos.y - explosionPosition.y);

        var directionWidth = Mathf.Sign(bodyCenterPos.x - explosionPosition.x);
        var directionHeight = Mathf.Sign(bodyCenterPos.y - explosionPosition.y);
        var hypotenuse = Mathf.Sqrt(Mathf.Pow(widthAbs, 2f) + Mathf.Pow(heightAbs, 2f));

        float arcSinValRad = Mathf.Asin(heightAbs / hypotenuse);

        float yAxisForce = directionHeight * Mathf.Sin(arcSinValRad) * explosionForce + explosionBouncePower;
        float xAxisForce = directionWidth * Mathf.Cos(arcSinValRad) * explosionForce;

        Vector2 bodyForce = new Vector2(xAxisForce, yAxisForce);

        body.velocity = bodyForce;
    }
}