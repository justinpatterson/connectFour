using System.Collections;
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
    public GameObject Start_Panel;
    public InputField Start_player1NameField, Start_player2NameField;
    public Slider bestOfSlider;
    public Text bestOfLabel;
    public ConnectFour_GameManager myGameManager;

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
                if (targetTile.GetComponent<ConnectFour_GridElement>())
                    targetTile.GetComponent<ConnectFour_GridElement>().SetPlayerOwner(targetPlayer);
                else
                {
                    Color targetColor = Color.white;
                    if (targetPlayer == 1)
                    {
                        targetColor = Color.red;

                    }
                    if (targetPlayer == 2)
                    {
                        targetColor = Color.yellow;
                    }
                    targetTile.GetComponent<Image>().color = targetColor;
                }
            }
        }
    }
    public void UpdatePlayerTurnIndicator(int Currentplayer) {
        PlayerTurnIndicator.sprite = PlayerTurnSprites[Currentplayer];


    }

    public void TriggerWinPopup_Open(int PlayerNumber) {
        string s = PlayerNumber == 1 ? PlayerPrefs.GetString("Player1") : PlayerPrefs.GetString("Player2");

        int p1Score = PlayerPrefs.GetInt("Player1Score");
        int p2Score = PlayerPrefs.GetInt("Player2Score");
        string appendBestOf = "";
        if (PlayerNumber == 1) appendBestOf = "\n" + p1Score.ToString() + " OUT OF " + PlayerPrefs.GetInt("BestOf");
        else appendBestOf = "\n" + p2Score.ToString() + " OUT OF " + PlayerPrefs.GetInt("BestOf");

        End_Panel_Text.text = s + " Wins!" + appendBestOf; 
        End_Panel.SetActive(true);


    }
    public void TriggerWinPopup_Close() 
    {
        End_Panel.SetActive(false);
    }
    public void TriggerStartScreen(bool active) 
    {
        Start_Panel.SetActive(active);
    }

    public void TriggerNewBestOfValue()
    {
        int value = (int) bestOfSlider.value;
        int valueMod2 = value % 2;
        if (valueMod2 == 0) value = value + 1;
        bestOfSlider.value = (float) value;
        bestOfLabel.text = "BEST OF: " + value.ToString();
        myGameManager.ReportNewBestOfValue(value);

    }

    public void TriggerNextRound() 
    {
        
    }
}

