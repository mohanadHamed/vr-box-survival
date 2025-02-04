using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ButtonInteraction : MonoBehaviour
{

    [SerializeField]
    UI_InteractionController uiInteractionsController;

    public TextMeshProUGUI simpleUIText; 

    public void OnStartButtonClicked()
    {
        GameManager.Instance.StartTrainingGame();
    }


}
