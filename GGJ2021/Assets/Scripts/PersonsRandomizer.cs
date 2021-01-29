using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomizes elements of decorations of persons from available pool
/// </summary>
public class PersonsRandomizer : MonoBehaviour
{
    [SerializeField] List<Color> _clothsColor = new List<Color>();
    [SerializeField] List<Color> _skinTones = new List<Color>();
    [SerializeField] List<Color> _hairColors = new List<Color>();

    // Serialize chances

    public void RandomizePerson(Person person)
    {
        var female = UnityEngine.Random.Range(0, 100) > 50;
        var useLuggage = UnityEngine.Random.Range(0, 100) > 50;
        var useBackpack = UnityEngine.Random.Range(0, 100) > 50;

        var components = person.gameObject.GetComponentsInChildren<BodyPart>();

        var trousersColor = _clothsColor[UnityEngine.Random.Range(0, _clothsColor.Count)];
        var skinTone = _skinTones[UnityEngine.Random.Range(0, _skinTones.Count)];
        var hairColor = _hairColors[UnityEngine.Random.Range(0, _hairColors.Count)];
        var shirtColor = _clothsColor[UnityEngine.Random.Range(0, _clothsColor.Count)];
        var backpackColor = _clothsColor[UnityEngine.Random.Range(0, _clothsColor.Count)];

        List<BodyPart> hairs = new List<BodyPart>();

        foreach (var bp in components)
        {
            bp.gameObject.SetActive(false);

            if(bp.Type == BodyPart.BodyPartType.Trousers)
            {
                bp.gameObject.SetActive(true);
                bp.GetComponent<SpriteRenderer>().color = trousersColor;
            }

            if(bp.Type == BodyPart.BodyPartType.FemaleHead && female)
            {
                bp.gameObject.SetActive(true);
                bp.GetComponent<SpriteRenderer>().color = skinTone;
            }

            if(bp.Type == BodyPart.BodyPartType.MaleHead && !female)
            {
                bp.gameObject.SetActive(true);
                bp.GetComponent<SpriteRenderer>().color = skinTone;
            }

            if (bp.Type == BodyPart.BodyPartType.SkinPart)
            {
                bp.gameObject.SetActive(true);
                bp.GetComponent<SpriteRenderer>().color = skinTone;
            }

            if(bp.Type == BodyPart.BodyPartType.HairFemale && female)
            {
                //Debug.Log("Adding hair female");
                hairs.Add(bp);
            }

            if(bp.Type == BodyPart.BodyPartType.HairMale && !female)
            {
                //Debug.Log("Adding hair male");
                hairs.Add(bp);
            }

            if(bp.Type == BodyPart.BodyPartType.Shirt)
            {
                bp.gameObject.SetActive(true);
                bp.GetComponent<SpriteRenderer>().color = shirtColor;
            }

            if(bp.Type == BodyPart.BodyPartType.Luggage && useLuggage)
            {
                bp.gameObject.SetActive(true);
            }

            if(bp.Type == BodyPart.BodyPartType.Backpack && useBackpack)
            {
                bp.gameObject.SetActive(true);
                bp.GetComponent<SpriteRenderer>().color = backpackColor;
            }

            if(bp.Type == BodyPart.BodyPartType.EyesFemale && female)
            {
                bp.gameObject.SetActive(true);
            }

            if (bp.Type == BodyPart.BodyPartType.EyesMale && !female)
            {
                bp.gameObject.SetActive(true);
            }

        }

        var hair = hairs[UnityEngine.Random.Range(0, hairs.Count)];
        hair.GetComponent<SpriteRenderer>().color = hairColor;
        hair.gameObject.SetActive(true);
    }

}
