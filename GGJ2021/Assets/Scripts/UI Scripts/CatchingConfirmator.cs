using UnityEngine;

public class CatchingConfirmator : MonoBehaviour
{
    Person _person = null;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void DisplayConfirmationPrompt(Person person)
    {
        _person = person;

        this.gameObject.SetActive(true);
    }

    public void ConfirmCatching()
    {
        _person.GetCaught();
        _person = null;
        this.gameObject.SetActive(false);
    }

    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }
}
