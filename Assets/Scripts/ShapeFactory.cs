using System.Collections;
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
  
    // Start is called before the first frame update
    void Start()
    {
        prefabParent = GameObject.Find("PrefabParent").transform;
    }

   public Shape Get (int shapeId=0, int materialId =0)
    {
        // instantiate the prefab tied to shapeId and set its parent
        Shape o = Instantiate(prefabs[shapeId], prefabParent);
        // set the shape's id to be the local random shapeId
        o.ShapeId = shapeId;
        o.SetMaterial(materials[materialId], materialId);
        return o;
    }
    public Shape GetRandom ()
    {
        // run get with the shapeId being the random number
        return Get(Random.Range(0, prefabs.Length),Random.Range(0,materials.Length));
    }
       
}
