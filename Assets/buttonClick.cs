using UnityEngine;
using UnityEngine.UI;

public class ChangeScenario : MonoBehaviour
{
    public GameObject scenario1;
    public GameObject scenario2;
    public Button changeButton;

    void Start()
    {
        changeButton.onClick.AddListener(SwitchScenario);
    }

    void SwitchScenario()
    {
        scenario1.SetActive(false);
        scenario2.SetActive(true);
    }
}
