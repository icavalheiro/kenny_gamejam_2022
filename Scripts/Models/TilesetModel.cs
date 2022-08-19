using System;

[Serializable]
public struct TilesetModel
{
    [Serializable]
    public struct PossibleNeighbour
    {
        public string Name;
        public float Probability;
    }

    public string ResourcePath;
    public string Name;
    public PossibleNeighbour[] PossibleNeighbours;
}