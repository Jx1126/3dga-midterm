using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndSceneScript : MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        if (ScoreScript.instance.GetCurrentScore() == 1)
            scoreText.text = "You were able to bounce " + ScoreScript.instance.GetCurrentScore() + " ball into the cups successfully!";
        else
            scoreText.text = "You were able to bounce " + ScoreScript.instance.GetCurrentScore() + " balls into the cups successfully!";
    }
}
