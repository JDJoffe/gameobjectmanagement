using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : PersistableObject
{
    // just the position 
    public PersistableObject prefab;
    public Transform prefabParent;
    public List<PersistableObject> cubes;
    public PersistentStorage storage;
    private string savePath;
    // keycode variable, could maybe use this for changeable controls
    public KeyCode createKey = KeyCode.C;
    public KeyCode newGameKey = KeyCode.N;
    public KeyCode saveKey = KeyCode.S;
    public KeyCode loadKey = KeyCode.L;
    private void Awake()
    {
        cubes = new List<PersistableObject>();
    }
    private void Update()
    {
        #region inputs
        if (Input.GetKeyDown(createKey))
        {
            // call function
            CreateObject();
        }
        else if (Input.GetKeyDown(newGameKey))
        {
            // call function
            NewGame();
        }
        else if (Input.GetKeyDown(saveKey))
        {
            // call function from storage
            storage.Save(this);
        }
        else if (Input.GetKeyDown(loadKey))
        {
            // call function from this class then from storage
            NewGame();
            storage.Load(this);
        }
        #endregion
    }
    #region creating and clearing objects in list
    private void CreateObject()
    {
       // Vector3 prevpos;
        PersistableObject o = Instantiate(prefab, prefabParent);
        // random.insideunitsphere makes the cubes spawn randomly within the sphere, 
        // they may jut out but their centrepoint is never outside the sphere
        #region diagram
        //              +5
        //              |
        //              |
        //       -5-----+-----+5
        //              |
        //              |
        //              -5
        #endregion     
       // foreach (var cube in cubes)
      //  {
         //   if (true)
         //   {
                o.transform.localPosition = Random.insideUnitSphere * 5f;
                o.transform.localRotation = Random.rotation;
                o.transform.localScale = Random.Range(01, 1) * Vector3.one;
                cubes.Add(o);
        //    }
         //  prevpos = cube.transform.position;
       // }
       
    }
    private void NewGame()
    {
        // var is a transform
        foreach (var cube in cubes)
        {
            // destroy selected gameobject cube transform is connected to
            Destroy(cube.gameObject);
        }
        cubes.Clear();
    }
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Vector3.zero, 5f);
    }
    #region saving & loading
    public override void Save(GameDataWriter writer)
    {
        writer.Write(cubes.Count);
        foreach (var cube in cubes)
        {
            cube.Save(writer);
        }
    }
    public override void Load(GameDataReader reader)
    {
        int count = reader.ReadInt();
        for (int i = 0; i < count; i++)
        {
            PersistableObject o = Instantiate(prefab,prefabParent);
            o.Load(reader);
            cubes.Add(o);
        }
    }
    #endregion
}

