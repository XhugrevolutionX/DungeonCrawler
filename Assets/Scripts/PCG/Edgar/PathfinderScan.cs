using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PathfinderScan", menuName = "PostProcess/PathfinderScan")]
public class PathfinderScan : DungeonGeneratorPostProcessBase
{
    public override void Run(GeneratedLevel level, LevelDescription levelDescription)
    {
        AstarPath.active.Scan();
    }
}
