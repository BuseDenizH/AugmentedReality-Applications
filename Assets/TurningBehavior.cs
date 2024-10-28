using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    public float rotationSpeed = 50.0f; // Rotation speed can be adjusted in the inspector

    // Update is called once per frame
    void Update()
    {
        // Rotate the cube along the Y-axis (or any axis you want)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
