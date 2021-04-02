using System;
using System.Timers;
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
    public TimerText timerText;
    
    public CueScript cue;

    public CamControl camAnchor;

    public Ball playerBall;

    public bool IsPlayerDead = false;
    
    private int coinAmount = 0;
    private int totalCoinAmount = 0;

    //private Ball[] balls;
    //private Field field;
    //private Logic logic;
    //private Ruler ruler;

    //bool stillnessTrigger;

    private void Awake()
    {
        instance = this;

        totalCoinAmount = GameObject.FindObjectsOfType<CollectableCoin>().Length;

        collectableText.Initialize(totalCoinAmount);

        initCameraPosition();

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

        timerText.SetTimerText((int)Time.time / 60, (int)Time.time % 60);
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
    
    private void LateUpdate()
    {
        Physics.Simulate(Time.deltaTime);
    }

    private class PlayerDeathAction : IRewindableAction
    {
        private GameObject player;
        private GameObject cue;

        public PlayerDeathAction(GameObject player, GameObject cue)
        {
            this.player = player;
            this.cue = cue;
        }
        
        public void Dispatch()
        {
            player.GetComponent<Ball>().Kill();
            cue.SetActive(false);

            instance.IsPlayerDead = true;
            instance.SetDeathUI(true);

            RewindManager.instance.SetFreeze(true);
        }

        public void Rewind()
        {
            player.GetComponent<Ball>().Revive();
            cue.SetActive(true);

            instance.IsPlayerDead = false;
            instance.SetDeathUI(false);

            RewindManager.instance.SetFreeze(false);
        }
    }

    public void PlayerGetsKilled(GameObject player)
    {
        if (Input.GetKey(KeyCode.R))
        {
            return;
        }
        
        RewindManager.instance.Dispatch(new PlayerDeathAction(player, cue.gameObject));
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetDeathUI(bool value)
    {

        deathCanvas.gameObject.SetActive(value);

    }

    public void IncreaseCoinAmount()
    {
        coinAmount++;

        collectableText.SetCoinText(coinAmount, totalCoinAmount);
    }

    //fix this as well
    public void Win()
    {
        OnWin?.Invoke();
    }

    public void DiamondCollect()
    {
        // do nothing
    }

    private void initCameraPosition()
    {
        camAnchor.transform.position = new Vector3(playerBall.transform.position.x, 0, playerBall.transform.position.z);
    }
}
