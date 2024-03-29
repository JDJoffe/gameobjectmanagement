﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ShapeFactory : ScriptableObject
{
    Transform prefabParent;
    [SerializeField]
    Shape[] prefabs;
    [SerializeField]
    Material[] materials;
    bool parentFound = false;

    public Shape Get(int shapeId = 0, int materialId = 0)
    {
        if (parentFound == false)
        {
            prefabParent = GameObject.Find("PrefabParent").transform;
            Debug.Log("found parent");
            parentFound = true;
        }
        // instantiate the prefab tied to shapeId and set its parent
        Shape o = Instantiate(prefabs[shapeId], prefabParent);
        // set the shape's id to be the local random shapeId
        o.ShapeId = shapeId;
        o.SetMaterial(materials[materialId], materialId);
        return o;
    }
    public Shape GetRandom()
    {
        // run get with the shapeId being the random number
        return Get(Random.Range(0, prefabs.Length), Random.Range(0, materials.Length));
    }

}
