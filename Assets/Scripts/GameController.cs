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
    public float currentDistance;

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

        currentScoreText.gameObject.SetActive(false);
        bestScoreText.gameObject.SetActive(false);
        bestDistanceText.gameObject.SetActive(false);
        var obj1 = GameObject.Find("\"BEST\"");
        if (obj1 != null) obj1.SetActive(false);
        var obj2 = GameObject.Find("distanceText");
        if (obj2 != null) obj2.SetActive(false);
        var obj3 = GameObject.Find("Best Distance Text");
        if (obj3 != null) obj3.SetActive(false);
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
        //UpdateDistanceText();
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

    public void UpdateDistanceText()
    {
        distanceText.text = currentDistance.ToString("0.0");
    }
}
