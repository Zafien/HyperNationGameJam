using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwapObject : MonoBehaviour
{
    public float parryWindow = 0.5f;  // Time window in seconds for pressing E
    public Image parryIndicator;  // UI Image for the parry indicator
    public Vector3 maxScale = new Vector3(2f, 2f, 2f);  // Maximum scale of the indicator
    public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f);  // Minimum scale of the indicator
    private float timer = 0f;  // Timer to track time remaining
    private bool canParry = false;  // If the player is in the parry window

    void Start()
    {
        if (parryIndicator != null)
        {
            parryIndicator.gameObject.SetActive(false);  // Hide the indicator at the start
        }
    }

    void Update()
    {
        // Count down the timer if the player is within the parry window
        if (canParry)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                // If the time is up, reset the parry window
                ResetParryWindow();
            }
            else
            {
                // Gradually scale the image down to indicate timing
                ShrinkIndicator();
            }
        }

        // Listen for the E button press
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canParry)
            {
                ParryAction();  // Execute the parry action
                ResetParryWindow();  // Reset the window after parry
            }
            else
            {
                Debug.Log("Parry failed. Too early or too late!");
            }
        }
    }

    // This function starts the parry window
    public void StartParryWindow()
    {
        canParry = true;
        timer = parryWindow;

        if (parryIndicator != null)
        {
            parryIndicator.gameObject.SetActive(true);  // Show the indicator
            parryIndicator.rectTransform.localScale = maxScale;  // Set to max scale
        }

        Debug.Log("Parry window started! Press E to parry.");
    }

    // This function gets called when the player successfully parries
    private void ParryAction()
    {
        Debug.Log("Parry successful!");
        //Swap the Object

        // Add your parry logic here (e.g., blocking the attack, counterattack, etc.)
    }

    // Resets the parry window if the player doesn't press E in time
    private void ResetParryWindow()
    {
        canParry = false;
        timer = 0f;

        if (parryIndicator != null)
        {
            parryIndicator.gameObject.SetActive(false);  // Hide the indicator
        }

        Debug.Log("Parry window closed.");
    }

    // Shrinks the parry indicator image gradually
    private void ShrinkIndicator()
    {
        if (parryIndicator != null)
        {
            float t = 1 - (timer / parryWindow);  // Normalize time (0 to 1)
            parryIndicator.rectTransform.localScale = Vector3.Lerp(maxScale, minScale, t);
        }
    }

    // Triggered when the enemy attacks
    public void EnemyAttack()
    {
        Debug.Log("Enemy is attacking! Starting parry window...");
        StartParryWindow();
    }
}
