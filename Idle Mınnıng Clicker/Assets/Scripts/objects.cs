using UnityEngine;

public class objects : MonoBehaviour
{
    public float pullBackSpeed; // Speed at which the object is pulled back
    public int moneyValue; // Value of the object in terms of money

    public bool isSuprise = false; //Indicates if the object is a suprise
    public bool isStone = false; // Indicates if the object is a stone
    public bool isGold = false; // Indicates if the object is gold
    public bool isDiamond = false; // Indicates if the object is a diamond
    public bool isBig = false; // Indicates if the object is big

    void Start()
    {
        // If the object is big
        if (isBig)
        {
            pullBackSpeed /= 2; // Halve the pull back speed
            moneyValue *= 2; // Double the money value
        }
        // If the object is a suprise
        if (isSuprise)
        {
            // Set money value to a random value between 20 and 500
            moneyValue = Random.Range(-200, 501); // Random.Range's upper limit is exclusive, so 501 ensures 500 is included                 
            pullBackSpeed *= Random.Range(0.5f, 3.01f); // Random.Range's upper limit is exclusive, so 3.01 ensures 3 is included

        }
        // If the object is a stone
        if (isStone)
        {
            pullBackSpeed /= 2; // Halve the pull back speed
        }

        // If the object is gold
        if (isGold)
        {
            moneyValue *= 5; // Multiply the money value by 5
        }

        // If the object is a diamond
        if (isDiamond)
        {
            moneyValue = 500; // Set the money value to 500
        }
    }
}
