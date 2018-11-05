using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectFour_GridElement : MonoBehaviour {
    public Image frameIMG;
    public Image pieceIMG;

    private void Awake()
    {
        SetPlayerOwner(-1);
    }

    public void SetPlayerOwner(int player)
    {
        Color targetColor = Color.white;
        if (player == 1)
        {
            targetColor = Color.red;
        }
        if (player == 2)
        {
            targetColor = Color.yellow;
        }
        pieceIMG.color = targetColor;
    }
}
