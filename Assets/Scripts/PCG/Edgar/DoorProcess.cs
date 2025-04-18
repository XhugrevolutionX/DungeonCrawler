
using System.Collections.Generic;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using UnityEngine;

[CreateAssetMenu(menuName = "PostProcess/DoorProcess", fileName = "Doors")]
public class DoorProcess : DungeonGeneratorPostProcessBase
{
    
    private List<GameObject> doors = new List<GameObject>();
    public override void Run(GeneratedLevel level, LevelDescription levelDescription)
    {
        
        doors.Clear();
        
        foreach (var room in level.GetRoomInstances())
        {
            if (room.RoomTemplateInstance.name.Contains("Corridor"))
            {
               for (int i = 0; i < room.RoomTemplateInstance.transform.childCount; i++)
               {
                    if (room.RoomTemplateInstance.transform.GetChild(i).CompareTag("Door"))
                    {
                        doors.Add(room.RoomTemplateInstance.transform.GetChild(i).gameObject); 
                    }
               }
            }
        }
        
        level.RootGameObject.GetComponent<DoorManager>().SetDoors(doors);
    }
}
