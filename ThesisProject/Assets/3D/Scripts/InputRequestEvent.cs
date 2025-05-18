using UnityEngine;

public struct InputRequestEvent 
{
    public RelativeDirection direction;
    public InputRequestEvent(RelativeDirection dir) => direction = dir;
}
