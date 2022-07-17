using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public static float currentMoney = 0;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
    }
    public static int rollDie(int n) {

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


}
