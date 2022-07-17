using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    public GameObject[] diceHud;

    public Animator anim;

    public ParticleSystem dust;


   // public static int steps;

    public GameObject player;

    public Direction playerDir = new Direction();
    public Direction r1Dir = new Direction();

    public GridSystem grid;
    public int[] initalCoor = new int[] {7, 7 };
    public int[] lastPose = new int[] { 5, 1 };

    private bool inBounds;
    private bool stepsOver = true;
    public static bool WASD = true;
    public static bool randomSteps = true;
    public static bool randomAll = false;
    private bool lr;

    public static int stepLoopCount = 1;
    public float waitRandomTime = 0;


    public bool playerOnTiles;

    // Start is called before the first frame update
    void Start()
    {
        WASD = false;
        randomSteps = true;
        randomAll = false;
        stepsOver = false;
        playerOnTiles = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerOnTiles)
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
                    setDiceDist(stepLoopCount);
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        r1Dir = Direction.Forward;
                        SetDiceDir(2);

                    }
                    else if (Input.GetKeyDown(KeyCode.A))
                    {
                        r1Dir = Direction.Left;
                        SetDiceDir(3);

                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        r1Dir = Direction.Backward;

                        SetDiceDir(4);

                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        r1Dir = Direction.Right;
                        SetDiceDir(1);
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
                    setDiceDist(stepLoopCount);

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        float num = Random.Range(1, 4);
                        switch (num)
                        {
                            case 1:
                                playerDir = Direction.Forward;
                                SetDiceDir(2);
                                break;
                            case 2:
                                playerDir = Direction.Left;
                                SetDiceDir(3);
                                break;
                            case 3:
                                playerDir = Direction.Right;
                                SetDiceDir(1);

                                break;
                            case 4:
                                playerDir = Direction.Backward;
                                SetDiceDir(4);
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
                
                waitRandomTime = 0.3f;


            }

            if (inBounds && !stepsOver)
            {

                switch (playerDir)
                {
                    case Direction.Forward:
                        StartCoroutine(MoveAndWait(-1, 0, waitRandomTime));
                        stepsOver = true;
                        anim.SetBool("North", false);
                        anim.SetBool("South", false);
                        anim.SetBool("West", true);
                        anim.SetBool("East", false);

                        

                        break;
                    case Direction.Backward:
                        StartCoroutine(MoveAndWait(1, 0, waitRandomTime));
                        stepsOver = true;
                        anim.SetBool("North", false);
                        anim.SetBool("South", false);
                        anim.SetBool("West", false);
                        anim.SetBool("East", true);

                        
                        break;
                    case Direction.Right:
                        StartCoroutine(MoveAndWait(0, 1, waitRandomTime));
                        stepsOver = true;
                        anim.SetBool("North", true);
                        anim.SetBool("South", false);
                        anim.SetBool("West", false);
                        anim.SetBool("East", false);


                        break;
                    case Direction.Left:
                        StartCoroutine(MoveAndWait(0, -1, waitRandomTime));
                        stepsOver = true;
                        anim.SetBool("North", false);
                        anim.SetBool("South", true);
                        anim.SetBool("West", false);
                        anim.SetBool("East", false);

                        
                        break;
                    default:
                        anim.SetBool("North", false);
                        anim.SetBool("South", false);
                        anim.SetBool("West", false);
                        anim.SetBool("East", false);
                        break;
                }
            }
        }

        if (!playerOnTiles)
        {
            player.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(resetToInit());

        }
        }

        void setDiceDist(float a)
        {
            switch (a)
            {
            case 1:
                diceHud[0].SetActive(true);
                diceHud[1].SetActive(false);
                diceHud[2].SetActive(false);   
                diceHud[3].SetActive(false); 
                diceHud[4].SetActive(false);
                diceHud[5].SetActive(false);

                break;
            case 2:
                diceHud[0].SetActive(false);
                diceHud[1].SetActive(true);
                diceHud[2].SetActive(false);
                diceHud[3].SetActive(false);
                diceHud[4].SetActive(false);
                diceHud[5].SetActive(false);

                break;
            case 3:
                diceHud[0].SetActive(false);
                diceHud[1].SetActive(false);
                diceHud[2].SetActive(true);
                diceHud[3].SetActive(false);
                diceHud[4].SetActive(false);
                diceHud[5].SetActive(false);

                break;
            case 4:
                diceHud[0].SetActive(false);
                diceHud[1].SetActive(false);
                diceHud[2].SetActive(false);
                diceHud[3].SetActive(true);
                diceHud[4].SetActive(false);
                diceHud[5].SetActive(false);

                break;
            case 5:
                diceHud[0].SetActive(false);
                diceHud[1].SetActive(false);
                diceHud[2].SetActive(false);
                diceHud[3].SetActive(false);
                diceHud[4].SetActive(true);
                diceHud[5].SetActive(false);

                break;
            case 6:
                diceHud[0].SetActive(false);
                diceHud[1].SetActive(false);
                diceHud[2].SetActive(false);
                diceHud[3].SetActive(false);
                diceHud[4].SetActive(false);
                diceHud[5].SetActive(true);

                break;
            default:
                break;


        }
        }

    void SetDiceDir(float a)
    {

        switch (a)
        {
            case 1:
                diceHud[6].SetActive(true);
                diceHud[7].SetActive(false);
                diceHud[8].SetActive(false);
                diceHud[9].SetActive(false);


                break;
            case 2:
                diceHud[6].SetActive(false);
                diceHud[7].SetActive(true);
                diceHud[8].SetActive(false);
                diceHud[9].SetActive(false);

                break;
            case 3:
                diceHud[6].SetActive(false);
                diceHud[7].SetActive(false);
                diceHud[8].SetActive(true);
                diceHud[9].SetActive(false);

                break;
            case 4:
                diceHud[6].SetActive(false);
                diceHud[7].SetActive(false);
                diceHud[8].SetActive(false);
                diceHud[9].SetActive(true);

                break;
            default:
                break;
        }
    }

    IEnumerator resetToInit()
        {
            yield return new WaitForSeconds(2);
            moveToSpot(initalCoor[0], initalCoor[1]);
        lastPose[0] = initalCoor[0];
        lastPose[1] = initalCoor[1];
            player.GetComponent<SpriteRenderer>().enabled = true;
        }

        IEnumerator MoveAndWait(int a, int b, float sec)
        {

            for (int i = 0; i < stepLoopCount; i++)
            {
                yield return new WaitForSeconds(sec);
                calculateNewPose(a, b);
            }
            lr = true;
        anim.SetBool("North", false);
        anim.SetBool("South", false);
        anim.SetBool("West", false);
        anim.SetBool("East", false);



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
            dust.Play();

            WASD = false;
            randomSteps = true;
            randomAll = false;
        }
        void OnTriggerStay2D(Collider2D other)
        {
          
            playerOnTiles = true;
        }
        void OnTriggerEnter2D(Collider2D other)
        {
         
            playerOnTiles = true;
        }

        void OnTriggerExit2D(Collider2D other)
        {
                playerOnTiles = false;
        }
}
