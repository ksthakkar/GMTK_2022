using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public static float currentMoney = 0;
    public static float gambleCost;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int rollDie(int n) {

        int roll = Random.Range(1, n+1);
        return roll;
    
    } 
    /*ik right now this doesnt really do much other than random number generation
     * which we already have Random.Range for, but we can probably add more to this like anims or whatever
     * and it just looks cleaner to have this which is nice
     * we'll definitely add more to it later
     * idk what, but we can probably do something
     * probably just anim here?
     * yeah
    */

    public void move() {
        int moveDist = rollDie(6);

    }


    public void gamble(int costIncrease) {
        currentMoney -= gambleCost;
        gambleCost += costIncrease;

        int roll = rollDie(20);

        //weights it a tiny bit, but still not absolutely terrible
        if (roll > 16 && roll != 20)
        {
            roll -= 2;
        }
        else if (roll > 12 && roll != 20 && roll > 1)
        {
            roll --;
        }

        //just have an if statement for each effect with certain ranges of rolls having certain effects
        //not implemented yet since no effects so idk what to do for balancing proper rolls
    }



}
