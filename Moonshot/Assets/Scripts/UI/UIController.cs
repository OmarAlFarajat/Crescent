using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text textStarCounter;
    public int starCounter;
    public int levelMaxStars;
    public PlayerController player;

    // Update is called once per frame
    void Update()
    {
        LevelStarCount();
    }


    private void LevelStarCount()
    {
        if (player.GotStar)
        {
            starCounter += 1;
            player.GotStar = false;
        }

        textStarCounter.text = starCounter.ToString() + "/" + levelMaxStars.ToString();
    }
}
