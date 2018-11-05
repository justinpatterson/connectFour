﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectFour_UI : MonoBehaviour {
    public Text boardText;
    public GameObject TILE_PREFAB;
    public Dictionary<Vector2, GameObject> boardGrid = new Dictionary<Vector2, GameObject>();
    public GameObject boardGridContainer;
    public Image PlayerTurnIndicator;
    public Sprite[] PlayerTurnSprites;
    public GameObject End_Panel;
    public Text End_Panel_Text;


    public void GenerateBoardUI(int w, int h) {
        
            boardGrid.Clear();
        for (int i = boardGridContainer.transform.childCount - 1; i >= 0; i--) {
            Destroy(boardGridContainer.transform.GetChild(i).gameObject);
        }

        for (int widthValue = 0; widthValue < w; widthValue++)
        {
            for (int heightValue = 0; heightValue < h; heightValue++)
            {
                GameObject tileInstance = Instantiate(TILE_PREFAB) as GameObject;
                tileInstance.transform.SetParent(boardGridContainer.transform);
                boardGrid.Add(new Vector2(widthValue, heightValue), tileInstance);
            }
        }
    }
    public void UpdateBoardUI(Dictionary<Vector2, int> boardStateReference) {
        foreach (Vector2 v in boardStateReference.Keys) {
            if (boardGrid.ContainsKey(v)) {
                GameObject targetTile = boardGrid[v];
                int targetPlayer = boardStateReference[v];
                Color targetColor = Color.grey;
                if (targetPlayer == 1) { targetColor = Color.red; 
                    
                }
                if (targetPlayer == 2)
                {
                    targetColor = Color.yellow;

                }
                targetTile.GetComponent<Image>().color = targetColor;
            }

        }

    }
    public void UpdatePlayerTurnIndicator(int Currentplayer) {
        PlayerTurnIndicator.sprite = PlayerTurnSprites[Currentplayer];


    }

    public void TriggerWinPopup_Open(int PlayerNumber) {
        string s = PlayerNumber == 1 ? "Red" : "Yellow";
        End_Panel_Text.text = s + " Wins!"; 
        End_Panel.SetActive(true);


    }
    public void TriggerWinPopup_Close() {
        End_Panel.SetActive(false);

    }

}

