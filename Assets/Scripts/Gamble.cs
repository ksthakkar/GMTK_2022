using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamble : MonoBehaviour
{
    public static float gambleCost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gamble()
    {
        Player.currentMoney -= gambleCost;
        gambleCost += 15;

        int roll = Player.rollDie(6);

        switch (roll) {
            case 1:
                return;
            case 2:
                PlayerMovement.randomAll = true;
                return;
            case 3:
                PlayerMovement.randomSteps = false;
                PlayerMovement.stepLoopCount = 3;
                return;
            case 4:
                PlayerMovement.stepLoopCount++;
                return;
            case 5:
                PlayerMovement.stepLoopCount--;
                return;
            case 6:
                PlayerMovement.stepLoopCount--;
                return;
        
        }
    }
}
