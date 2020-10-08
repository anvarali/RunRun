using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public Transform lastTile;
    public List<GameObject> tiles;
    private void Start()
    {
        BoundaryManager.onFinishTile += AddNewTile;
    }
   
    void AddNewTile()
    {
        var newPos = new Vector3(lastTile.position.x, lastTile.position.y, lastTile.position.z + 10);
        lastTile = Instantiate(tiles[Random.Range(0,tiles.Count)],transform).transform;
        lastTile.position = newPos;
    }

    private void OnDestroy()
    {
        BoundaryManager.onFinishTile -= AddNewTile;
    }
}
