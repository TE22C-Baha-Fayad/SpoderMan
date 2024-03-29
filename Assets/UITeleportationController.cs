using TMPro;
using UnityEngine;

public class UITeleportationController : MonoBehaviour
{
    private TextMeshProUGUI teleportationLabelUi;
    string labelStartValue;
    void Start()
    {
        PlayerController.onTeleport += Teleported;
        teleportationLabelUi = GetComponent<TextMeshProUGUI>();
        labelStartValue = teleportationLabelUi.text;
    }

    void Teleported(int teleportationsAvailable)
    {
        teleportationLabelUi.text = labelStartValue + teleportationsAvailable.ToString();
    }

}
