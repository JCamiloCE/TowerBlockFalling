using System;
using UnityEngine;

namespace Emc2.Scripts.BuildingTenant
{
    //Each component of the enum need a identification
    //The identification must be the index of the first three letters
    //Index: A:00, B:01,...,Y:25,Z:25
    //Note: the  index -1 is reserved for Invalid
    public enum EToyIdentifier
    {
        Invalid = -1,
        None = 0,
        CLOUD = 021114
    }

    [Serializable]
    public class ToyData
    {
        public EToyIdentifier toyIdentifier;
        public Sprite sprite;
        public float emergenceHeightMin;
        public float emergenceHeightMax;
        public float speed;
    }
}