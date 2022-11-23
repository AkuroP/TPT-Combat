using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private TileBase tileBase;
    
    [SerializeField] private List<Tilemap> accessibleMap;
    [SerializeField] private Tilemap mm_accessible;

    [SerializeField] private List<Tilemap> unaccessibleMap;
    [SerializeField] private Tilemap mm_unaccessible;

    [SerializeField] private List<Tilemap> interactibleMap;
    [SerializeField] private Tilemap mm_interactible;

    [SerializeField] private List<Tilemap> pathMap;
    [SerializeField] private Tilemap mm_path;
    public MapState mapState;
    public enum MapState
    {
        ACCESSIBLE,
        UNACESSIBLE,
        INTERACTIBLE,
        PATH
    }



    // Start is called before the first frame update
    private void Start()
    {
        //Assign green tile in accessible map
        AssignColorTilesInMap(accessibleMap, ref mm_accessible, tileBase);
        
        //Assign red tile in unaccessible map
        AssignColorTilesInMap(unaccessibleMap, ref mm_unaccessible, tileBase);

        //Assign blue tile in interactible map / object
        AssignColorTilesInMap(interactibleMap, ref mm_interactible, tileBase);

        //Assign brown tile in path
        AssignColorTilesInMap(pathMap, ref mm_path, tileBase);
    }

    private void AssignColorTilesInMap(List<Tilemap> map, ref Tilemap minimap, TileBase tileBase)
    {
        for(int i = 0; i < map.Count; i++)
        {
            foreach(var mapPos in map[i].cellBounds.allPositionsWithin)
            {
                if(!map[i].HasTile(mapPos))continue;
                else minimap.SetTile(mapPos, tileBase);
            }
        }
    }
}
