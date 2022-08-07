using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectedTextController : MonoBehaviour
{   
    private TextMeshProUGUI playerText;
    // Start is called before the first frame update
    void Start()
    {
        playerText = GetComponent<TextMeshProUGUI>();
        if (playerText.text == "P1"){
            playerText.color =  PlayerSelectController.P1selected ? Color.green : Color.red;
        }if (playerText.text == "P2"){
            playerText.color =  PlayerSelectController.P2selected ? Color.green : Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerText.text == "P1"){
            playerText.color =  PlayerSelectController.P1selected ? Color.green : Color.red;
        }if (playerText.text == "P2"){
            playerText.color =  PlayerSelectController.P2selected ? Color.green : Color.red;
        }
    }
}
