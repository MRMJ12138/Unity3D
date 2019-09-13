using UnityEngine;
using System.Collections;

public class TicTacToe : MonoBehaviour
{

    private int turn = 1;    //1 - Player 1, 0 - Player 2 
    private int[,] board = new int[3, 3];
    private int result = 0;  //0代表未完成，1 - 1 wins，2-2 wins，3-平局

    //背景及棋子
    public Texture2D img;
    public Texture2D img1;
    public Texture2D img2;

    // Use this for initialization 
    void Reset()
    {
        result = 0;
        turn = 1;
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                board[i, j] = 0;
            }
        }
    }

    void Start()
    {
        Reset();
    }

    void OnGUI()
    {

        //Welcome's fontstyle
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;
        fontStyle.normal.textColor = new Color(0, 0, 0);
        fontStyle.fontSize = 20;

        //Result's fontstyle
        GUIStyle fontStyle2 = new GUIStyle();
        fontStyle2.normal.background = null;
        fontStyle2.normal.textColor = new Color(0, 0, 0);
        fontStyle2.fontSize = 15;

        //Players & turns' fontstyle
        GUIStyle fontStyle3 = new GUIStyle();
        fontStyle3.normal.background = null;
        fontStyle3.normal.textColor = new Color(0, 0, 0);
        fontStyle3.fontSize = 15;

        //dots' fontstyle
        GUIStyle fontStyle4 = new GUIStyle();
        fontStyle4.normal.background = null;
        fontStyle4.normal.textColor = new Color(0, 0, 0);
        fontStyle4.fontSize = 40;

        //Background
        GUI.Label(new Rect(0, 0, 640, 360), img);

        //Welcome slogan
        GUI.Label(new Rect(210, 15, 120, 100), "井字棋", fontStyle);

        //Show the Players
        GUI.Label(new Rect(50, 70, 50, 50), img1);
        GUI.Label(new Rect(120, 95, 100, 50), "玩家1", fontStyle3);
        GUI.Label(new Rect(50, 140, 50, 50), img2);
        GUI.Label(new Rect(120, 165, 100, 50), "玩家2", fontStyle3);

        //Show the turns
        GUI.Label(new Rect(30, 50, 100, 50), "回合：", fontStyle3);
        if (turn == 1)
        {
            GUI.Label(new Rect(30, 70, 100, 50), "·", fontStyle4);
        }
        else
        {
            GUI.Label(new Rect(30, 140, 100, 50), "·", fontStyle4);
        }

        //Reset button
        if (GUI.Button(new Rect(270, 60, 90, 50), "重新开始"))
            Reset();

        // Show the board
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (board[i, j] == 1)
                    GUI.Button(new Rect(210 + i * 70, 140 + j * 70, 70, 70), img1);
                if (board[i, j] == 2)
                    GUI.Button(new Rect(210 + i * 70, 140 + j * 70, 70, 70), img2);
                if (GUI.Button(new Rect(210 + i * 70, 140 + j * 70, 70, 70), ""))
                {
                    if (result == 0)
                    {
                        if (turn == 1)
                            board[i, j] = 1;
                        else
                            board[i, j] = 2;
                        turn = 1 - turn;
                    }
                }
            }
        }

        //Show the result
        GUI.Label(new Rect(50, 220, 100, 70), "结果：", fontStyle2);
        result = check();
        if (result == 1)
        {
            GUI.Label(new Rect(105, 220, 100, 70), "玩家1获胜！", fontStyle2);
        }
        else if (result == 2)
        {
            GUI.Label(new Rect(105, 220, 100, 70), "玩家2获胜！", fontStyle2);
        }
        else if (result == 3)
        {
            GUI.Label(new Rect(105, 220, 100, 70), "平局", fontStyle2);
        }
        else
        {
            GUI.Label(new Rect(105, 220, 100, 70), "正在进行", fontStyle2);
        }

        //A random step
        if (GUI.Button(new Rect(50, 255, 100, 70), "电脑") && result == 0)
        {
            int ri = (int)Random.Range(0, 20);
            ri %= 3;
            int rj = (int)Random.Range(0, 20);
            rj %= 3;
            int cnt = 0;
            while (board[ri, rj] != 0)
            {
                ri = ri + 1;
                ri %= 3;
                cnt++;
                if (cnt == 3)
                    break;
            }
            cnt = 0;
            while (board[ri, rj] != 0)
            {
                rj = rj + 1;
                rj %= 3;
                if (cnt == 3)
                    break;
            }
            int flag = 0;
            if (board[ri, rj] == 0)
                flag = 1;
            if (turn == 1 && flag == 1)
                board[ri, rj] = 1;
            else if (turn == 0 && flag == 1)
                board[ri, rj] = 2;
            turn = 1 - turn;
        }
    }

    //Check the result
    int check()
    {
        int count = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] != 0)
                    count++;
            }
        }
        for (int i = 0; i < 3; ++i)
        {
            if (board[i, 0] != 0 && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                return board[i, 0];
            }
        }
        for (int j = 0; j < 3; ++j)
        {
            if (board[0, j] != 0 && board[0, j] == board[1, j] && board[1, j] == board[2, j])
            {
                return board[0, j];
            }
        }
        if (board[1, 1] != 0 &&
            board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] ||
            board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            return board[1, 1];
        }
        if (count == 9)
            return 3;
        return 0;
    }
}