using System.Collections;
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
    public int MaterialId { get; private set; }
    Color color;
    Color color2;
    MeshRenderer meshRenderer;
    static int colorPropertyID = Shader.PropertyToID("_Color");
    static int colorPropertyID2 = Shader.PropertyToID("_Color2");
    static MaterialPropertyBlock sharedPropertyBlock;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void SetColor(Color color, Color color2)
    {
        // setting a material's color makes a new material, to avoid this we use a materialpropertyblock
        this.color = color;
        this.color2 = color2;
        if (sharedPropertyBlock == null)
        {
            sharedPropertyBlock = new MaterialPropertyBlock();
        }  
        sharedPropertyBlock.SetColor(colorPropertyID, color);
        sharedPropertyBlock.SetColor(colorPropertyID2, color2);
        meshRenderer.SetPropertyBlock(sharedPropertyBlock);
        

    }
    public void SetMaterial(Material material, int materialId)
    {
        meshRenderer.material = material;
        MaterialId = materialId;
    }
    public override void Save(GameDataWriter writer)
    {
        base.Save(writer);
        writer.Write(color);
    }

    public override void Load(GameDataReader reader)
    {
        base.Load(reader);
        SetColor(reader.Version > 0 ? reader.ReadColor() : Color.white,reader.Version > 0 ? reader.ReadColor() : Color.white);
       
    }
}
