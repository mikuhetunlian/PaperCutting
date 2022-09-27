using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ����㼶��صľ�̬���ݸ����������
/// </summary>
public static class LayerMgr 
{
    //layer
    public static int PlatformsLayer = 6;
    public static int OneWayPlatformLayer = 7;
    public static int LadderLayer = 8;
    public static int BackgroundLayer = 9;
    public static int PushableLayer = 10;

    //layerMask
    public static int PlatformLayerMask = 1 << PlatformsLayer;
    public static int OneWayPlatformLayerMask = 1 << OneWayPlatformLayer;
    public static int LadderLayerMask = 1 << LadderLayer;
    public static int PushableLayerMask = 1 << PushableLayer;

    //���Ա����߼���赲��layerMask
    public static int ObstaclesLayerMask = PlatformLayerMask | OneWayPlatformLayerMask;

}
