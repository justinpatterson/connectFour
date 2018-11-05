using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectFour_GameManager : MonoBehaviour {
    public ConnectFour_Board myBoard;
    public ConnectFour_UI myUI;
    public enum ConnectFour_phases {start, p1turn, p2turn, end}
    public ConnectFour_phases Phase;


    public void Awake()
    {

       InitializeGame();
    }

    public void InitializeGame()
    {
        Phase = ConnectFour_phases.start;
        myBoard.Init();
        myUI.TriggerStartScreen(true);
        myUI.TriggerWinPopup_Close();
       
    }
    public void StartGame() {
        myUI.TriggerStartScreen(false);
        myUI.GenerateBoardUI(myBoard.width, myBoard.height);
        myUI.boardText.text = myBoard.UpdateBoardDisplay(myBoard.width, myBoard.height);
        Phase = ConnectFour_phases.p1turn;
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
    }
       

    }
