using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainElementCaracteristics", menuName = "Scriptable Objects/TerrainElementCaracteristics")]
public class TerrainElementCaracteristics : ScriptableObject
{
    public ElementType Type;
    public ElementMaterial Material;
    public enum ElementType{
        TROU,
        MUR,
        DRAPEAU
    }

    public enum ElementMaterial
    {
        BASE
    }



}
