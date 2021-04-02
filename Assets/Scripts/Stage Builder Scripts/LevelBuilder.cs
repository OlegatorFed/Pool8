using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    private List<GameObject> mapProps = new List<GameObject>();

    public WallProp wallProp;
    public PlaneProp planeProp;
    public EnemyBall enemyBall;
    public CollectableCoin coin;
    public DestructableWall desWall;

    public PlaneProp safePlane;
    public Ball player;
    public CueScript cue;

    public Material PlayerMaterial;

    Vector3 wallAscension = new Vector3(0, 0.5f, 0);


    // Start is called before the first frame update
    void Start()
    {
        string level = ReadLevel("Assets\\Scripts\\Stage Builder Scripts\\TestLevel");

        Debug.Log(level);

        BuildLevel(level);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ReadLevel(string name)
    {
        string path = name;

        string readText = File.ReadAllText(path);

        return readText;
    }

    private void BuildLevel(string textMap)
    {
        int lineLength;
        Vector3 brushCoor;
        
        string[] lineText = textMap.Split('\n');
        int lines = lineText.Length;


        for (int i = 0; i < lines; i++)
        {
            lineLength = lineText[i].Length;

            for (int j = 0; j < lineLength; j++)
            {
                brushCoor = new Vector3(i, 0, j);

                switch (lineText[i][j])
                {
                    case 'w':
                        mapProps.Add(Instantiate(wallProp, brushCoor + wallAscension, Quaternion.identity).gameObject);
                        break;
                    case '.':
                        mapProps.Add(Instantiate(planeProp, brushCoor, Quaternion.identity).gameObject);
                        break;
                    case 'P':
                        mapProps.Add(Instantiate(planeProp, brushCoor, Quaternion.identity).gameObject);
                        player.transform.position = brushCoor + wallAscension * 1.5f;
                        break;
                    case 'c':
                        mapProps.Add(Instantiate(coin, brushCoor, Quaternion.identity).gameObject);
                        mapProps.Add(Instantiate(planeProp, brushCoor, Quaternion.identity).gameObject);
                        break;
                    case 'e':
                        mapProps.Add(Instantiate(enemyBall, brushCoor, Quaternion.identity).gameObject);
                        mapProps.Add(Instantiate(planeProp, brushCoor, Quaternion.identity).gameObject);
                        break;
                    //case 'E':
                    //    EnemyBall e = Instantiate(enemyBall, brushCoor + wallAscension*0.6f, Quaternion.identity);
                    //    e.Bullet.bulletTimeFactor = Bullet.TimeFactor.RealTime;
                    //    mapProps.Add(e.gameObject);
                    //    mapProps.Add(Instantiate(planeProp, brushCoor, Quaternion.identity).gameObject);
                    //    break;
                    case 'd':
                        mapProps.Add(Instantiate(desWall, brushCoor + wallAscension, Quaternion.identity).gameObject);
                        mapProps.Add(Instantiate(planeProp, brushCoor, Quaternion.identity).gameObject);
                        break;
                }

            }
        }
        int totalCoins = GameObject.FindObjectsOfType<CollectableCoin>().Length;
        Gameplay.instance.totalCoinAmount = totalCoins;
        Gameplay.instance.collectableText.Initialize(totalCoins);
    }

    public void NextLevelLoad(string nextLevel)
    {
        player.transform.position = safePlane.transform.position + wallAscension;

        foreach (GameObject obj in mapProps)
        {
            Destroy(obj);
        }

        BuildLevel(ReadLevel(nextLevel));
    }
}
