using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectFour_Board : MonoBehaviour {


    public int height = 5;
    public int width = 6;
    public Vector2[] directions;

//    public char[3] colorValues = new char[] { '-', 'r', 'y' };

    public Dictionary<Vector2, int> boardState = new Dictionary<Vector2, int>();
    Vector2 _lastPosition;
    int _lastColor;



    public void Init() 
    {
        GenerateBoard(width, height);
        Debug.Log(UpdateBoardDisplay(width, height));

    }

    public void GenerateBoard(int w, int h)
    {
        boardState.Clear();

        for (int widthValue = 0; widthValue < w; widthValue ++ )
        {
            for (int heightValue = 0; heightValue < h; heightValue++)
            {
                boardState.Add(new Vector2(widthValue, heightValue), -1);
            }
        }
    }

    public string UpdateBoardDisplay(int w, int h)
    {
        
        string grid = "";  
        for (int heightValue = h-1; heightValue >= 0; heightValue--)
        {
            for (int widthValue = 0; widthValue < w; widthValue++)
            {
                Vector2 targetPosition = new Vector2(widthValue, heightValue);
                if (boardState.ContainsKey(targetPosition))
                {
                    int targetPlayer = boardState[targetPosition];
                    string color = "808080ff";
                    if (targetPlayer == 1) {
                        color = "ff0000ff";
                    }
                    else if (targetPlayer ==2) {
                        color = "ffff00ff";

                    }
                    grid += "<color=#" + color + ">O</color>";
                   // grid += boardState[targetPosition].ToString() + ",";
                
                
                }
                    
                else
                    Debug.Log("Could not find position: " + targetPosition);

            }
            grid += "\n";
        }
        return grid;
    }
    public bool DropPieceAtColumn(int playerIndex, int columnIndex)
    {
        for (int heightValue = 0; heightValue < height; heightValue++)
        {
            Vector2 targetPosition = new Vector2(columnIndex, heightValue);
            if (boardState.ContainsKey(targetPosition))
            {
                if (boardState[targetPosition] == -1)
                {
                    _lastColor = playerIndex;
                    _lastPosition = targetPosition;
                    Debug.Log(_lastPosition);
                    boardState[targetPosition] = playerIndex;
                    return true;
                }
            }
        }
        _lastColor = -1;
        _lastPosition = Vector2.one * -1;

        return false;
    }

    public void ExplodeBoard()
    {
        
    }
    public bool CheckWinState_full() {
        bool win = false;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 checkposition = new Vector2(x, y);
                if (boardState.ContainsKey(checkposition)) {
                    if (boardState[checkposition] == _lastColor) {
                        if (win == false) win = CheckWinState(checkposition, _lastColor);
                    }
                }
            }

        }
        return win;
    }

    public bool CheckWinState() {
        Debug.Log("hi");
        if (_lastColor == -1) {
            

            return false;
        }
        return CheckWinState_full();//CheckWinState(_lastPosition, _lastColor);

    }

    bool CheckWinState(Vector2 inputPosition, int inputColor) {
        Debug.Log(inputPosition);
        int maxWinCount = 0;
        foreach (Vector2 direction in directions) {
            int winCount = 1;
            for (int i = 1; i < 4; i++) {
                string output = "Checking Space:" + direction + "...";
                Vector2 targetPosition = new Vector2();
                targetPosition.x = inputPosition.x + direction.x * i; 
                targetPosition.y = inputPosition.y + direction.y * i;

                if (targetPosition.x < 0 || targetPosition.x >= width || targetPosition.y < 0 || targetPosition.y >= height)
                {
                    output += "INVALID";

                }
                else
                {
                    output += "VALID:" + inputColor + " vs " + boardState[targetPosition];
                   
                    if (boardState[targetPosition] == inputColor)
                    {
                        Debug.Log(output);
                        winCount++;
                        if (winCount > maxWinCount) {
                            maxWinCount = winCount;

                        }
           

                    }
                  
                }
               
            }
            Debug.Log("Score for Direction " + direction.ToString() + " is " + winCount);
         
        }
        Debug.Log("winCount for " + inputColor + " is " + maxWinCount);
        if (maxWinCount > 3) {
            return true;
        }

        return false;

    }
}







/*
 HI HI HI HI HI

*/ 
   




