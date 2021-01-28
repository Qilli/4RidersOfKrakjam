using UnityEngine;

public class PoliceResponder : MonoBehaviour
{
    public void CatchThatGuy(Person person)
    {
        if(person.IsPrisoner)
        {
            // Handle prisoner caught
            Debug.Log("Prisoner Caught!");

        }
        else
        {
            Debug.Log("Innocent man have a bad day");
            // Some penalty? Or Game Over?
        }

        // Notify UI or other systems that this person is going to have a bad day

        // Remove this person

        // Deploy some kind of gravestone
    }
}
