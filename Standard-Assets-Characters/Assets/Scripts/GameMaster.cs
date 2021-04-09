using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StandardAssets.Characters.ThirdPerson
{
    public class GameMaster : MonoBehaviour
    {
        private static GameMaster _instance;
        public static GameMaster Instance
        {
            get
            {
                return _instance;
            }

        }

        public Text textDisplay;
        public ThirdPersonInput thirdPersonInput;
        private bool playerWon;
        public float timeRemaining = 100;

        // Start is called before the first frame update
        void Start()
        {
            if (GameMaster.Instance == null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogError("Multiple game masters detected " + gameObject.name);
                Destroy(this.gameObject); //Delete the object
            }
            playerWon = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (timeRemaining > 0)
            {
                CountDown();
            }else{

            }
        }

        public void EndGame(bool playerWins){
            Debug.Log("Disabling input");
            thirdPersonInput.enabled = false;
            playerWon = playerWins;

            if (playerWon)
            {
                textDisplay.text = "Virtual Player Wins";
            }else{
                textDisplay.text = "Physical Player Wins";
            }
        }

        void CountDown()
        {
            if (!playerWon)
            {
                // Reference: https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
                timeRemaining -= Time.deltaTime;
                timeRemaining = Mathf.Max(timeRemaining, 0);
                textDisplay.text = "Time Remaining: " + DisplayTime(timeRemaining);
            }
        }

        string DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}