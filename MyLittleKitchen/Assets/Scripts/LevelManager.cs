using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class PlateRequirementsList
    {
        public List<ServeFoodScript.PlateRequirements> plates;
    }

    [Header("Levels")]
    public List<PlateRequirementsList> levels;
}
