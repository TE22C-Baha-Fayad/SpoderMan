using TMPro;
using UnityEngine;

public class UITeleportationController : MonoBehaviour
{
    private TextMeshProUGUI teleportationLabelUi;
    string labelStartValue;
    void Awake()
    {
        PlayerController.OnTeleport += Teleported;
        teleportationLabelUi = GetComponent<TextMeshProUGUI>();
        labelStartValue = teleportationLabelUi.text;
    }

    void Teleported(int teleportationsAvailable)
    {
        if (teleportationsAvailable < 0)
            teleportationsAvailable++;
        teleportationLabelUi.text = labelStartValue + teleportationsAvailable.ToString();
    }

}
