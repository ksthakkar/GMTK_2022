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

    public void gamble(int costIncrease)
    {
        Player.currentMoney -= gambleCost;
        gambleCost += costIncrease;

        int roll = Player.rollDie(20);

        //weights it a tiny bit, but still not absolutely terrible
        if (roll > 16 && roll != 20)
        {
            roll -= 2;
        }
        else if (roll > 12 && roll != 20 && roll > 1)
        {
            roll--;
        }

        //just have an if statement for each effect with certain ranges of rolls having certain effects
        //not implemented yet since no effects so idk what to do for balancing proper rolls
    }
}
