using Godot;
using System;

public class player : KinematicBody2D
{
    private Vector2 direction;
    public Vector2 Direction
    {
        get { return direction;}
        set { direction = value;}
    }
    public ENTITY_TYPES type { get; set; }
    map grid;

    const float MAX_SPEED = 100;
    float speed;

    Vector2 velocity;

    bool isMoving = false;
    Vector2 targetPos = new Vector2(0,0);
    Vector2 targetDirection = new Vector2(0,0);

    public override void _Ready()
    {
        grid = (map)GetParent();
        type = ENTITY_TYPES.PLAYER;
        SetProcess(true);

    }

    public override void _Process(float delta)
    {
        direction = new Vector2(0,0);
        speed = 0;

        if (Input.IsActionPressed("moveUp"))
        {
            direction.y = -1;
        }
        else if (Input.IsActionPressed("moveDown"))
        {
            direction.y = 1;
        }

         if (Input.IsActionPressed("moveLeft"))
        {
            direction.x = -1;
        }
        else if (Input.IsActionPressed("moveRight"))
        {
            direction.x = 1;
        }
        

        if (!isMoving && direction != new Vector2(0,0))
        {
            targetDirection = direction.Normalized();
            if (grid.IsCellVacant(GetPosition(), targetDirection))
            {
                targetPos = grid.UpdateChildPos(GetPosition(), direction, type);
                isMoving = true;
            }
        }
        else if (isMoving){
            speed = MAX_SPEED;
            velocity = speed * targetDirection * delta;
            

            Vector2 position = GetPosition();
            float distanceToTarget = position.DistanceTo(targetPos);
            float moveDistance = velocity.Length();
            if (moveDistance > distanceToTarget)
            {
                velocity = targetDirection * distanceToTarget;
                isMoving = false;
            }
            MoveAndCollide(velocity);
            Console.WriteLine(velocity+"speed units");
        }
        
    }
}
