using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GameDataWriter
{
    #region writing
    private BinaryWriter writer;
    public GameDataWriter(BinaryWriter writer)
    {
        this.writer = writer;
    }
    // you can have two functions with the same name as long as the parameters are a different type
    public void Write(float value)
    {
        writer.Write(value);
    }
    public void Write(int value)
    {
        writer.Write(value);
    }
    // write rotation
    public void Write(Quaternion value)
    {
        writer.Write(value.x);
        writer.Write(value.y);
        writer.Write(value.z);
        writer.Write(value.w);
    }
    // write v3 (used for position and size)
    public void Write(Vector3 value)
    {
        writer.Write(value.x);
        writer.Write(value.y);
        writer.Write(value.z);
    }
    public void Write(Color value)
    {
        writer.Write(value.r);
        writer.Write(value.g);
        writer.Write(value.b);
        writer.Write(value.a);
    }
    #endregion  
}

public class GameDataReader
{
    #region reading
    private BinaryReader reader;
    public GameDataReader(BinaryReader reader)
    {
        this.reader = reader;
    }
    // cannot have the same name as there are no parameters
    public float ReadFloat()
    {
        return reader.ReadSingle();
    }
    public int ReadInt()
    {
        return reader.ReadInt32();
    }
    // read rotation
    public Quaternion ReadQuarternion()
    {
        Quaternion value;
        value.x = reader.ReadSingle();
        value.y = reader.ReadSingle();
        value.z = reader.ReadSingle();
        value.w = reader.ReadSingle();

        return value;
    }
    // read v3
    public Vector3 ReadVector3()
    {
        Vector3 value;
        value.x = reader.ReadSingle();
        value.y = reader.ReadSingle();
        value.z = reader.ReadSingle();
        return value;
    }
    public Color ReadColor()
    {
        Color value;
        value.r = reader.ReadSingle();
        value.g = reader.ReadSingle();
        value.b = reader.ReadSingle();
        value.a = reader.ReadSingle();
        return value;
    }
    #endregion
}
