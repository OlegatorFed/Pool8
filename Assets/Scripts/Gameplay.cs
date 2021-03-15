using System;

using Pool8;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{

    //private CueScript cue;
    public static Gameplay instance = null;
    public event Action OnWin;

    public DeathCanvas deathCanvas;
    public CollectableText collectableText;
    
    public CueScript cue;

    public bool IsPlayerDead = false;
    
    private int coinAmount = 0;
    private int totalCoinAmount = 0;
    private Ball[] balls;
    private Field field;
    private Logic logic;
    private Ruler ruler;

    bool stillnessTrigger;

    private void Awake()
    {
        instance = this;

        totalCoinAmount = GameObject.FindObjectsOfType<CollectableCoin>().Length;

        collectableText.Initialize(totalCoinAmount);

        //balls = GameObject.FindGameObjectsWithTag("Ball")
        //    .Select((GameObject gameObject) => gameObject.GetComponent<Ball>())
        //    .ToArray();

        //field = new Field();
        //ruler = new Ruler();
        //logic = new Logic(ruler, field);

        //logic.currentPlayer = Player.Makoto;
        //logic.otherPlayer = Player.Joker;
    }

    void Start()
    {
        //ruler.firstPlayer = Player.Makoto;

        //field.remainBalls = new List<Pattern> { };
        //field.scoredBalls = new List<Pattern> { };

        //logic.OnWin += (Player currentPlayer) => Debug.Log($"{currentPlayer} is winner");
        //logic.OnTurn += (Player currentPlayer) => Debug.Log($"{currentPlayer} made their turn");
        //logic.OnTurnPass += (Player otherPlayer) => Debug.Log($"{otherPlayer} gets turn");
        //logic.OnOneMore += (Player currentPlayer) => Debug.Log($"One More! for {currentPlayer}");
    }

    // Update is called once per frame
    void Update()
    {
        //fix it later
        if (coinAmount == totalCoinAmount && IsPlayerDead == false)
        {
            Win();
        }

        //var motionless = balls.All((Ball ball) => ball.IsStill);

        //if ( motionless && !stillnessTrigger )
        //{
        //    field.stopAllBalls();
        //    stillnessTrigger = true;
        //
        //    Debug.Log("Boru tomare");
        //}
        //else if ( !motionless && stillnessTrigger )
        //{
        //    stillnessTrigger = false;
        //}
    }

    public void PlayerGetsKilled(GameObject player)
    {
        Destroy(player);
        Destroy(cue.gameObject);

        IsPlayerDead = true;

        ShowDeathUI();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowDeathUI()
    {

        deathCanvas.gameObject.SetActive(true);

    }

    public void IncreaseCoinAmount()
    {
        coinAmount++;

        collectableText.SetCoinText(coinAmount, totalCoinAmount);
    }

    //fix this as well
    private void Win()
    {
        OnWin?.Invoke();
    }
}
