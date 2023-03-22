using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enum
{
    public enum BlockType
    {
        WOOD,
        STONE,
        IRON,
        GOLD,
        DIAMOND,
        CUSTOM
    }

    public enum GameState
    {

    }

    public enum GameStateLv2
    {
        SPAWN_RANDOM,
        SPAWN_WAVE1,
        SPAWN_WAVE2,
        SPAWN_WAVE3
    }

    public enum ItemType
    {
        HP1,
        HP2,
        HP3,
        COIN,
        BOMB,
        SHIELD,
        UPGRADE
    }

    public enum SceneIndex
    {
        Level1 = 5,
        Level2 = 7,
        Level3 = 3,
        Level4 = 4
    }
}
