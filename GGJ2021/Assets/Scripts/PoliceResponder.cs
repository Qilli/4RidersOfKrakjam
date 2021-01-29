using UnityEngine;

/// <summary>
/// Handles the situation when we select a person for being caught.
/// </summary>
public class PoliceResponder : MonoBehaviour
{
    [SerializeField] MainGameManager _gameManager = null;

    public void CatchThatGuy(Person person)
    {
        if(person.IsPrisoner)
        {
            // Deploy some mini game scene, if we succeedd, then the person is caught.
            Debug.Log("Prisoner Caught!");

        }
        else
        {
            Debug.Log("Innocent man have a bad day");
            // Just catch that guy?
        }

        _gameManager.PersonCaught(person);
        person.gameObject.SetActive(false);

        // Deploy some kind of gravestone
    }
}
