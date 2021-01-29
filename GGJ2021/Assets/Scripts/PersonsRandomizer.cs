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
    [Header("Chances")]
    [Range(0.0f, 100.0f)]
    [SerializeField] float _chanceForFemale = 50.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField] float _chanceForLuggage = 50.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField] float _chanceForBackpack = 50.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField] float _chanceForShirtDecor = 50.0f;
    [Range(0.0f, 100.0f)]
    [SerializeField] float _chanceForGlasses = 50.0f;

    public void RandomizePerson(Person person)
    {
        var female = UnityEngine.Random.Range(0, 100) > _chanceForFemale;
        var useLuggage = UnityEngine.Random.Range(0, 100) > _chanceForLuggage;
        var useBackpack = UnityEngine.Random.Range(0, 100) > _chanceForBackpack;
        var useShirtDecor = UnityEngine.Random.Range(0, 100) > _chanceForShirtDecor;
        var useGlasses = UnityEngine.Random.Range(0, 100) > _chanceForGlasses;

        var components = person.gameObject.GetComponentsInChildren<BodyPart>();

        var trousersColor = _clothsColor[UnityEngine.Random.Range(0, _clothsColor.Count)];
        var skinTone = _skinTones[UnityEngine.Random.Range(0, _skinTones.Count)];
        var hairColor = _hairColors[UnityEngine.Random.Range(0, _hairColors.Count)];
        var shirtColor = _clothsColor[UnityEngine.Random.Range(0, _clothsColor.Count)];
        var backpackColor = _clothsColor[UnityEngine.Random.Range(0, _clothsColor.Count)];

        List<BodyPart> hairs = new List<BodyPart>();
        List<BodyPart> shirtDecors = new List<BodyPart>();
        List<BodyPart> mouths = new List<BodyPart>();
        List<BodyPart> brows = new List<BodyPart>();
        List<BodyPart> glasses = new List<BodyPart>();

        foreach (var bp in components)
        {
            bp.gameObject.SetActive(false);

            if (bp.Type == BodyPart.BodyPartType.Trousers)
            {
                bp.gameObject.SetActive(true);
                bp.GetComponent<SpriteRenderer>().color = trousersColor;
            }

            if (bp.Type == BodyPart.BodyPartType.FemaleHead && female)
            {
                bp.gameObject.SetActive(true);
                bp.GetComponent<SpriteRenderer>().color = skinTone;
            }

            if (bp.Type == BodyPart.BodyPartType.MaleHead && !female)
            {
                bp.gameObject.SetActive(true);
                bp.GetComponent<SpriteRenderer>().color = skinTone;
            }

            if (bp.Type == BodyPart.BodyPartType.SkinPart)
            {
                bp.gameObject.SetActive(true);
                bp.GetComponent<SpriteRenderer>().color = skinTone;
            }

            if (bp.Type == BodyPart.BodyPartType.HairFemale && female)
            {
                hairs.Add(bp);
            }

            if (bp.Type == BodyPart.BodyPartType.HairMale && !female)
            {
                hairs.Add(bp);
            }

            if (bp.Type == BodyPart.BodyPartType.Shirt)
            {
                bp.gameObject.SetActive(true);
                bp.GetComponent<SpriteRenderer>().color = shirtColor;
            }

            if (bp.Type == BodyPart.BodyPartType.Luggage && useLuggage)
            {
                bp.gameObject.SetActive(true);
            }

            if (bp.Type == BodyPart.BodyPartType.Backpack && useBackpack)
            {
                bp.gameObject.SetActive(true);
                bp.GetComponent<SpriteRenderer>().color = backpackColor;
            }

            if (bp.Type == BodyPart.BodyPartType.EyesFemale && female)
            {
                bp.gameObject.SetActive(true);
            }

            if (bp.Type == BodyPart.BodyPartType.EyesMale && !female)
            {
                bp.gameObject.SetActive(true);
            }

            if (bp.Type == BodyPart.BodyPartType.ShirtDecor && useShirtDecor)
            {
                shirtDecors.Add(bp);
            }

            if(bp.Type == BodyPart.BodyPartType.Mouth)
            {
                mouths.Add(bp);
            }

            if(bp.Type == BodyPart.BodyPartType.Brows)
            {
                brows.Add(bp);
            }

            if(bp.Type == BodyPart.BodyPartType.Glasses && useGlasses)
            {
                glasses.Add(bp);
            }

            if(bp.Type == BodyPart.BodyPartType.AlwaysOn)
            {
                bp.gameObject.SetActive(true);
            }

        }

        var hair = hairs[UnityEngine.Random.Range(0, hairs.Count)];
        hair.GetComponent<SpriteRenderer>().color = hairColor;
        hair.gameObject.SetActive(true);

        var mouth = mouths[UnityEngine.Random.Range(0, mouths.Count)];
        mouth.gameObject.SetActive(true);

        var brow = brows[UnityEngine.Random.Range(0, brows.Count)];
        brow.gameObject.SetActive(true);

        if(useGlasses)
        {
            var glass = glasses[UnityEngine.Random.Range(0, glasses.Count)];
            glass.gameObject.SetActive(true);
        }

        if (useShirtDecor)
        {
            var decor = shirtDecors[UnityEngine.Random.Range(0, shirtDecors.Count)];
            decor.gameObject.SetActive(true);
            decor.GetComponent<SpriteRenderer>().color = backpackColor;
        }
    }

}
