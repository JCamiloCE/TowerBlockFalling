using System.Collections.Generic;
using UnityEngine;

namespace Emc2.Scripts.BuildingTenant
{
    [CreateAssetMenu(fileName = "ToyData", menuName = "ScriptableObjects/ToyDataScriptableObject", order = 0)]
    public class ToyDataScriptableObject : ScriptableObject
    {
        public List<ToyData> toyDataScriptable;

        public ToyData GetToyDataByHeight(float height) 
        {
            return toyDataScriptable.Find(toy => height >= toy.emergenceHeightMin && height <= toy.emergenceHeightMax);
        }
    }
}