using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    private GameObject selectedObject;

    // Method for selecting Object 1
    public void SelectObject1()
    {
        SelectObject(object1);
    }

    // Method for selecting Object 2
    public void SelectObject2()
    {
        SelectObject(object2);
    }

    // Method for selecting Object 3
    public void SelectObject3()
    {
        SelectObject(object3);
    }

    // Method to handle object selection and highlighting
    private void SelectObject(GameObject newSelectedObject)
    {
        if (selectedObject != null)
        {
            // Reset the previous selected object to white
            selectedObject.GetComponent<Renderer>().material.color = Color.white;
        }

        // Update the selected object
        selectedObject = newSelectedObject;
        
        // Highlight the new selected object in yellow
        selectedObject.GetComponent<Renderer>().material.color = Color.yellow;
    }
}
