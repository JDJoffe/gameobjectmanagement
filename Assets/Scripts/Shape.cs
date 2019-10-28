﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : PersistableObject
{
    public int ShapeId
    {
        get { return shapeId; }
        set
        {
            if (shapeId == int.MinValue && value != int.MinValue) { { shapeId = value; } }      
            else { Debug.LogError("Not allowed to change shapeid"); }
        }
    }
    int shapeId = int.MinValue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
