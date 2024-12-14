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
            // Spawn the first ball
            SpawnBall();
        }

        public void SpawnNextBall()
        {
            // Check if the current ball is destroyed or shot
            if (currentBall == null || currentBall.GetComponent<BallScript>().ballShot)
            {
                // Check if there are still balls left to spawn
                if (ballsUsed < totalBalls)
                {
                    // Spawn the next ball when the Next Ball UI button is clicked
                    ballsAmountText.text = "Ball " + (ballsUsed + 1) + " / " + totalBalls;
                    SpawnBall();
                }
                else
                {
                    // Load the end scene when all balls are used
                    SceneManager.LoadScene("EndScene");
                }
            }
            else
            {
                Debug.Log("Shoot the current ball first before spawning the next one!");
            }

            // Change the Next Ball button text into Result when all balls are used
            if (ballsUsed == totalBalls)
            {
                resultText.text = "RESULT";
            }
        }

        private void SpawnBall()
        {
            // Don't spawn a new ball if all balls are used
            if (ballsUsed >= totalBalls)
            {
                return;
            }

            // Spawn a new ball prefab at the start position
            currentBall = Instantiate(ballPrefab, ballStartPos.position, Quaternion.identity);

            // Get the forcebar UIs again for the new ball
            ballScript = currentBall.GetComponent<BallScript>();
            ballScript.GetForceBarUI(forceBar, forceBarContainer);

            // Increase the ballsUsed count
            ballsUsed++;
        }
    }
