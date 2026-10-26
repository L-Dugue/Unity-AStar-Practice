using UnityEngine;

public class Grid : MonoBehaviour
{
   public LayerMask unWalkableMask;  
   public Vector2 gridWorldSize; // the size of the grid.
   public float nodeRadius; // How much space EACH node covers.
   Node[,] grid; // the 2D grid, represented with an Array.


    
    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;

        // How large the grid is, within the Node[,] array, in relation to the node sizes.
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }


    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY]; // Create the 2D grid, size related to the nodeDiamater 

        // Bottom-left corner of our scene
        Vector3 worldBottomLeft = transform.position - (Vector3.right * gridWorldSize.x / 2) - (Vector3.forward * gridWorldSize.y / 2);
        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {                                         // Going one node size to the right until bounds.   // Going one node size to the up until bounds.
                Vector3 worldPoint = worldBottomLeft + (Vector3.right * (x * nodeDiameter + nodeRadius)) + (Vector3.forward * (y * nodeDiameter + nodeRadius)); 
                bool walkableNode = !(Physics.CheckSphere(worldPoint, nodeRadius, unWalkableMask));

                grid[x, y] = new Node(walkableNode, worldPoint);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        // Puts it in a decimal.
        float rangeX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float rangeY = (worldPos.z + gridWorldSize.y / 2) / gridWorldSize.y; 

        rangeX = Mathf.Clamp01(rangeX);
        rangeY = Mathf.Clamp01(rangeY); 

        int xCoord = Mathf.RoundToInt((gridSizeX - 1) * rangeX);
        int yCoord = Mathf.RoundToInt((gridSizeY - 1) * rangeY);

        return grid[xCoord, yCoord];
    }

   void OnDrawGizmos()
   {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if(grid != null)
        {
            foreach(Node node in grid)
            {
                Gizmos.color = node.isWalkable ? Color.white : Color.red; // Make walkable areas white and non-walkable red.
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - 0.1f)); // Make a gizmo cube, of the size of the circle.
            }
        }
   }

}
