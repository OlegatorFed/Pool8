using System.Collections.Generic;
using System.IO;
using UnityEditor;
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

        cyclerAnother.nextScene = levelList[1 % levelList.Length];

        BuildLevel(levelList[0]);

        levelCount++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Texture2D ReadLevel(string name)
    {
        var levelData = Resources.Load<Texture2D>($"Levels\\{name}");

        return levelData;
    }

    private void BuildLevel(string path)
    {
        Texture2D imageMap = ReadLevel(path);

        Vector3 brushCoor;

        for (int i = 0; i < imageMap.height; i++)
        {
            for (int j = 0; j < imageMap.width; j++)
            {
                brushCoor = new Vector3(i, 0, j);

                Color pixel = imageMap.GetPixel(j, imageMap.height - i - 1);

                int colorCode = ((int) (pixel.r * 255) << 16) | ((int) (pixel.g * 255) << 8) | ((int) (pixel.b * 255) << 0);

                if (colorCode == 0x7F7F7F)
                {
                    mapProps.Add(Instantiate(wallProp, brushCoor + wallAscension, Quaternion.identity).gameObject);
                }
                else if (colorCode == 0xFFFFFF)
                {
                    mapProps.Add(Instantiate(planeProp, brushCoor, Quaternion.identity).gameObject);
                }
                else if (colorCode == 0xFFD800)
                {
                    mapProps.Add(Instantiate(coin, brushCoor, Quaternion.identity).gameObject);
                    mapProps.Add(Instantiate(planeProp, brushCoor, Quaternion.identity).gameObject);
                }
                else if (colorCode == 0xED1C24)
                {
                    mapProps.Add(Instantiate(enemyBall, brushCoor + enemyAscension, Quaternion.identity).gameObject);
                    mapProps.Add(Instantiate(planeProp, brushCoor, Quaternion.identity).gameObject);
                }
                else if (colorCode == 0x4CFF00)
                {
                    mapProps.Add(Instantiate(desWall, brushCoor + wallAscension, Quaternion.identity).gameObject);
                    mapProps.Add(Instantiate(planeProp, brushCoor, Quaternion.identity).gameObject);
                }
                else if (colorCode == 0x00A2E8)
                {
                    mapProps.Add(Instantiate(planeProp, brushCoor, Quaternion.identity).gameObject);
                    player.transform.position = brushCoor + wallAscension * 1.5f;
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
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        foreach (GameObject obj in mapProps)
        {
            Destroy(obj);
        }

        cyclerAnother.nextScene = levelList[levelCount++ % levelList.Length];

        BuildLevel(nextLevel);
    }
}
