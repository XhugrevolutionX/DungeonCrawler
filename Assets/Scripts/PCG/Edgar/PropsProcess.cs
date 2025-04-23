using System;
using System.Collections.Generic;
using System.Linq;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "PostProcess/PropsProcess", fileName = "Props")]
public class PropsProcess : DungeonGeneratorPostProcessBase
{
    [SerializeField] private List<GameObject> props;
    [SerializeField] private LayerMask doorsLayer;
    private DoorManager _doorManager;
    Transform _parent;

    private Tilemap _groundTilemap;
    private Tilemap _wallsTilemap;


    private Vector3Int[] _Neighbours = new[]
    {
        new Vector3Int(0, 1, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(-1, 0, 0),
    };

    public override void Run(GeneratedLevel level, LevelDescription levelDescription)
    {
        _doorManager = level.RootGameObject.GetComponent<DoorManager>();
        
        _groundTilemap = level.GetSharedTilemaps()[0];
        _wallsTilemap = level.GetSharedTilemaps()[1];

        _parent = level.RootGameObject.transform.parent.Find("Props");

        for (int i = _parent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(_parent.GetChild(i).gameObject);
        }

        for (int i = _groundTilemap.origin.x; i < _groundTilemap.origin.x + _groundTilemap.size.x; i++)
        {
            for (int j = _groundTilemap.origin.y; j < _groundTilemap.origin.y + _groundTilemap.size.y; j++)
            {
                if (_groundTilemap.HasTile(new Vector3Int(i, j)))
                {
                    if (!_doorManager.HasDoor(new Vector2(i + 0.5f, j + 0.5f)))
                    {
                        foreach (var neighbour in _Neighbours)
                        {
                            if (_wallsTilemap.HasTile(new Vector3Int(i, j) + neighbour))
                            {
                                int rnd = UnityEngine.Random.Range(0, 100) % 3;
                                if (rnd == 0)
                                {
                                    Instantiate(props[UnityEngine.Random.Range(0, props.Count)], new Vector3(i, j), Quaternion.identity, _parent);
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}