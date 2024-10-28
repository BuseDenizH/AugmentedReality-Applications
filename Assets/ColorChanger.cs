using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public GameObject targetObject; // Object to change color
    private int colorIndex = 0;

    public void ChangeColor()
    {
        if (targetObject == null) return;

        colorIndex = (colorIndex + 1) % 3;

        // Change colors based on index
        switch (colorIndex)
        {
            case 0:
                targetObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 1:
                targetObject.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 2:
                targetObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
        }
    }
}
