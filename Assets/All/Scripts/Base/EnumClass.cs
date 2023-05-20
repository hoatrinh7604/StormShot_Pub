public enum CHARACTER
{
    PLAYER = 1,
    HOSTAGE,
    ENEMY
}

public enum ItemObject
{
    WALL = 1,
    BOX,
    WOODEN,
    TNT,
    SAW,
    TRAP,
    IRON_BALL,
    STONE,
    BOMB,
    POISON_BARREL,
    CHAIN
}

public enum Weapons
{
    GUN = 1,
    LONG_GUN,
    BOMB,
    ROCKET
}

public enum Bullets
{
    GUN = 0,
    AK,
    BOMB,
    ROCKET
}

public enum ItemType
{
    AMMO = 1,
    RIFLE
}

public enum AnimState
{
    IDLE = 0,
    WALKING,
    AIMING,
    SHOOTING,
    VICTORY,
    HIT,
    DEATH,
    DEFEAT
}

public enum GameState
{
    RUNNING = 0,
    PAUSE
}

public enum TypeOfEnd
{
    WIN = 0,
    OUT_OF_BULLET,
    YOU_DEAD
}

public enum Barrel
{
    SOFT = 0,
    HARD
}