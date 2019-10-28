using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : PersistableObject
{
    // just the position 
    public ShapeFactory shapeFactory;
    public Transform prefabParent;
    public List<Shape> shapes;
   //public List<Transform> cubepos;
    public PersistentStorage storage;
    const int saveVersion = 1;
    private string savePath;
    // keycode variable, could maybe use this for changeable controls
    public KeyCode createKey = KeyCode.C;
    public KeyCode newGameKey = KeyCode.N;
    public KeyCode saveKey = KeyCode.S;
    public KeyCode loadKey = KeyCode.L;
    private void Awake()
    {
        shapes = new List<Shape>();
       // cubepos = new List<Transform>();
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
      // o is an instance (instantiated object)
        Shape o = shapeFactory.GetRandom();
        // random.insideunitsphere makes the shapes spawn randomly within the sphere, 
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
        Transform t = o.transform;
       t.localPosition = Random.insideUnitSphere * 5f;
        t.transform.localRotation = Random.rotation;
        t.transform.localScale = Random.Range(0.1f, 1f) * Vector3.one;
        shapes.Add(o);
        //cubepos.Add(o.transform);
        //for (int i = 0; i < 20; i++)
        //{
        //    bool overlapping = false;
        //    foreach (var cubeposition in cubepos)
        //    {
        //        if (Vector3.Distance(o.transform.position, cubeposition.transform.position) < 5f)
        //        {
        //            overlapping = true;
        //            break;
        //        }
        //    }
        //    if (overlapping) {
        //        Debug.Log("new pos for this boy");
        //        o.transform.localPosition = Random.insideUnitSphere * 5f;             
        //    } else {
        //        break;
        //    }
        //}

    }
    private void NewGame()
    {
        // var is a transform
        foreach (var cube in shapes)
        {
            // destroy selected gameobject cube transform is connected to
            Destroy(cube.gameObject);
        }
        shapes.Clear();
        //cubepos.Clear();
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
        // save each cube
        writer.Write(-saveVersion);
        writer.Write(shapes.Count);
        foreach (var cube in shapes)
        {
            cube.Save(writer);
        }
    }
    public override void Load(GameDataReader reader)
    {
        // load each cube
        int version = reader.ReadInt();
        int count = version <= 0 ? -version : reader.ReadInt();
        for (int i = 0; i < count; i++)
        {
            // instantiate shapes again
            Shape o = shapeFactory.Get(0);
            o.Load(reader);
            shapes.Add(o);
           // shapes.Add(o.transform);
        }
    }
    #endregion
}

