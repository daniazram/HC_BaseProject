using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Controllers
{
    [Serializable]
    public class MainMenu
    {
        public GameObject Panel;
    }

    [Serializable]
    public class Gameplay
    {
        public string LevelText = "Level :";
        public Text levelNumber;
        public GameObject Panel;
    }

    [Serializable]
    public class LevelComplete
    {
        [Range(0, 3)] public float delay = 1.5f;
        public GameObject Panel;
    }

    [Serializable]
    public class LevelFail
    {
        [Range(0, 3)] public float delay = 1.5f;
        public GameObject Panel;
    }

    public class UIManager : MonoBehaviour
    {
        public MainMenu mainMenu;
        public Gameplay gameplay;
        public LevelComplete levelComplete;
        public LevelFail levelFail;
        IEnumerator isCalled;

        public void OnGameStateChange(object[] data)
        {
            var state = (GameStateN)data[0];
            switch (state)
            {
                case GameStateN.MainMenu:
                    DisableAllPanel();
                    mainMenu.Panel.SetActive(true);
                    break;
                case GameStateN.Gameplay:
                    DisableAllPanel();
                    gameplay.levelNumber.text = string.Format("{0}{1}", gameplay.LevelText, 0);//LevelManager.Instance.LevelIndex);
                    gameplay.Panel.SetActive(true);
                    break;
                case GameStateN.LevelComplete:
                    if (isCalled == null)
                        isCalled = GameEnd(levelComplete.Panel, levelComplete.delay);
                    StartCoroutine(isCalled);
                    break;
                case GameStateN.LevelFail:
                    if (isCalled == null)
                        isCalled = GameEnd(levelFail.Panel, levelFail.delay);
                    StartCoroutine(isCalled);
                    break;
            }
        }

        IEnumerator GameEnd(GameObject gameEnd, float time, Action OnComplete = null)
        {
            yield return new WaitForSeconds(time);
            DisableAllPanel();
            gameEnd.SetActive(true);
            if (OnComplete != null)
            {
                OnComplete.Invoke();
            }
        }

        void DisableAllPanel()
        {
            mainMenu.Panel.SetActive(false);
            gameplay.Panel.SetActive(false);
            levelComplete.Panel.SetActive(false);
            levelFail.Panel.SetActive(false);
        }
    }
}