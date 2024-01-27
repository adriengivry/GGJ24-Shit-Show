using UnityEngine;

public enum EMovementDirection
{
    None,
    Left,
    Right,
    Up,
    Down
}

public static class MovementUtils
{
    public static EMovementDirection DirectionVectorToEnum(Vector2 direction)
    {
        if (direction.x < 0) return EMovementDirection.Left;
        if (direction.x > 0) return EMovementDirection.Right;
        if (direction.y < 0) return EMovementDirection.Down;
        if (direction.y > 0) return EMovementDirection.Up;

        return EMovementDirection.None;
    }

    public static Vector2 DirectionEnumToVector(EMovementDirection direction)
    {
        switch (direction)
        {
            case EMovementDirection.Left: return Vector2.left;
            case EMovementDirection.Right: return Vector2.right;
            case EMovementDirection.Down: return Vector2.down;
            case EMovementDirection.Up: return Vector2.up;
        }

        return Vector2.zero;
    }

    public static bool AreOppositeDirections(EMovementDirection a, EMovementDirection b)
    {
        return
            (a == EMovementDirection.Left && b == EMovementDirection.Right) ||
            (a == EMovementDirection.Right && b == EMovementDirection.Left) ||
            (a == EMovementDirection.Up && b == EMovementDirection.Down) ||
            (a == EMovementDirection.Down && b == EMovementDirection.Up);
    }
}