using UnityEngine;

[CreateAssetMenu(fileName = "New Cell Type", menuName = "Cell Type/Create new Cell Type" , order = 51)]
public class CellType : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _strength;
    [SerializeField] private int _spawnDepth;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Sprite _ingotSprite;
    [SerializeField] private AudioClip[] _breakSounds;

    public int GetSpawnDepth()
    {
        return _spawnDepth;
    }
    public int GetStrength()
    {
        return _strength;
    }
    public Sprite GetSprite()
    {
        return _sprite;
    }

    public Sprite GetIngotSprite()
    {
        return _ingotSprite;
    }

    public AudioClip GetRandomBreakSound()
    {
        return _breakSounds[Random.Range(0,_breakSounds.Length)];
    }
}
