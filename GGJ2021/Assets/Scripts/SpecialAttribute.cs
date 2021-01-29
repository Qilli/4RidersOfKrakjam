using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAttribute")]
public class SpecialAttribute : ScriptableObject
{
    [SerializeField] protected string _nameToDisplay = string.Empty;

    public string NameToDisplay { get { return _nameToDisplay; } }

    public virtual void ApplyEffects(Person person)
    {
        // Should apply some extra effects to this person, like being fast, or slower?
    }
}
