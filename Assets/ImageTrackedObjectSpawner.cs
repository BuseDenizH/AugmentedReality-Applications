using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ImageTrackedObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The virtual object to display on the image

    private ARTrackedImageManager trackedImageManager;

    void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            // Hide the object when the image is removed or lost
            objectToSpawn.SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.referenceImage.name == "Assets/WhatsApp Image 2024-10-26 at 21.37.45.jpeg") // Replace with the exact name of your image in the Reference Image Library
        {
            // Position and rotate the object to match the detected image
            objectToSpawn.transform.position = trackedImage.transform.position;
            objectToSpawn.transform.rotation = trackedImage.transform.rotation;
            objectToSpawn.SetActive(true); // Show the object when the image is detected
        }
    }
}
