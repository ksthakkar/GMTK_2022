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
    public enum Direction
    {
        Forward,
        Backward,
        Left,
        Right
    }

    public static int steps;

    public GameObject player;

    public Direction playerDir = new Direction();
    public Direction r1Dir = new Direction();

    public GridSystem grid;
    private int[] initalCoor = new int[] { 0, 0 };
    public int[] lastPose = new int[] { 5, 1 };

    private bool inBounds;
    private bool stepsOver = true;
    public static bool WASD = true;
    public static bool randomSteps = true;
    public static bool randomAll = false;
    private bool lr;

    public int stepLoopCount = 1;
    public float waitRandomTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        WASD = false;
        randomSteps = true;
        randomAll = false;
        stepsOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (lastPose[0] > -1 && lastPose[0] < GridSystem.size[0] && lastPose[1] > -1 && lastPose[1] < GridSystem.size[1])
        {
            inBounds = true;
        }
        else
        {
            inBounds = false;
        }

        if (WASD)
        {
            waitRandomTime = 0;
            stepLoopCount = 1;
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

        if (randomSteps)
        {
            if (lr)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    r1Dir = Direction.Forward;

                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    r1Dir = Direction.Left;

                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    r1Dir = Direction.Backward;

                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    r1Dir = Direction.Right;

                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerDir = r1Dir;
                    stepsOver = false;
                    lr = false;
                }
            }
        }

        if (randomSteps == true && !stepsOver)
        {

            stepLoopCount = Random.Range(1, 6);
            waitRandomTime = 0.5f;

        }


        if (randomAll)
        {
            if (lr)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    float num = Random.Range(1, 4);
                    switch (num)
                    {
                        case 1:
                            playerDir = Direction.Forward;
                            break;
                        case 2:
                            playerDir = Direction.Left;
                            break;
                        case 3:
                            playerDir = Direction.Right;
                            break;
                        case 4:
                            playerDir = Direction.Backward;
                            break;
                        default:
                            break;

                    }
                    stepsOver = false;
                    lr = false;
                }
            }
        }

            if (randomAll == true && !stepsOver)
            {

                stepLoopCount = Random.Range(1, 6);
                waitRandomTime = 0.5f;


            }

            if (inBounds && !stepsOver)
            {

                switch (playerDir)
                {
                    case Direction.Forward:
                        StartCoroutine(MoveAndWait(-1, 0, waitRandomTime));
                        stepsOver = true;
                        break;
                    case Direction.Backward:
                        StartCoroutine(MoveAndWait(1, 0, waitRandomTime));
                        stepsOver = true;
                        //lr = true;
                        break;
                    case Direction.Right:
                        StartCoroutine(MoveAndWait(0, 1, waitRandomTime));
                        stepsOver = true;
                        //lr = true;
                        break;
                    case Direction.Left:
                        StartCoroutine(MoveAndWait(0, -1, waitRandomTime));
                        stepsOver = true;
                        //lr = true;
                        break;
                    default:
                        break;
                }
            }
        }


        IEnumerator MoveAndWait(int a, int b, float sec)
        {

            for (int i = 0; i < stepLoopCount; i++)
            {
                yield return new WaitForSeconds(sec);
                calculateNewPose(a, b);
            }
            lr = true;

        }

        void pickRandomDir(Direction x)
        {
            float num = Random.Range(1, 4);

            

        }

        void calculateNewPose(int a, int b)
        {

            lastPose[0] = lastPose[0] + a;
            lastPose[1] = lastPose[1] + b;

            if (lastPose[0] < 0)
            {
                lastPose[0] = 0;
            }
            if (lastPose[0] > (GridSystem.size[0] - 1))
            {
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


        void moveToSpot(int a, int b)
        {
            Vector2 temp_pos = GridSystem.gridNum[a, b].transform.position;
            player.transform.position = temp_pos;
        }
    }
