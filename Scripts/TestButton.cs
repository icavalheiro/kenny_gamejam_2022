using Godot;

public class TestButton : Button
{
    private Label _dynamicLabel;
    private int _counter = 0;

    public override void _Ready()
    {
        _dynamicLabel = GetNode<Label>("../DynamicLabel");
        Connect("pressed", this, nameof(OnBtnPressed));
    }

    private void OnBtnPressed()
    {
        _counter++;
        _dynamicLabel.Text = _counter + "";
    }

}



