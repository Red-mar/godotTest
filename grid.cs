using Godot;
using System;

public class grid : Control
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
    Vector2 lineFrom;
    Vector2 lineTo;

    public override void _Ready()
    {
        lineFrom = new Vector2(0, 0);
        lineTo = new Vector2(3200,0);
    }

    public override void _Draw()
    {
        for (int i = 0; i < 320000; i += 32)
        {
            lineFrom.y = i;
            lineTo.y = i;
            DrawLine(lineFrom, lineTo, Color.Color8(0, 0, 0, 255), 1, false);
            
        }

    }

    //    public override void _Process(float delta)
    //    {
    //        // Called every frame. Delta is time since last frame.
    //        // Update game logic here.
    //        
    //    }
}
