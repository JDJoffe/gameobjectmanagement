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
    public void Write(Quaternion value)
    {
        writer.Write(value.x);
        writer.Write(value.y);
        writer.Write(value.z);
        writer.Write(value.w);
    }
    public void Write(Vector3 value)
    {
        writer.Write(value.x);
        writer.Write(value.y);
        writer.Write(value.z);
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
    public Quaternion ReadQuarternion()
    {
        Quaternion value;
        value.x = reader.ReadSingle();
        value.y = reader.ReadSingle();
        value.z = reader.ReadSingle();
        value.w = reader.ReadSingle();

        return value;
    }
    public Vector3 ReadVector3()
    {
        Vector3 value;
        value.x = reader.ReadSingle();
        value.y = reader.ReadSingle();
        value.z = reader.ReadSingle();
        return value;
    }
    #endregion
}
