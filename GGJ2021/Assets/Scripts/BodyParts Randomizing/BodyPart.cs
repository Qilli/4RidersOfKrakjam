using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public enum BodyPartType {None, HairMale, HairFemale, Trousers, FemaleHead, MaleHead, SkinPart, Shirt, Luggage, Backpack, EyesFemale, EyesMale, ShirtDecor, Mouth, Brows, Glasses, AlwaysOn}

    [SerializeField] BodyPartType _partType;

    public BodyPartType Type { get { return _partType; } }
}
