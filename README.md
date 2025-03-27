# DungeonCrawler

Main Scene (Movement and IA), All Enemies are under the AStarEnemies Object.

# Controls
                    Mouse & KeyBoard        Controller
    
    Movement        WASD                    Left Stick
    Aim             Mouse                   Right Stick
    Shoot           Left Click             Right Trigger


# Enemies

Skeleton - Follows the player

OldMage - Stop at a distance and start shooting 

Kamikaze - Start to follow the player when it sees it. When near enough the player start an explosion which takes 1.5 seconds to happen 

Summoner - Summon Tiny kamikaze and reposition between each summon. Will also reposition if it takes a hit 

NewMage - Same as mage but with FSM and a Teleport mechanic when it takes damage

Friendly Summoner - A summoner that spawns additional characters for you (a bug that I had... I thought it was funny)

SummonerCeption - DO NOT USE !!!!!