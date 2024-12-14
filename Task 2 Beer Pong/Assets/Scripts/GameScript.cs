    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using TMPro;

    public class GameScript : MonoBehaviour
    {
        public GameObject ballPrefab;
        public Transform ballStartPos;
        public float ballStartY;
        public int totalBalls = 15;
        public TMP_Text buttonText;

        public Image forceBar;
        public RectTransform forceBarContainer;

        private int ballsUsed = 0;
        private GameObject currentBall;
        private BallScript ballScript;

        void Start()
        {
            SpawnBall();
        }

        public void SpawnNextBall()
        {
            if (currentBall != null && currentBall.GetComponent<BallScript>().ballShot)
            {
                if (ballsUsed < totalBalls)
                {
                    SpawnBall();
                }
                else
                {
                    Debug.Log("No more balls left!");
                    SceneManager.LoadScene("EndScene");
                }
;
                if (ballsUsed == totalBalls)
                {
                    buttonText.text = "RESULT";
                }
            }
            else
            {
                Debug.Log("Shoot the current ball first before spawning the next one!");
            }
        }

        private void SpawnBall()
        {
            if (ballsUsed >= totalBalls)
            {
                return;
            }

            currentBall = Instantiate(ballPrefab, ballStartPos.position, Quaternion.identity);
            
            ballScript = currentBall.GetComponent<BallScript>();
            ballScript.GetForceBarUI(forceBar, forceBarContainer);
            ballsUsed++;

            Debug.Log("Spawned Ball " + ballsUsed + " / " + totalBalls);;
        }
    }
