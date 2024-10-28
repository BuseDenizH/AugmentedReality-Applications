using UnityEngine;

public class MultiStepInteraction : MonoBehaviour
{
    private int interactionStep = 0;

    void Start()
{
    Debug.Log("MultiStepInteraction script initialized on " + gameObject.name);
}

    void OnMouseDown()
    {
        Debug.Log("Object clicked");
        interactionStep++;
        
        if (interactionStep == 1)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (interactionStep == 2)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (interactionStep == 3)
        {
            GetComponent<Renderer>().material.color = Color.blue;
            //interactionStep = 0; // Reset to allow looping
        }
    }
}
