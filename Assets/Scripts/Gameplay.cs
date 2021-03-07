using Pool8;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{

    //private CueScript cue;
    public static Gameplay instance = null;

    public RespawnButton respawnButton;

    

    public bool IsPlayerDead = false;

    private Ball[] balls;
    private Field field;
    private Logic logic;
    private Ruler ruler;

    bool stillnessTrigger;

    private void Awake()
    {
        instance = this;

        

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

    public void PlayerGetsKilled()
    {
        IsPlayerDead = true;

        respawnButton.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
