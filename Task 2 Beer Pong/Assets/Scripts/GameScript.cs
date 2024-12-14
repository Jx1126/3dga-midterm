    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameScript : MonoBehaviour
    {
        public GameObject ballPrefab;
        public Transform ballStartPos;
        public float ballStartY;
        public int totalBalls = 15;

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
            ballsUsed++;

            Debug.Log("Spawned Ball " + ballsUsed + " / " + totalBalls);;
        }
    }
