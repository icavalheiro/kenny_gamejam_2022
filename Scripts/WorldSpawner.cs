using Godot;
public class WorldSpawner : Node
{
    private TextureRect _testTexture;
    private NoiseTexture _noiseTexture;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var random = new System.Random();
        _testTexture = GetNode<TextureRect>("%TestTexture");
        _noiseTexture = new NoiseTexture();
        _noiseTexture.Noise = new OpenSimplexNoise();
        _noiseTexture.Noise.Octaves = 1;
        _noiseTexture.Noise.Period = 65;
        _noiseTexture.Noise.Seed = random.Next(int.MinValue, int.MaxValue);
        _noiseTexture.NoiseOffset = new Vector2(random.Next(-5000, 5000), random.Next(-5000, 5000));
        GD.Print(_noiseTexture.NoiseOffset);
        _noiseTexture.Connect("changed", this, nameof(UpdateTexture));
    }

    private void UpdateTexture()
    {
        _testTexture.Texture = _noiseTexture;
    }
}
