using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI startText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI bestDistanceText;

    private ObstacleController obstacleController;
    private CameraController cameraController;
    private PlayerController playerController;

    int currentScore;
    float currentDistance;

    void Start()
    {
        currentScore = 0;
        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        bestDistanceText.text = PlayerPrefs.GetFloat("BestDistance", 0).ToString("0.0");
        SetScore();

        startText.gameObject.SetActive(false);

        cameraController = Camera.main.GetComponent<CameraController>();

        GameObject playerInstance = GameObject.Find("Player");
        playerController = playerInstance.GetComponent<PlayerController>();
        cameraController.player = playerInstance;

        GameObject obstacleControllerInstance = GameObject.Find("Obstacle Controller");
        obstacleController = obstacleControllerInstance.GetComponent<ObstacleController>();
        obstacleController.player = playerInstance;
    }

    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    startText.gameObject.SetActive(false);
        //}
    }

    public void CallGameOver()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        gameOverPanel.SetActive(true);
        yield break;
    }

    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        obstacleController.ResetObstacles();
        playerController.ResetState();
        currentScore = 0;
        currentDistance = 0;
        SetScore();
        UpdateDistanceText();
    }

    public void AddScore()
    {
        currentScore++;
        if(currentScore > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            bestScoreText.text = currentScore.ToString();
        }
        SetScore();
    }

    void SetScore()
    {
        currentScoreText.text = currentScore.ToString();
    }

    public void UpdateDistance()
    {
        currentDistance = playerController.DistanceFromStart.y;
        if (currentDistance > PlayerPrefs.GetFloat("BestDistance", 0))
        {
            PlayerPrefs.SetFloat("BestDistance", currentDistance);
            bestDistanceText.text = currentDistance.ToString("0.0");
        }
        UpdateDistanceText();
    }

    void UpdateDistanceText()
    {
        distanceText.text = currentDistance.ToString("0.0");
    }
}
