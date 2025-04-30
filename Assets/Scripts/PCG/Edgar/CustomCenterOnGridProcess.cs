using System;
using System.Collections.Generic;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using ProceduralLevelGenerator.Unity.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "CustomCenterOnGrid", menuName = "PostProcess/CustomCenterOnGrid")]
public class CustomCenterOnGridProcess : DungeonGeneratorPostProcessBase
{
    public override void Run(GeneratedLevel level, LevelDescription levelDescription)
    {
        var center = GetTilemapsCenter(level.GetSharedTilemaps(), false);

        foreach (Transform transform in level.RootGameObject.transform)
        {
            transform.position -= center;
        }
    }
    
    public static Vector3 GetTilemapsCenter(List<Tilemap> tilemaps, bool compressBounds = false)
    {
        var minX = int.MaxValue;
        var maxX = int.MinValue;
        var minY = int.MaxValue;
        var maxY = int.MinValue;
    
        foreach (var tilemap in tilemaps)
        {
            if (compressBounds)
            {
                tilemap.CompressBounds();
            }

            var cellBounds = tilemap.cellBounds;

            if (cellBounds.size.x + cellBounds.size.y == 0)
            {
                continue;
            }

            minX = Math.Min(minX, cellBounds.xMin);
            maxX = Math.Max(maxX, cellBounds.xMax);
            minY = Math.Min(minY, cellBounds.yMin);
            maxY = Math.Max(maxY, cellBounds.yMax);
        }

        var offset = new Vector3((maxX + minX) / 2f, (maxY + minY) / 2f);

        var grid = tilemaps[0].layoutGrid;

        if (grid != null)
        {
            offset = grid.GetCellCenterLocal((offset * 100).RoundToUnityIntVector3()) / 100;
        }

        return offset;
    }
}
