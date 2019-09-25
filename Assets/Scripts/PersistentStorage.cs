using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// script saves and loads data of a single persistabe object 
public class PersistentStorage : MonoBehaviour
{
    string savePath;
    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveFile");
    }
    // save the data to the location
    public void Save(PersistableObject o)
    {
        using (var writer = new BinaryWriter(File.Open(savePath,FileMode.Create)))
        {
            o.Save(new GameDataWriter(writer));
        }
    }
    // load the data from the location
    public void Load(PersistableObject o)
    {
        using (var reader = new BinaryReader(File.Open(savePath, FileMode.Open)))
        {
            o.Load(new GameDataReader(reader));
        }
    }
}
