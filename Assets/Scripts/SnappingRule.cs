using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SnappingRule
{
    public string placedObjectTag;
    public string objectToSnapTag;
    public bool canBeSnapped;
}
