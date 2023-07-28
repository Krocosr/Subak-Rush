using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project.Data
{
    [CreateAssetMenu(menuName = "ScriptableObject/ItemData")]
    public class ItemData : ScriptableObject
    {
        public string _itemName;
        public string _itemType;
        public int id;
        public bool _state;
        public List<GameObject> _itemPrefab = new List<GameObject>();
    }
}

