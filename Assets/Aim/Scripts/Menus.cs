using System;
using System.Collections;
using System.Collections.Generic;
using Jackal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menus : Singleton<Menus> {

    public GameObject mainMenuUI;
    public GameObject settingsMenu;
    public GameObject levelSelectUI;
    public GameObject gameplayUI;
    public GameObject pauseMenuUI;
    public GameObject levelCompleteUI;
    public GameObject pauseButton;
    public InstantiateBall instantiateBall;
    public Toggle invertControls;
    public Toggle vibration;
    public Slider audioSlider;
    private AudioSource buttonSound;

    void Start() {
        buttonSound = GameObject.Find("ButtonSound").GetComponent<AudioSource> ();
    }

    public void ShowSettingsMenu() {
        buttonSound.Play();
        settingsMenu.SetActive(true);
        if(PlayerPrefs.GetInt("InvertControls") == 0) {
            invertControls.isOn = false;
        }else {
            invertControls.isOn = true;
        }

        if(PlayerPrefs.GetInt("Vibration") == 0) {
            vibration.isOn = false;
        }else {
            vibration.isOn = true;
        }
    }

    public void Volume() {
        AudioListener.volume = audioSlider.value;
    }

    public void InvertControls() {
        if(invertControls.isOn) {
            PlayerPrefs.SetInt("InvertControls", 1);
        }else {
            PlayerPrefs.SetInt("InvertControls", 0); 
        }
     }

    public void Vibration() {
        if(vibration.isOn) {
            PlayerPrefs.SetInt("Vibration", 1);
        }else {
            PlayerPrefs.SetInt("Vibration", 0); 
        }
    }

    public void HideSettingsMenu() {
        buttonSound.Play();
        settingsMenu.SetActive(false);
    }

    public void ShowLevelSelectMenuAnimation() {
        buttonSound.Play();
        GetComponent<MenuTransitionAnimation> ().menu = 0;
        GetComponent<MenuTransitionAnimation> ().enabled = true;
    }

    public void HideLevelSelectMenuAnimation() {
        buttonSound.Play();
        GetComponent<MenuTransitionAnimation> ().menu = 1;
        GetComponent<MenuTransitionAnimation> ().enabled = true;
    }

    public void ShowLevelSelectMenu() {
        mainMenuUI.SetActive(false);
        levelSelectUI.SetActive(true);
    }

    public void HideLevelSelectMenu() {
        mainMenuUI.SetActive(true);
        levelSelectUI.SetActive(false);
    }

    public void BackToTheMainMenu () {

    }

    public void LevelLoadAnimation () {
        buttonSound.Play();
        Vars.currentLevel = EventSystem.current.currentSelectedGameObject.name;
        GetComponent<MenuTransitionAnimation> ().menu = 2;
        GetComponent<MenuTransitionAnimation> ().enabled = true;
    }
   
    public void LoadLevel() {
        instantiateBall.enabled = true;
        GameObject level = Instantiate(Resources.Load("Levels/Level" + Vars.currentLevel, typeof(GameObject))) as GameObject;
        level.name = "Level";

        mainMenuUI.SetActive(false);
        levelSelectUI.SetActive(false);
        gameplayUI.SetActive(true);
	}

    public void ShowPauseMenu() {
        buttonSound.Play();
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void HidePauseMenu() {
        buttonSound.Play();
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void RestartLevelAnimation() {
        buttonSound.Play();
        Time.timeScale = 1;
        GetComponent<MenuTransitionAnimation> ().menu = 3;
        GetComponent<MenuTransitionAnimation> ().enabled = true;
        CancelInvoke("ShowLevelCompleteMenu");
    }

    public void RestartLevel() {
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        levelCompleteUI.SetActive(false);
        pauseButton.SetActive(true);
        if(GameObject.Find("Level") != null) {
            Destroy(GameObject.Find("Level"));
        }
        LoadLevel();
    }

    public void ExitToMainMenuAnimation() {
        buttonSound.Play();
        Time.timeScale = 1;
        GetComponent<MenuTransitionAnimation> ().menu = 4;
        GetComponent<MenuTransitionAnimation> ().enabled = true;
        CancelInvoke("ShowLevelCompleteMenu");
    }

    public void ExitToMainMenu() {
        HideLevelSelectMenu();
        pauseMenuUI.SetActive(false);
        levelCompleteUI.SetActive(false);
        pauseButton.SetActive(true);
        gameplayUI.SetActive(false);
        if(GameObject.Find("Ball") != null) {
            Destroy(GameObject.Find("Ball"));
        }
        if(GameObject.Find("Level") != null) {
            Destroy(GameObject.Find("Level"));
        }
        instantiateBall.enabled = false;
    }

    public void LevelComplete() {
        int currentLevel = Int32.Parse(Vars.currentLevel);
        if(PlayerPrefs.GetInt("LevelUnlock") < currentLevel + 1) {
            PlayerPrefs.SetInt("LevelUnlock", currentLevel + 1);
        }
        Invoke("NextLevelAnimation", 1f);
    }

    private void ShowLevelCompleteMenu() {
        levelCompleteUI.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void NextLevelAnimation() {
        GameObject.Find("SuccessSound").GetComponent<AudioSource> ().Play();
        GetComponent<MenuTransitionAnimation> ().menu = 5;
        GetComponent<MenuTransitionAnimation> ().enabled = true;
    }

    public void NextLevel() {
        instantiateBall.enabled = true;
        Vars.currentLevel = "" + (Int32.Parse(Vars.currentLevel) + 1);
        if(GameObject.Find("Level") != null) Destroy(GameObject.Find("Level"));
        GameObject level = Instantiate(Resources.Load("Levels/Level" + Vars.currentLevel, typeof(GameObject))) as GameObject;
        level.name = "Level";
        levelCompleteUI.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void ExitTheGame() {
        buttonSound.Play();
        Application.Quit();
    }

}
