﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : PersistableObject
{
    // just the position 
    public ShapeFactory shapeFactory;
    public List<Shape> shapes;
    //public List<Transform> cubepos;
    public PersistentStorage storage;
    const int saveVersion = 1;
    private string savePath;
    // keycode variable, could maybe use this for changeable controls
    public KeyCode createKey = KeyCode.C;
    public KeyCode destroyKey = KeyCode.X;
    public KeyCode newGameKey = KeyCode.N;
    public KeyCode saveKey = KeyCode.S;
    public KeyCode loadKey = KeyCode.L;
    // speeds
    public float CreationSpeed { get; set; }
    public float DestructionSpeed { get; set; }
    float creationProgress,destructionProgress;

    private void Awake()
    {

        shapes = new List<Shape>();
        // cubepos = new List<Transform>();
    }
    private void Update()
    {
        creationProgress += Time.deltaTime * CreationSpeed;
        while (creationProgress >= 1f)
        {
            creationProgress -= 1f;
            CreateObject();
        }
        destructionProgress += Time.deltaTime * DestructionSpeed;
        while (destructionProgress >= 1f)
        {
            destructionProgress -= 1f;
            DestroyShape();
        }
        #region inputs
        if (Input.GetKeyDown(createKey))
        {
            // call function
            CreateObject();
        }
        if (Input.GetKeyDown(destroyKey))
        {
            // call function
            DestroyShape();
        }
        else if (Input.GetKeyDown(newGameKey))
        {
            // call function
            NewGame();
        }
        else if (Input.GetKeyDown(saveKey))
        {         
            // call function from storage
            storage.Save(this,saveVersion);
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
        o.SetColor(Random.ColorHSV(0f, 1f, 0.5f, 1f, .25f, 1f, 1f, 1f), Random.ColorHSV(0f, 1f, 0.5f, 1f, .25f, 1f, 1f, 1f));
       
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
    void DestroyShape()
    {
        // remove random shape
        int index = Random.Range(0, shapes.Count);
        Destroy(shapes[index].gameObject);
        // swap deleted element's slot with the last element to leave the gap at the end of the list
        int lastIndex = shapes.Count - 1;
        shapes[index] = shapes[lastIndex];
        shapes.RemoveAt(lastIndex);
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
        writer.Write(shapes.Count);

        for (int i = 0; i < shapes.Count; i++)
        {
            writer.Write(shapes[i].ShapeId);
            writer.Write(shapes[i].MaterialId);
            shapes[i].Save(writer);
        }
    }
    public override void Load(GameDataReader reader)
    {
        // load each cube
        int version = reader.Version;
        if (version > saveVersion)
        {
            Debug.LogError("Unsupported future save version " + version);
            return;
        }
        int count = version <= 0 ? -version : reader.ReadInt();
        for (int i = 0; i < count; i++)
        {
            // get id
            int shapeId = version > 0 ? reader.ReadInt() : 0;
            // get material
            int materialId = version > 0 ? reader.ReadInt() : 0;
            // instantiate shapes again
            Shape o = shapeFactory.Get(shapeId, materialId);
            o.Load(reader);
            shapes.Add(o);
            // shapes.Add(o.transform);
        }
    }
    #endregion
}

