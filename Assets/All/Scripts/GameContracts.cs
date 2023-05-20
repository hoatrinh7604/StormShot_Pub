using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContracts : MonoBehaviour
{
    public static bool TEST_MODE = true;

    public static string BASE64_KEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmYofeNIUpVNJO9OVEr6SEEskDkdWOWY5rY1dfRLHXnqBhP2lEatYDOJcE8/5bRrj2qjB2UeeqFbvwNtyDaX6dNjuMlaj4CcalbLqcuP7PxThcp+LecayM01HNwEqSJNCHqo0lEdC9SW0/nV2YGtSKlsujndOHqdSvJJZh9/omfSxSenUK4OJFtNc7HK5qPxldRjesMnvRqapzvV8TIaRFzCDpsPLhpA+GLnwTRxVPg2q9avmFqeG471LHOU9AundC2u5Zd4O1dVmbsCzSVAzkC1d7tUJz61IqAhawiz8BBVM6y2Z28eRMpWXpVqRHx8Ax3shaGAAUwtEY6vcLp9ZrQIDAQAB";
    public static string HASH_KEY = "stormshot";
    #region TAG
    public static string ENEMY_TAG = "Enemy";
    public static string HOSTAGE_TAG = "Hostage";
    public static string PLAYER_TAG = "Player";
    public static string NORMAL_BARRIER_TAG = "NormalFence";
    public static string HARD_BARRIER_TAG = "HardFence";
    public static string BULLET_TAG = "Bullet";
    public static string DAMAGE_TAG = "Damage";
    public static string TRAP_TAG = "Trap";
    public static string GROUND_TAG = "Ground";
    public static string OUTSIDE_TAG = "OutSide";
    public static string CYLINDER_TAG = "Cylinder";
    #endregion

    #region LAYER
    public static string CHARACTER_LAYER = "Character";
    public static string CHARACTER_DEATH_LAYER = "Ragdoll";
    public static string BARRIER_LAYER = "Barrier";
    #endregion

    public static string EXPLOSION_NAME = "Explosion";
    public static string SAW_NAME = "Saw";
    public static string TNT_NAME = "TNT";
}
