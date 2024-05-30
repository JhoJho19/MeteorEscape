using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public UIManager uIManager;

	public ScoreManager scoreManager;

	[Space(5f)]
	public GameObject player;

	[Header("Game settings")]
	[Space(5f)]
	public Material trailMaterial;

	[Space(5f)]
	public Color[] colorTable;

	[Space(5f)]
	public GameObject [] obstaclePrefab;

	[Space(5f)]
	public float minTimeBetweenObstacles = 0.5f;

	public float startTimeBetweenObstacles = 1f;

	private float currentTimeBetweenObstacles;

	private bool spawning;

	private GameObject tempObstacle;

	private Vector2 tempPos;

	private Vector3 screenSize;

	private Color color;

	public bool isClassik;
    public bool isSports;
	public bool isSpace;
	[SerializeField] Sprite [] spritepack;

	int b = 3;

	public static GameManager Instance
	{
		get;
		set;
	}

	private void Awake()
	{
		//Object.DontDestroyOnLoad(this);
		if (Instance == null && SceneManager.GetActiveScene().name != "MM")
		{
			Instance = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
    {
        Application.targetFrameRate = 60;
        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        ClassikOrNot();
        if (b == 99)
        {
            demonishen();
        }
    }

    private void ClassikOrNot()
    {
        if (isClassik)
        {
            Color color = colorTable[Random.Range(0, colorTable.Length)];
            player.GetComponent<Player>().SetColor(color);
            trailMaterial.color = color;
        }
        else if (isSports || isSpace)
        {
            Sprite sprite = spritepack[Random.Range(0, spritepack.Length)];
            player.GetComponent<Player>().SetSprite(sprite);
        }
        else
        {
            Debug.Log("You foger to set the galka, please, set the galka");
            int ret = 0;
            ret++;
            if (ret == 1)
            {
                Debug.Log("Started the maker");
            }
            ret = 3;
            ret--;
            if (ret == 2)
            {
                Debug.Log("Started the rememberer");
            }
            ret = 22;
            ret *= 2;
            if (ret == 2)
            {
                Debug.Log("Started the rememberer");
            }
            else
            {
                InitVariables();
                SpawnObstacle();
                isSports = false;
                isSpace = false;
                isSports = false;
            }
        }
        if (b == 99)
        {
            demonishen();
        }
    }

    private void Update()
	{
		if (uIManager.gameState == GameState.PLAYING && Input.GetMouseButton(0) && !uIManager.IsButton() && !spawning)
		{
			spawning = true;
			ScoreManager.Instance.StartCounting();
			InitVariables();
			StartCoroutine(SpawnObstacle());
		}
	}

	private void InitVariables()
	{
		currentTimeBetweenObstacles = startTimeBetweenObstacles;
        if (b == 99)
        {
            demonishen();
        }
    }

	private IEnumerator SpawnObstacle()
	{
		if (ScoreManager.Instance.currentScore > 50f)
		{
			currentTimeBetweenObstacles = minTimeBetweenObstacles;
		}
		else if (ScoreManager.Instance.currentScore > 35f)
		{
			currentTimeBetweenObstacles = startTimeBetweenObstacles - 0.25f;
		}
		else if (ScoreManager.Instance.currentScore > 15f)
		{
			currentTimeBetweenObstacles = startTimeBetweenObstacles - 0.15f;
		}

        int randonInt = Random.Range(0,obstaclePrefab.Length);

		tempObstacle = UnityEngine.Object.Instantiate(obstaclePrefab[randonInt]);
		tempPos = new Vector2(UnityEngine.Random.Range(0f - screenSize.x + obstaclePrefab[randonInt].GetComponent<SpriteRenderer>().bounds.size.x, screenSize.x - obstaclePrefab[randonInt].GetComponent<SpriteRenderer>().bounds.size.x), screenSize.y + obstaclePrefab[randonInt].GetComponent<SpriteRenderer>().bounds.size.y);
		if(isClassik) 
		{
            color = colorTable[Random.Range(0, colorTable.Length)];
            tempObstacle.GetComponent<Obstacle>().InitObstacle(tempPos, color);
        }
		else 
		{
            tempObstacle.GetComponent<Obstacle>().InitObstacle(tempPos);
        }

		yield return new WaitForSecondsRealtime(currentTimeBetweenObstacles);
		StartCoroutine(SpawnObstacle());
        if (b == 99)
        {
            demonishen();
        }
    }

	public void RestartGame()
	{
		if (uIManager.gameState == GameState.PAUSED)
		{
			Time.timeScale = 1f;
		}
		uIManager.ShowGameplay();
		ClearScene();
		scoreManager.ResetCurrentScore();
        if (b == 99)
        {
            demonishen();
        }
    }

	public void ClearScene()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Obstacle");
		for (int i = 0; i < array.Length; i++)
		{
			UnityEngine.Object.Destroy(array[i]);
		}
		player.GetComponent<SpriteRenderer>().enabled = true;
		player.transform.position = new Vector2(0f, -2.5f);
		ClassikOrNot();
        trailMaterial.color = color;
		player.GetComponent<TrailRenderer>().enabled = true;
        if (b == 99)
        {
			demonishen();
        }

    }

	public void GameOver()
	{
		if (uIManager.gameState == GameState.PLAYING)
		{
			player.GetComponent<TrailRenderer>().enabled = false;
			player.GetComponent<SpriteRenderer>().enabled = false;
			StopAllCoroutines();
			spawning = false;
			ScoreManager.Instance.StopCounting();
			AudioManager.Instance.PlayEffects(AudioManager.Instance.gameOver);
			uIManager.ShowGameOver();
			scoreManager.UpdateScoreGameover();
			if (b == 99)
			{
				demonishen();
            }
		}
	}

	private void demonishen()
	{
		if (b > 3)
		{
			b--;
		}
		else if (b < 0) { b++; }
		else { return; };
	}
}
