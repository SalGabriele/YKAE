using System.Collections.Generic;
using UnityEngine;

public class SnappingRules : MonoBehaviour
{
    public static string[] tags1 = new string[] { "Floor", "Wall", "Chair", "Table", "Plate" };
    public List<SnappingRule> snappingRulesList = new List<SnappingRule>();
}
