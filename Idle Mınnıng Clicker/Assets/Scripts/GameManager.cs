using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalSeconds; // Total time in seconds for the game

    public Text moneyText; // Text UI element to display the collected money
    public Text timeText; // Text UI element to display the remaining time
    public Text targetMoneyText; // Text UI element to display the target money

    public int collectedMoney = 0; // Amount of money collected by the player
    public int targetMoney; // Target amount of money to collect
    public int nextlevel = 2;
    public GameObject won, lose, player;

    void Start()
    {
        // Display the target money at the start of the game
        targetMoneyText.text = "" + targetMoney;
        // Invoke the method 'ReduceSeconds' every 1 second
        InvokeRepeating("ReduceSeconds", 0, 1.0f);
    }

    // Method to increase the collected money
    public void IncreaseMoney(int amount)
    {
        collectedMoney += amount;
        // Update the money text UI element
        moneyText.text = collectedMoney.ToString();
    }

    // Method to reduce the remaining time by one second
    void ReduceSeconds()
    {
        
        // Update the time text UI element
        if (totalSeconds > 0)
        {
            totalSeconds--;
            timeText.text = totalSeconds + "";
        }

        // Check if time is up
        if (totalSeconds <= 0)
        {
            // Check if collected money is greater than or equal to the target money
            if (collectedMoney >= targetMoney)
            {
                won.SetActive(true);
                player.GetComponent<Animator>().SetTrigger("playerwin");
            }
            else
            {
                // Reload the current scene if the target money is not collected
                lose.SetActive(true);
                player.GetComponent<Animator>().SetBool("isstop", true);
            }
        }
    }

    public void nextlevelButton()
    {
        SceneManager.LoadScene(nextlevel);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
