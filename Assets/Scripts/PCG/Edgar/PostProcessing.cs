using System;
using System.Collections.Generic;
using System.Linq;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "Dungeon generator/Examples/ApplyingTags", fileName = "Tags")]
public class PostProcessing : DungeonGeneratorPostProcessBase
{
    [Serializable]
    private struct NameToTag
    {
        public string _name;
        public string _tag;
    }
    
    [SerializeField] private List<NameToTag> _tags = new List<NameToTag>();
    public override void Run(GeneratedLevel level, LevelDescription levelDescription)
    {
        foreach (var n in _tags)
        {
            Tilemap TilemapFound = level.GetSharedTilemaps().Find(t => t.name == n._name);
            if (TilemapFound != null)
            {
                TilemapFound.tag = n._tag;
                if (TilemapFound.name == "Walls")
                {
                    TilemapFound.gameObject.layer = LayerMask.NameToLayer("AIDetectable");
                    TilemapFound.GetComponent<TilemapCollider2D>().compositeOperation = Collider2D.CompositeOperation.None;
                }
            }
        }
    }
}
