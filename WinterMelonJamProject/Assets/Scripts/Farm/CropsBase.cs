using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm
{
    public class CropsBase : MonoBehaviour
    {
        public enum CropsEnum
        {
            None,
            Crop1
        }


        [System.Serializable]
        public struct Crops
        {
            [System.Serializable]
            public struct None
            {
                [SerializeField] private string _name;

                public string Name { get { return _name; } }
            }

            [System.Serializable]
            public struct Crop1
            {
                [SerializeField] private string _name;
                [SerializeField] private Sprite _sprite;
                [SerializeField] private Sprite[] _gardenBedSprites;

                public string Name { get { return _name; } }
                public Sprite Sprite { get { return _sprite; } }
                public Sprite[] GardenBedSprites { get { return _gardenBedSprites; } }
            }

            public None _None;
            public Crop1 _Crop1;
        }

        public Crops _CropsBase;
    }
}