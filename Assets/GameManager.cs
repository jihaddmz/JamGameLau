using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // needed for reloading scenes


public class GameManager : MonoBehaviour
{

    // variables
    public float initialTime = 60f; // initial time for the level
    public float timeToMovePortal = 5f; // time in seconds before the portal moves
    public static GameManager Instance;
    public TMP_Text timerText; // UI Text to display the timer of the level
    public TMP_Text timerPortalText; // UI Text to display the timer of the portal to move
    public GameObject portalCapsule; // reference to the portal capsule
    public float timeBonus = 5f; // Add 5 seconds per coin
    public GameObject gameOverPanel; // reference to the game over panel
    public GameObject gameWinPanel; // reference to the game over panel

    private float timer; // time for the level to finish

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this to change timer
    public void AddTime(float seconds)
    {
        if (timeToMovePortal + seconds > 9f)
        {
            timeToMovePortal = 9f;
            Debug.Log("Timer updated: " + timeToMovePortal);
            return;
        }
        timeToMovePortal += seconds;
        Debug.Log("Timer updated: " + timeToMovePortal);
    }

    // Call this to change timer
    public void MinusTime(float seconds)
    {
        timeToMovePortal -= seconds;
        Debug.Log("Timer updated: " + timeToMovePortal);
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // int minutes2 = Mathf.FloorToInt(timer / 5f);
        // int seconds2 = Mathf.FloorToInt(timer % 5f);
        timerPortalText.text = string.Format($"00:0{((int)timeToMovePortal)}");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = initialTime; // reset timer value
        Time.timeScale = 1f; // make sure time runs normally
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: decrease timer over time
        timer -= Time.deltaTime;
        if (timer < 0) { timer = 0;  ShowGameOver(); };


        timeToMovePortal -= Time.deltaTime;
        if (timeToMovePortal < 0)
        {
            timeToMovePortal = 5f; // reset timer for portal move
            MoveCapsule();  
        } 

        UpdateTimerUI();
    }

    void MoveCapsule()
    {
        if (portalCapsule != null)
        {
            Camera cam = Camera.main;
            if (cam == null) return;

            // Get viewport limits (0..1)
            float minX = 0.1f;
            float maxX = 0.9f;
            float minY = 0.1f;
            float maxY = 0.9f;

            // Pick random position inside the camera view
            Vector3 randomViewportPos = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                cam.WorldToViewportPoint(portalCapsule.transform.position).z // keep same depth
            );

            // Convert back to world position
            Vector3 newWorldPos = cam.ViewportToWorldPoint(randomViewportPos);

            portalCapsule.transform.position = newWorldPos;
        }
    }

    void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Show the panel
        }

        // Optionally stop time or disable player movement
        Time.timeScale = 0f;
    }

    public void RetryGame()
    {
        Time.timeScale = 1f; // Resume time if paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

}
