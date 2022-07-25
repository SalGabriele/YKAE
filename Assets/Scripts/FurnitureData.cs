using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FurnitureType {Chair, Table, Plate, Picture1, Picture2}

[Serializable]
public class FurnitureData
{
    public FurnitureType furniture;
    public Vector3 position;
    public Quaternion rotation;
    public Color color;

    public FurnitureData(FurnitureType f, Vector3 p, Quaternion r, Color c)
    {
        furniture = f;
        position = p;
        rotation = r;
        color = c;
    }
}
