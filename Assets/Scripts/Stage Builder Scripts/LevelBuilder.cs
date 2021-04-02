using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{

    public WallProp wallProp;
    public PlaneProp planeProp;
    public EnemyBall enemyBall;

    public Ball player;
    public CueScript cue;

    public Material PlayerMaterial;

    Vector3 wallAscension = new Vector3(0, 0.5f, 0);


    // Start is called before the first frame update
    void Start()
    {
        player.tag = "Player";

        cue.ball = player;

        string level = ReadLevel("Assets\\Scripts\\Stage Builder Scripts\\TestLevel");

        Debug.Log(level);

        BuildLevel(level);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string ReadLevel(string name)
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
                        Instantiate(wallProp, brushCoor + wallAscension, Quaternion.identity);
                        break;
                    case '.':
                        Instantiate(planeProp, brushCoor, Quaternion.identity);
                        break;
                }

            }

        }

    }

    private void SpawnPlayer(Vector3 coor)
    {
        
    }
}
