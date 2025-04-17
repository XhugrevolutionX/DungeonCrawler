using System;
using System.Collections.Generic;
using System.Linq;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Generators.Common.Rooms;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "Dungeon generator/Examples/PlacingCharacter", fileName = "Tags")]
public class SpawnProcess : DungeonGeneratorPostProcessBase
{
    private GameObject _player;
    public override void Run(GeneratedLevel level, LevelDescription levelDescription)
    {

        _player = GameObject.FindGameObjectWithTag("Player");

        List<RoomInstance> Rooms = level.GetRoomInstances();
        
        foreach (RoomInstance room in Rooms)
            Debug.Log(room.RoomTemplateInstance.transform.position + room.RoomTemplateInstance.name);
        
        if (Rooms[0].RoomTemplateInstance != null)
        {
            _player.transform.position = Rooms[0].RoomTemplateInstance.transform.GetChild(1).position;
        }
    }
}
