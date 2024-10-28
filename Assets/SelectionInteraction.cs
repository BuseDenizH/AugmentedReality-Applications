using UnityEngine;

public class SelectionInteraction : MonoBehaviour
{
    private int selectionStep = 0;

    void OnMouseDown()
    {
        selectionStep++;

        if (selectionStep == 1)
        {
            // First object interaction: change color to red
            GetComponent<Renderer>().material.color = Color.red;
            Debug.Log(gameObject.name + " selected: Step 1");
        }
        else if (selectionStep == 2)
        {
            // Second object interaction: change color to green
            GetComponent<Renderer>().material.color = Color.green;
            Debug.Log(gameObject.name + " selected: Step 2");
        }
        else if (selectionStep == 3)
        {
            // Third object interaction: change color to blue
            GetComponent<Renderer>().material.color = Color.blue;
            Debug.Log(gameObject.name + " selected: Step 3");
            selectionStep = 0; // Reset for looping
        }
    }
}
