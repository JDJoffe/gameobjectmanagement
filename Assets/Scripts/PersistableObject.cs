using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
// saves and loads data of this object
public class PersistableObject : MonoBehaviour
{
    // write the properties of this object
    public virtual void Save(GameDataWriter writer)
    {
        writer.Write(transform.localPosition);
        writer.Write(transform.localRotation);
        writer.Write(transform.localScale);
    }
    public virtual void Load(GameDataReader reader)
    {
        // load the properties of this object
        transform.localPosition = reader.ReadVector3();
        transform.localRotation = reader.ReadQuarternion();
        transform.localScale = reader.ReadVector3();
    }
}
