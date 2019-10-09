using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    GameObject scoreText;
    GameObject gameStatuText;
    IScore score = FirstSceneControllerBase.GetFirstSceneControllerBase() as IScore;
    IQueryStatus gameStatu = FirstSceneControllerBase.GetFirstSceneControllerBase() as IQueryStatus;

    // Use this for initialization
    void Start() {
        scoreText = GameObject.Find("Score");
        gameStatuText = GameObject.Find("GameStatu");
    }

    // Update is called once per frame
    void Update() {
        string score = Convert.ToString(this.score.GetScore());
        if (gameStatu.QueryGameStatus() == GameStatus.Lose) {
            gameStatuText.GetComponent<Text>().text = "Game Over!";
        } else if (gameStatu.QueryGameStatus() == GameStatus.Win) {
            gameStatuText.GetComponent<Text>().text = "Good Job!";
        }
        scoreText.GetComponent<Text>().text = "空格发射飞碟，鼠标击落飞碟\n" +
            "成功击落得分，不成功则扣分\n" + "分数为负游戏失败\n" +
            "分数超过150分进入挑战模式该模式一个飞碟都不能放过！\n\n\n"+
            "分数: " + score;
    }
}
