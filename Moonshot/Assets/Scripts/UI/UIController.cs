using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text _TextTimer;
    public float _Timer;
    public PlayerController _Player;


    // Start is called before the first frame update
    void Start()
    {
        //mTextTimer = null;
        //mTimer = 30;
    }

    // Update is called once per frame
    void Update()
    {
        LevelCountdownTimer();
    }


    private void LevelCountdownTimer()
    {
        if (_Player.GotTimeStar)
        {
            _Timer += 45.0f;            //TODO: how much more time to add
            _Player.GotTimeStar = false;
        }

        if (_Timer > 0)
        {
            _Timer -= Time.deltaTime;
            _TextTimer.text = ((int)_Timer).ToString();
        }
        else
        {
            _Timer = 0;
            _TextTimer.text = _Timer.ToString();
            //SceneManager.LoadScene(3); //wtv scene is the gameover scene
        }
    }
}
