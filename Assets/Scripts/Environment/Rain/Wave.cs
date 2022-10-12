using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

    public GameObject Umbrella;
    public GameObject UmbrellaHandle;
    public List<PaperBridgeHandle> _paperBrigdeList = new List<PaperBridgeHandle>();
    protected string respwanEvnetName = "waveDeathCallback";
    protected bool _stopDestoryPaperBridge;

    void Start()
    {
        EventMgr.GetInstance().AddLinstener<string>("StopDestoryPaperBridge", (info) =>
         {
             _stopDestoryPaperBridge = true;
         });
        EventMgr.GetInstance().AddLinstener<Collider2D>(respwanEvnetName, WaveDeathCallback);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Health health = collision.GetComponent<Health>();
            health.Damage(health.MaximumHealth,Health.DeathStyle.DeathwithoutFlower,()=>
            {
                EventMgr.GetInstance().EventTrigger<Collider2D>(respwanEvnetName, collision);
            });
            PlayerController playerController = collision.GetComponent<PlayerController>();
            playerController.GravityActive(false);
            playerController.SetForce(Vector2.zero);
            Debug.Log("重力取消");
        }
    }


    /// <summary>
    /// 在海浪中死了后复活的回调
    /// </summary>
    /// <param name="collider"></param>
    public void WaveDeathCallback(Collider2D collider)
    {
        PlayerController playerController = collider.GetComponent<PlayerController>();
        playerController.GravityActive(true);
        Debug.Log("重力应用");
        
        if (!_stopDestoryPaperBridge)
        {
            foreach (PaperBridgeHandle ph in _paperBrigdeList)
            {
                ph.ResetBrigde();
            }
            Umbrella.GetComponent<PathMovement>().ResetPath();
            UmbrellaHandle.GetComponent<UmbrellaHandle>().ResetHandleState();
        }
    }


}
