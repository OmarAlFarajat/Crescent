using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private AudioManager audioManager;

    // MAIN MENU
    [SerializeField]
    private GameObject _MenuGroup = null;
    [SerializeField]
    private GameObject _CreditsGroup = null;
    [SerializeField]
    private GameObject _LoadingGroup = null;
    [SerializeField]
    private GameObject _MenuControlsGroup = null;

    // MAIN MENU / PAUSE MENU
    [SerializeField]
    private GameObject _OptionsGroup = null;

    // PAUSE MENU
    [SerializeField]
    private GameObject _PauseMenuGroup = null;
    [SerializeField]
    private GameObject _ControlsGroup = null;

    void Awake()
    {
        GameObject temp_player = GameObject.Find("Player");

        if(temp_player)
            playerController = temp_player.GetComponent<PlayerController>();

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // MAIN MENU
        if (_MenuGroup)
            _MenuGroup.SetActive(true);

        if(_CreditsGroup)
            _CreditsGroup.SetActive(false);

        if (_MenuControlsGroup)
            _MenuControlsGroup.SetActive(false);

        if (_LoadingGroup)
            _LoadingGroup.SetActive(false);

        // MAIN/PAUSE MENU
        if (_OptionsGroup)
            _OptionsGroup.SetActive(false);

        // PAUSE MENU
        if (_PauseMenuGroup)
            _PauseMenuGroup.SetActive(false);

        if (_ControlsGroup)
            _ControlsGroup.SetActive(false);

    }

    void Update()
    {
        CheckInputs();
    }

    void CheckInputs()
    {
        // PLAYER CONTROLS
        if (playerController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerController.Attack();
            }

            if (Input.GetKey(KeyCode.LeftShift)){
                playerController.animator.SetBool("isRunning", true);
                playerController.isRunning = true;
            }
            else
            {
                playerController.animator.SetBool("isRunning", false);
                playerController.isRunning = false;
            }

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                playerController.animator.SetBool("isMoving", true);
                playerController.isMoving = true;
                playerController.Move();

                // Stops Run sound (if playing) and plays Walk sound
                if (!audioManager.isPlaying("Walk") && !playerController.isRunning)
                {
                    audioManager.Stop("Run");
                    audioManager.Play("Walk");
                }

                // Stops Walk sound (if playing) and plays Run sound
                if (!audioManager.isPlaying("Run") && playerController.isRunning)
                {
                    audioManager.Stop("Walk");
                    audioManager.Play("Run");
                }

                // Stop Walk or Run sound if player is jumping/falling, or attacking
                if (!playerController.isGrounded || playerController.isAttacking)
                {
                    audioManager.Stop("Walk");
                    audioManager.Stop("Run");
                }
            }
            else
            { 
                playerController.animator.SetBool("isMoving", false);
                playerController.isMoving = false;

                if (audioManager.isPlaying("Walk"))
                    audioManager.Stop("Walk");

                if (audioManager.isPlaying("Run"))
                    audioManager.Stop("Run");
            }

            if (Input.GetButtonDown("Jump")) 
            {
                playerController.Jump();
            }

            // PAUSE MENU
            if (Input.GetKeyDown("escape") && _PauseMenuGroup)
            {
                if (!_PauseMenuGroup.activeSelf && !_ControlsGroup.activeSelf)
                {
                    _PauseMenuGroup.SetActive(true);
                    Time.timeScale = 0f;
                }

                else if (!_PauseMenuGroup.activeSelf && _ControlsGroup.activeSelf)
                {
                    PauseControlsBack();
                }
                
                else if (_PauseMenuGroup.activeSelf)
                {
                    PauseContinue();
                }
            }
        }

        // MAIN MENU
        if (Input.GetKeyDown("escape"))
        {

            if (_MenuGroup && _CreditsGroup.activeSelf)
                MenuCreditsBack();

            if (_MenuGroup && _MenuControlsGroup.activeSelf)
                MenuControlsBack();

            if ((_MenuGroup || _PauseMenuGroup) && _OptionsGroup.activeSelf)
                OptionsBack();
        }
    }

    // MAIN MENU
    public void MenuStart()
    {
        _MenuGroup.SetActive(false);
        _LoadingGroup.SetActive(true);
        // TO-DO:
        //SceneManager.LoadScene("Main Scene");
        //SceneManager.LoadScene("Omar");
    }

    public void MenuCredits()
    {
        _MenuGroup.SetActive(false);
        _CreditsGroup.SetActive(true);
    }

    public void MenuControls()
    {
        _MenuGroup.SetActive(false);
        _MenuControlsGroup.SetActive(true);
    }

    public void MenuControlsBack()
    {
        _MenuGroup.SetActive(true);
        _MenuControlsGroup.SetActive(false);
    }

    public void MenuCreditsBack()
    {
        _MenuGroup.SetActive(true);
        _CreditsGroup.SetActive(false);
    }

    // MAIN/PAUSE MENU
    public void MenuExit()
    {
        Application.Quit();
    }

    public void MenuOptions()
    {
        if (_MenuGroup)
        {
            _MenuGroup.SetActive(false);
            _OptionsGroup.SetActive(true);
        }
        else if (_PauseMenuGroup)
        {
            _PauseMenuGroup.SetActive(false);
            _OptionsGroup.SetActive(true);
        }
    }

    public void OptionsBack()
    {
        if (_MenuGroup)
        {
            _MenuGroup.SetActive(true);
            _OptionsGroup.SetActive(false);
        }
        else if (_PauseMenuGroup)
        {
            _PauseMenuGroup.SetActive(true);
            _OptionsGroup.SetActive(false);
        }
    }

    // PAUSE MENU
    public void PauseQuitToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        // TO-DO:
        //SceneManager.LoadScene("Main Menu");
    }

    public void PauseControls()
    {
        _PauseMenuGroup.SetActive(false);
        _ControlsGroup.SetActive(true);
    }

    public void PauseControlsBack()
    {
        _PauseMenuGroup.SetActive(true);
        _ControlsGroup.SetActive(false);
    }

    public void PauseContinue()
    {
        _PauseMenuGroup.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}
