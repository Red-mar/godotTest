using Godot;
using System;

public enum ENTITY_TYPES
{
    PLAYER, OBSTACLE, COLLECTIBLE
}

public class map : TileMap
{
    Label lblPlayer;
    Vector2 tileSize;
    Vector2 halfTileSize;
    public Vector2 gridSize { get; set; }
    public int?[,] grid {get; set;}
    Random random;


    public override void _Ready()
    {
        PackedScene obstacle = (PackedScene)ResourceLoader.Load("res://Obstacle.tscn");
        PackedScene player = (PackedScene)ResourceLoader.Load("res://player.tscn");

        tileSize = this.GetCellSize();
        halfTileSize = tileSize / 2;
        gridSize = new Vector2(16,16);
        grid = new int?[(int)gridSize.x,(int)gridSize.y];
        random = new Random();

        player Player = (player)player.Instance();
        Player.SetPosition(MapToWorld(new Vector2(4,4)) + halfTileSize);
        AddChild(Player);

        Vector2[] position = new Vector2[5];
        
        for (int i = 0; i < 5; i++)
        {
            Vector2 gridPos = new Vector2(random.Next() % (int)gridSize.x, random.Next() % (int)gridSize.y);
            if (!Array.Exists(position, element => element == gridPos))
            {
                position[i] = gridPos;
            } else {
                i--;
            }
        }

        int obstAmount = 0;

        foreach (var pos in position)
        {
            KinematicBody2D newObstacle = (KinematicBody2D)obstacle.Instance();
            newObstacle.SetPosition(MapToWorld(pos)+halfTileSize);
            grid[(int)pos.x,(int)pos.y] = (int)ENTITY_TYPES.OBSTACLE;
            AddChild(newObstacle);
            obstAmount++;
        }


        SetProcess(true);
        // Called every time the node is added to the scene.
        // Initialization here

    }

    public bool IsCellVacant(Vector2 position, Vector2 direction){
        Vector2 gridPos = WorldToMap(position) + direction;

        if (gridPos.x < gridSize.x && gridPos.x >= 0)
        {
            if (gridPos.y < gridSize.y && gridPos.y >= 0)
            {
                return grid[(int)gridPos.x,(int)gridPos.y] == null ? true : false;
            }
        }
        return false;
    }

    public Vector2 UpdateChildPos(Vector2 newPosition, Vector2 direction, ENTITY_TYPES type){
        Vector2 gridPos = WorldToMap(newPosition);

        grid[(int)gridPos.x,(int)gridPos.y] = null;

        gridVisual visual = (gridVisual)GetNode("gridVisual");
        visual.Update();

        Vector2 newGridPos = gridPos + direction;
        grid[(int)newGridPos.x,(int)newGridPos.y] = (int)type;

        Vector2 targetPos = MapToWorld(newGridPos) + halfTileSize;
        return targetPos;
    }

    public override void _Process(float delta)
    {
    }
}
