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

    [SerializeField]
    private GameObject _StoryGroup = null;
    [SerializeField]
    private GameObject _EndBackground = null;

    private void Start()
    {
        if (_StoryGroup)
            _StoryGroup.SetActive(false);

        if (_EndBackground)
            _EndBackground.SetActive(false);
    }

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

        // End Scene
        if(starCounter >= levelMaxStars)
        {
            Cursor.visible = true;

            if (_EndBackground)
                _EndBackground.SetActive(true);

            _StoryGroup.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
