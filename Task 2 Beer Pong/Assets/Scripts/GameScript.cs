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
        
        public TMP_Text resultText;
        public TMP_Text ballsAmountText;
        public Image forceBar;
        public RectTransform forceBarContainer;

        private int ballsUsed = 0;
        private GameObject currentBall;
        private BallScript ballScript;

        void Start()
        {
            ballsAmountText.text = "Ball " + (ballsUsed + 1) + " / " + totalBalls;
            SpawnBall();
        }

        public void SpawnNextBall()
        {
            if (currentBall == null || currentBall.GetComponent<BallScript>().ballShot)
            {
                if (ballsUsed < totalBalls)
                {
                    ballsAmountText.text = "Ball " + (ballsUsed + 1) + " / " + totalBalls;
                    SpawnBall();
                }
                else
                {
                    Debug.Log("No more balls left!");
                    SceneManager.LoadScene("EndScene");
                }
            }
            else
            {
                Debug.Log("Shoot the current ball first before spawning the next one!");
            }

            if (ballsUsed == totalBalls)
            {
                resultText.text = "RESULT";
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
