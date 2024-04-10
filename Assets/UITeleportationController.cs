using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UITeleportationController : MonoBehaviour
{
    //the text label ui for teleportations left
    private TextMeshProUGUI teleportationLabelUi;
    string labelStartValue; 
    void Awake()
    {
        //subscribe to the event to get reports on how many teleps are left.
        PlayerController.OnTeleport += Teleported;
        //assigning the values
        teleportationLabelUi = GetComponent<TextMeshProUGUI>(); 
        labelStartValue = teleportationLabelUi.text;
    }
    //invoked by the player when teleporting mode is active
    void Teleported(int teleportationsAvailable,bool teleportActive)
    {
        //if teleps count is less the 0
        if (teleportationsAvailable < 0)
            teleportationsAvailable++; //increase the count of teleportation, a way to handle the count of teleportations to match the actual count.


        teleportationLabelUi.text = labelStartValue + teleportationsAvailable.ToString(); // sets the text to the value
    }

}
