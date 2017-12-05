using Godot;
using System;

public class gridVisual : Node2D
{
    map grid;
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        grid = (map)GetParent();
        
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

    public override void _Draw(){
        Color lineColor = Color.Color8(0,0,0,128);
        int lineWidth = 2;
        Vector2 windowSize = OS.GetWindowSize();

        for (int x = 0; x < grid.gridSize.x+1; x++)
        {
            var colPos = x * grid.GetCellSize().x;
            var limit = grid.gridSize.y * grid.GetCellSize().y;
            DrawLine(new Vector2(colPos, 0), new Vector2(colPos, limit), lineColor, lineWidth);
        }

        for (int y = 0; y < grid.gridSize.y+1; y++)
        {
            var rowPos = y * grid.GetCellSize().y;
            var limit = grid.gridSize.y * grid.GetCellSize().y;
            DrawLine(new Vector2(0, rowPos), new Vector2(limit, rowPos), lineColor, lineWidth);
        }

        for (int x = 0; x < grid.gridSize.x; x++)
        {
            for (int y = 0; y < grid.gridSize.y; y++)
            {
                int? i = grid.grid[x,y];
                if (i == null)
                {
                    DrawRect(new Rect2(grid.MapToWorld(new Vector2(x,y)), grid.gridSize), lineColor);
                }
            }
        }
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
