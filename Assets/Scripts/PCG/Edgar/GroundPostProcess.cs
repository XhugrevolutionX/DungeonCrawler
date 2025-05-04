using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using UnityEngine;

[CreateAssetMenu(fileName = "GroundPostProcess", menuName = "PostProcess/GroundPostProcess")]
public class GroundPostProcess : DungeonGeneratorPostProcessBase
{
    public override void Run(GeneratedLevel level, LevelDescription levelDescription)
    {
        level.RootGameObject.GetComponentInChildren<ObjectsRef>().GroundTilemap = level.GetSharedTilemaps()[0];
    }
}
