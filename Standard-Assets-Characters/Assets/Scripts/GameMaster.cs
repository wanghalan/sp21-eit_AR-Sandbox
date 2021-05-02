using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;

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
        public int playerNum = 1;
        public GameObject playerObject;

        // Start is called before the first frame update
        void Start()
        {
            if (GameMaster.Instance == null)
            {
                _instance = this;
                WriteEvent("Player " + playerNum + " game start");
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
                KeyLogger();
                CountDown();
            }else{

            }
            //WriteString("Player " + playerNum + " game updates");
            if (playerObject.transform.position.y < -200)
            {
                EndGame(true);
            }
        }

        public void KeyLogger()
        {
            if (Input.GetKeyDown("w"))
            {
                WriteEvent("w");
            }
            if (Input.GetKeyDown("a"))
            {
                WriteEvent("a");
            }
            if (Input.GetKeyDown("s"))
            {
                WriteEvent("s");
            }
            if (Input.GetKeyDown("d"))
            {
                WriteEvent("d");
            }
            if (Input.GetKeyDown("space"))
            {
                WriteEvent("space");
            }
            if (Input.GetKeyDown("r"))
            {
                WriteEvent("r");
            }
        }

        public void WriteEvent(string s)
        {
            // Write string to file
            string filepath = Application.dataPath + "/Logs/" + playerNum + "_log.txt";
            Debug.Log("File path: " + filepath);
            using (StreamWriter sw = File.AppendText(filepath))
            {
                string timestamp = System.DateTime.Now.ToString("h:mm:ss tt");
                sw.WriteLine("[" + timestamp + "] " + s);
                //Debug.Log("writing: " + s);
            }
        }
        
        public void EndGame(bool playerWins){
            Debug.Log("Disabling input");
            thirdPersonInput.enabled = false;
            playerWon = playerWins;


            if (playerWon)
            {
                textDisplay.text = "Virtual Player Wins";
                WriteEvent("Virtual Player Wins");
            }
            else
            {
                textDisplay.text = "Physical Player Wins";
                WriteEvent("Physical Player Wins");
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