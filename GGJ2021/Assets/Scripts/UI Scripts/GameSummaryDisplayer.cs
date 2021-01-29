using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Opinion
{
    public Opinion(string opinion, float fromEffi)
    {
        OpinionText = opinion;
        FromEffi = fromEffi;
    }

    public string OpinionText;
    public float FromEffi;
}

/// <summary>
/// Displays a summary of our playthrough
/// </summary>
public class GameSummaryDisplayer : MonoBehaviour
{
    [SerializeField] Text _prisonersCaughtText = null;
    [SerializeField] Text _civiliansCaughtText = null;
    [SerializeField] Text _efficiencyText = null;
    [SerializeField] Text _opinionText = null;

    [SerializeField] List<Opinion> _opinions = new List<Opinion>();

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void DisplaySummary(List<Person> caughtPersons, int spawnedPrisoners, int caughtPrisoners, int caughtCivilians)
    {
        this.gameObject.SetActive(true);

        Time.timeScale = 0;
        // Display portraits and effects of given persons


        _prisonersCaughtText.text = caughtPrisoners.ToString() + " / " + spawnedPrisoners.ToString();
        _civiliansCaughtText.text = caughtCivilians.ToString();
        float efficiency = ((float)caughtPrisoners * 100.0f) / (float)(spawnedPrisoners + caughtCivilians);
        _efficiencyText.text = efficiency.ToString("F") + "%";
        _opinionText.text = GetOpinionFor(efficiency);
    }

    private string GetOpinionFor(float efficiency)
    {
        Opinion highestFound = new Opinion("NONE", -1);

        // Find the highest matching opinion to display
        foreach(var o in _opinions)
        {
            if(o.FromEffi >= highestFound.FromEffi && o.FromEffi < efficiency)
            {
                highestFound = o;
            }
        }

        return highestFound.OpinionText;
    }
}
