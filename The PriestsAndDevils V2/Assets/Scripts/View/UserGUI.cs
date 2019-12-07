using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MyNamespace {
  public class UserGUI : MonoBehaviour {
    private string guide = "船每次载两个人。\n" +
        "一个人必须驾驶这艘船。\n" +
        "单击人或船移动它们。\n" +
        "牧师人数少于魔鬼时会被杀害。\n" +
        "保证所有的牧师都活着。\n" +
        "加油！\n";
    private string hint = "";

    private IUserAction action;
    private GUIStyle textStyle;
    private GUIStyle hintStyle;
    private GUIStyle prietStyle;
    private GUIStyle devilStyle;
    private GUIStyle btnStyle;

    public CharacterController characterCtrl;
    public static IState state = new IState(0, 0, 3, 3, false, null);
    public static IState endState = new IState(3, 3, 0, 0, true, null);
    public static int status;
    public static FirstController controller;

    // Use this for initialization
    void Start() {
      status = 0;
      action = Director.GetInstance().CurrentSecnController as IUserAction;
      controller = Director.GetInstance().CurrentSecnController as FirstController;
    }

    // Update is called once per frame
    void OnGUI() {
      textStyle = new GUIStyle {
        fontSize = 40,
        alignment = TextAnchor.MiddleCenter
      };
      hintStyle = new GUIStyle {
        fontSize = 15,
        fontStyle = FontStyle.Normal
      };
      prietStyle = new GUIStyle {
        fontSize = 20,
        fontStyle = FontStyle.Normal,
      };
      prietStyle.normal.textColor = new Color(255, 215, 0);
      devilStyle = new GUIStyle {
        fontSize = 20,
        fontStyle = FontStyle.Normal
      };
      devilStyle.normal.textColor = new Color(255, 0, 0);
      btnStyle = new GUIStyle("button") {
        fontSize = 20
      };
      GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 250, 100, 50),
          "牧师与魔鬼", textStyle);
      GUI.Label(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 185, 100, 50),
          "方块: 牧师", prietStyle);
      GUI.Label(new Rect(Screen.width / 2 + 20, Screen.height / 2 - 185, 100, 50),
          "球体: 魔鬼", devilStyle);
      GUI.Label(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 210, 100, 50),
         hint, hintStyle);
      if (GUI.Button(new Rect(Screen.width / 2 + 25, Screen.height / 2 - 150, 75, 50), "帮助", btnStyle)) {
        UnityEditor.EditorUtility.DisplayDialog("帮助", guide, "明白了");
      }
      if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 75, 50), "提示", btnStyle)) {
        //Debug.Log("StateRight: " + state.rightDevils + " " + state.rightPriests);
        //Debug.Log("StateLeft: " + state.leftDevils + " " + state.leftPriests);

        IState temp = IState.bfs(state, endState);
        //Debug.Log("NextRight: " + temp.rightDevils + " " + temp.rightPriests);
        //Debug.Log("NextLeft: " + temp.leftDevils + " " + temp.leftPriests);
        hint = "提示：\n" + "右侧：魔鬼: " + temp.rightDevils + "   牧师: " + temp.rightPriests +
          "\n左侧：魔鬼: " + temp.leftDevils + "   牧师: " + temp.leftPriests;
        //int priestsOffset = temp.leftPriests - state.leftPriests;
        //int devilsOffset = temp.leftDevils - state.leftDevils;
        //Debug.Log("offset: " + priestsOffset + " " + devilsOffset);
        //controller.AIMove(priestsOffset, devilsOffset);
      }
      if (status == 1) {
        // GameOver
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "游戏结束!", textStyle);
        if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "重新开始", btnStyle)) {
          status = 0;
          action.Restart();
        }
      } else if (status == 2) {
        // Win
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "你赢了!", textStyle);
        if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "重新开始", btnStyle)) {
          status = 0;
          action.Restart();
        }
      }
    }

    public void SetCharacterCtrl(CharacterController _characterCtrl) {
      characterCtrl = _characterCtrl;
    }

    void OnMouseDown() {
      if (status != 1) {
        if (gameObject.name == "boat") {
          action.MoveBoat();
          // update state
          int rightPriest = controller.rightCoastCtrl.GetCharacterNum()[0];
          int rightDevil = controller.rightCoastCtrl.GetCharacterNum()[1];
          int leftPriest = controller.leftCoastCtrl.GetCharacterNum()[0];
          int leftDevil = controller.leftCoastCtrl.GetCharacterNum()[1];
          bool location = controller.boatCtrl.boat.Location == Location.left ? true : false;
          int pcount = controller.boatCtrl.GetCharacterNum()[0];
          int dcount = controller.boatCtrl.GetCharacterNum()[1];
          if (location) {
            leftPriest += pcount;
            leftDevil += dcount;
          } else {
            rightPriest += pcount;
            rightDevil += dcount;
          }
          state = new IState(leftPriest, leftDevil, rightPriest, rightDevil, location , null);
        } else {
          action.CharacterClicked(characterCtrl);
        }
      }
    }
  }
}
