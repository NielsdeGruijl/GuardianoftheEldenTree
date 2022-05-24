using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private  List<float> highScores = new List<float>();

    public List<float> GetHighScores()
    {
        return highScores;
    }
}
