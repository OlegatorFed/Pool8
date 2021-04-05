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

    public LevelCyclerAnother cyclerAnother;

    public PlaneProp safePlane;
    public Ball player;
    public CueScript cue;

    private int levelCount = 0;
    
    private string[] levelList;
    private string levelPath = "Assets\\Scripts\\Stage Builder Scripts\\";

    public Material PlayerMaterial;

    Vector3 wallAscension = new Vector3(0, 0.5f, 0);
    Vector3 enemyAscension = new Vector3(0, 0.15f, 0);
    

    // Start is called before the first frame update
    void Start()
    {
        string levelListString = File.ReadAllText("Assets\\Scripts\\Stage Builder Scripts\\LevelList");

        levelList = levelListString.Split(' ');

        cyclerAnother.nextScene = levelList[levelCount + 1];

        foreach (string s in levelList)
        {
            Debug.Log(s);
        }

        //Debug.Log(levelPath + levelList[levelCount]);

        //Debug.Log(levelList[levelCount]);

        BuildLevel(ReadLevel(levelList[levelCount]));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ReadLevel(string name)
    {
        string readText = File.ReadAllText("Assets\\Scripts\\Stage Builder Scripts\\" + levelList[levelCount]);


        return readText;
    }

    private void BuildLevel(string path)
    {
        string textMap = ReadLevel(path);

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
                        mapProps.Add(Instantiate(enemyBall, brushCoor + enemyAscension, Quaternion.identity).gameObject);
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

        if (levelCount + 1 <= levelList.Length - 1)
        {
            levelCount += 1;
        }
        else
        {
            levelCount = 0;
            cyclerAnother.nextScene = levelList[levelCount + 1];
        }
        

        Debug.Log(nextLevel);
        Debug.Log(ReadLevel( nextLevel));

        BuildLevel(ReadLevel(nextLevel));
    }
}
