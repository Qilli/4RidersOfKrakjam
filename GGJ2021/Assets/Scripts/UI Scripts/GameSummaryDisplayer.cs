using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Displays a summary of our playthrough
/// </summary>
public class GameSummaryDisplayer : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void DisplaySummary(List<Person> caughtPersons)
    {
        this.gameObject.SetActive(true);

        // Display portraits and effects of given persons

        // Display score and statistics
    }
}
