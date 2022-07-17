using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * @author Karan
 * 
 * 
 * Uses isometric grid spots and snaps to the corresponding direction
 * Takes two static variable direction and count
 *                     disabled by WASD movement 
 *                     
 *  3 cases: WASD (mainly for testin) RANDOM move count only, Random direction + count
 */
public class PlayerMovement : MonoBehaviour
{
    public enum Direction { 
        Forward,
        Backward,
        Left,
        Right
    }

    public static int steps;

    public GameObject player;

    public Direction playerDir = new Direction();

    public GridSystem grid;
    public int[] initalCoor = new int[]{0,0};
    public int[] lastPose = new int[]{ 1, 1 };

    public bool inBounds;
    public bool stepsOver = true;
    public bool WASD = false;
    public bool randomSteps = false;
    public bool randomAll = false;

    public int stepLoopCount;
    public int waitRandomTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lastPose[0] + "   " + lastPose[1]);
        Debug.Log(GridSystem.size[0] + "   " + GridSystem.size[1]);

        if (lastPose[0] > -1 && lastPose[0] < GridSystem.size[0] && lastPose[1] > -1 && lastPose[1] < GridSystem.size[1])
        {
            inBounds = true;
        } else{
            inBounds = false;
        }

        if (WASD)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerDir = Direction.Forward;
                stepsOver = false;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                playerDir = Direction.Left;
                stepsOver = false;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                playerDir = Direction.Backward;
                stepsOver = false;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                playerDir = Direction.Right;
                stepsOver = false;
            }
        }

        if (randomSteps = true)
        {
        }

        if (inBounds && !stepsOver) {

            switch (playerDir)
            {
                case Direction.Forward:
                    for (int i = 0; i <= stepLoopCount; i++)
                    {
                        calculateNewPose(-1, 0);
                    }
                    stepsOver = true;
                    break;
                case Direction.Backward:
                    calculateNewPose(1, 0);
                    stepsOver = true;
                    break;
                case Direction.Right:
                    calculateNewPose(0, 1);
                    stepsOver = true;
                    break;
                case Direction.Left:
                    calculateNewPose(0, -1);
                    stepsOver = true;
                    break;
                default:
                    break;
            }
        }
    }

    int stepNumber()
    {

        return 0;
    }

    void calculateNewPose(int a, int b)
    {

        lastPose[0] = lastPose[0] + a;
        lastPose[1] = lastPose[1] + b;

        if (lastPose[0] < 0){
            lastPose[0] = 0;
        }
        if (lastPose[0] > (GridSystem.size[0] - 1)) {
            lastPose[0] = GridSystem.size[0] - 1;
        }
        if (lastPose[1] < 0)
        {
            lastPose[1] = 0;
        }
        if (lastPose[1] > (GridSystem.size[1] - 1))
        {
            lastPose[1] = GridSystem.size[1] - 1;
        }
        moveToSpot(lastPose[0], lastPose[1]);
        
    }


    void bounds()
    {
        if (lastPose[0] > GridSystem.size[0] - 1){
            lastPose[0] = GridSystem.size[0] - 1;
        } else if (lastPose[1] > GridSystem.size[1] - 1)
        {
            lastPose[1] = GridSystem.size[1] - 1;
        }
    }

    void moveKeyBoard(Direction d)
    {
        
    }

    void moveSpaces()
    {

    }


    void moveToSpot(int a, int b)
    {
        Vector2 temp_pos = GridSystem.gridNum[a, b].transform.position;
        player.transform.position = temp_pos;
    }
}
