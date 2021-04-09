using System;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{

    //private CueScript cue;
    public static Gameplay instance = null;
    public event Action OnWin;
    public event Action OnWinAnother;

    public DeathCanvas deathCanvas;
    public CollectableText collectableText;
    public TimerText timerText;
    
    public CueScript cue;

    public CamControl camAnchor;

    public Ball playerBall;

    public bool IsPlayerDead = false;
    
    public int coinAmount = 0;
    public int totalCoinAmount = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        initCameraPosition();
    }

    public void NewLevel()
    {
        initCameraPosition();

        enabled = true;
        coinAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //fix it later
        if (coinAmount == totalCoinAmount && IsPlayerDead == false)
        {
            //Win();
            WinAnother();
        }

        timerText.SetTimerText((int)Time.time / 60, (int)Time.time % 60);
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

    public void WinAnother()
    {
        OnWinAnother?.Invoke();

        enabled = false;
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
