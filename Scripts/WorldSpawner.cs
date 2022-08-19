using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class WorldSpawner : Node
{
    private readonly TilesetModel[] _tilesetModels = Tilesets.AvailableModels;
    private static List<Tile> _spawnNeighboursQueue = new List<Tile>();
    private static Dictionary<string, PackedScene> _tileScenes;
    private static Dictionary<string, TilesetModel> _tilesetModelsHashtable;
    private Random _random = new Random();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("0");
        CreateTilesetModelsHashtable();
        GD.Print("1");
        // Task.Run(SpawnWorld);
        SpawnWorld();
        GD.Print("2");
    }

    private void CreateTilesetModelsHashtable()
    {
        _tilesetModelsHashtable = new Dictionary<string, TilesetModel>();
        foreach (var model in _tilesetModels)
        {
            _tilesetModelsHashtable.Add(model.Name, model);
        }
    }

    private async Task SpawnWorld()
    {
        GD.Print("3");
        await PreloadTileScenes();

        SpawnFirstTile();

        GD.Print("6");

        var a = 7;
        while (_spawnNeighboursQueue.Count > 0)
        {
            GD.Print(a + "");
            a++;

            var tile = _spawnNeighboursQueue[0];
            _spawnNeighboursQueue.Remove(tile);
            SpawnNeighbours(tile);
            await Task.Delay(TimeSpan.FromMilliseconds(500)); //add a delay so we can see the tiles spawning :D
        }

        GD.Print("done");
    }

    private void SpawnFirstTile()
    {
        GD.Print("4");
        var startingTiles = new string[] { "dirt", "grass", "forest" };
        var startingTileName = startingTiles[_random.Next(0, startingTiles.Length)];
        var tileScene = _tileScenes[startingTileName];
        var instance = tileScene.Instance<Tile>();
        instance.Model = _tilesetModelsHashtable[startingTileName];
        AddChild(instance);
        _spawnNeighboursQueue.Add(instance);
        GD.Print("5");
    }

    private void SpawnNeighbours(Tile tile)
    {
        if (tile.Model.Name == "water")
        {
            //we do not spawn neighbours for water :D
            return;
        }

        Tile spawn(float x, float z)
        {
            var selectedNeighbourModel = GetNeighbourModel(tile);
            var scene = _tileScenes[selectedNeighbourModel.Name];
            var tileInstance = scene.Instance<Tile>();
            AddChild(tileInstance);
            tileInstance.Translate(tile.Translation + new Vector3(x, 0, z));
            _spawnNeighboursQueue.Add(tileInstance);
            return tileInstance;
        };

        if (tile.Top == null)
        {
            var newInstance = spawn(Tilesets.TopOffset, 0);
            tile.Top = newInstance;
        }

        tile.Top.Bottom = tile;

        if (tile.TopRight == null)
        {
            var newInstance = spawn(Tilesets.TopOffset * 0.5f, Tilesets.DiagonalOffset);
            tile.TopRight = newInstance;
        }

        tile.TopRight.BottomLeft = tile;
        tile.TopRight.TopLeft = tile.Top;
        tile.Top.BottomRight = tile.TopRight;

        if (tile.BottomRight == null)
        {
            var newInstance = spawn(-Tilesets.TopOffset * 0.5f, Tilesets.DiagonalOffset);
            tile.BottomRight = newInstance;
        }

        tile.BottomRight.TopLeft = tile;
        tile.BottomRight.Top = tile.TopRight;
        tile.TopRight.Bottom = tile.BottomRight;

        if (tile.Bottom == null)
        {
            var newInstance = spawn(-Tilesets.TopOffset, 0);
            tile.Bottom = newInstance;
        }

        tile.Bottom.Top = tile;
        tile.Bottom.TopRight = tile.BottomRight;
        tile.BottomRight.BottomLeft = tile.Bottom;

        if (tile.BottomLeft == null)
        {
            var newInstance = spawn(-Tilesets.TopOffset * 0.5f, -Tilesets.DiagonalOffset);
            tile.BottomLeft = newInstance;
        }

        tile.BottomLeft.BottomRight = tile.Bottom;
        tile.Bottom.TopLeft = tile.BottomLeft;
        tile.BottomLeft.TopRight = tile;

        if (tile.TopLeft == null)
        {
            var newInstance = spawn(Tilesets.TopOffset * 0.5f, -Tilesets.DiagonalOffset);
            tile.TopLeft = newInstance;
        }

        tile.TopLeft.TopRight = tile.Top;
        tile.Top.BottomLeft = tile.TopLeft;
        tile.TopLeft.BottomRight = tile;
        tile.TopLeft.Bottom = tile.BottomLeft;
        tile.BottomLeft.Top = tile.TopLeft;

    }

    private TilesetModel GetNeighbourModel(Tile tile)
    {
        //repeat tile at 50% chance
        if (_random.Next(0, 101) > 50)
        {
            return tile.Model;
        }

        //find a different tile
        float luckyNumber = _random.Next(0, 100);
        float accumulator = 0;
        foreach (var entry in tile.Model.PossibleNeighbours)
        {
            if ((entry.Probability + accumulator) * 100 <= luckyNumber)
            {
                return _tilesetModelsHashtable[entry.Name];
            }

            accumulator += entry.Probability;
        }

        var lastOneName = tile.Model.PossibleNeighbours.Last().Name;
        return _tilesetModelsHashtable[lastOneName];

    }

    private async Task PreloadTileScenes()
    {
        GD.Print("3.1");
        if (_tileScenes != null) return;

        _tileScenes = new Dictionary<string, PackedScene>();
        GD.Print("3.2");

        GD.Print("3.3");
        var tasks = new List<Task<(string name, PackedScene scene)>>();
        foreach (var model in _tilesetModels)
        {
            GD.Print("3.4 " + model.Name);
            var task = LoadScene(model.Name, model.ResourcePath);
            tasks.Add(task);
        }

        GD.Print("3.5");
        var results = await Task.WhenAll(tasks);
        GD.Print("3.6");
        foreach (var result in results)
        {
            GD.Print("3.7 " + result.name);
            _tileScenes.Add(result.name, result.scene);
        }

        GD.Print("3.8");
    }

    private async Task<(string, PackedScene)> LoadScene(string sceneName, string resourcePath)
    {
        PackedScene scene = null;

        await Task.Run(() =>
        {
            scene = GD.Load<PackedScene>(resourcePath);
        });

        return (sceneName, scene);
    }
}
