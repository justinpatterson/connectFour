﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectFour_GameManager : MonoBehaviour {
    public ConnectFour_Board myBoard;
    public ConnectFour_UI myUI;
    public ConnectFour_AudioManager myAudio;
    public enum ConnectFour_phases {start, p1turn, p2turn, end}
    public ConnectFour_phases Phase;


    public void Awake()
    {
        InitializeGame();
        UpdatePlayerNames("Player1", "Player2");
    }

    public void InitializeGame()
    {
        Phase = ConnectFour_phases.start;
        myBoard.Init();
        myUI.TriggerStartScreen(true);
        myUI.TriggerWinPopup_Close();
        myAudio.PlayMusic(Phase);
       
    }
    public void StartGame() {
        myUI.TriggerStartScreen(false);
        myUI.GenerateBoardUI(myBoard.width, myBoard.height);
        myUI.boardText.text = myBoard.UpdateBoardDisplay(myBoard.width, myBoard.height);
        Phase = ConnectFour_phases.p1turn;
        myAudio.PlayMusic(Phase);
    }
 


    public void TriggerPieceDrop(int column) {
            switch (Phase ) {
                case ConnectFour_phases.p1turn:

                    if (myBoard.DropPieceAtColumn(1, column))
                   {
                        bool Win = myBoard.CheckWinState();
                        if (Win)
                        {
                        TriggerWin(1);
                        }
                        else
                        {
                            Phase = ConnectFour_phases.p2turn;
                            myUI.UpdatePlayerTurnIndicator(1);
                        }
                    }
                    myUI.boardText.text = myBoard.UpdateBoardDisplay(myBoard.width, myBoard.height);
                    myUI.UpdateBoardUI(myBoard.boardState);
               

                    break;
                case ConnectFour_phases.p2turn: 
                   
                if (myBoard.DropPieceAtColumn(2, column))
                    {
                        bool Win = myBoard.CheckWinState();
                        if (Win)
                        {
                            TriggerWin(2);
                        }
                        else
                        {
                            Phase = ConnectFour_phases.p1turn;
                            myUI.UpdatePlayerTurnIndicator(0);
                        }   
                       
                    }
                    myUI.boardText.text = myBoard.UpdateBoardDisplay(myBoard.width, myBoard.height);
                    myUI.UpdateBoardUI(myBoard.boardState);

                    break;
                default: break;
            }

            }
    public void TriggerWin(int player) {
        Debug.Log("Win:" + player);
        Phase = ConnectFour_phases.end;
        myUI.TriggerWinPopup_Open(player);
        myAudio.PlayMusic(Phase);
    }


    public void TriggerPlayerNameChange()
    {
        string p1Name = myUI.Start_player1NameField.text;
        string p2Name = myUI.Start_player2NameField.text;
        UpdatePlayerNames(p1Name, p2Name);
    }

    void UpdatePlayerNames(string p1, string p2)
    {
        PlayerPrefs.SetString("Player1", p1);
        PlayerPrefs.SetString("Player2", p2);
        myUI.Start_player1NameField.text = p1;
        myUI.Start_player2NameField.text = p2;
    }
    string GetPlayerName(string PlayerPrefKey)
    {
        return PlayerPrefs.GetString(PlayerPrefKey, "Default_" + PlayerPrefKey);
    }

}
