using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks
{
    public class BlockRange : MonoBehaviour
    {
        public List<GameObject> objectsInRange = new List<GameObject>();
        private void OnTriggerEnter(Collider other)
        {
            objectsInRange.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            objectsInRange.Remove(other.gameObject);
        }

        public bool InRange()
        {
            return objectsInRange.Count > 0;
        }
    }
}
