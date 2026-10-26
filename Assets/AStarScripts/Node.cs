using UnityEngine;
public class Node
{
   public bool isWalkable;
   public Vector3 worldPosition;

   public Node(bool _isWalkable, Vector3 _worldPosition)
   {
        isWalkable = _isWalkable;
        worldPosition = _worldPosition; 
   }
}
