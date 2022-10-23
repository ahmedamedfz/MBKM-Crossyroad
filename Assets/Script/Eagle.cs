using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
   [SerializeField] private float speed = 1;
   [SerializeField] AudioSource suarameledak;
   Player Player;
   void Update()
   {
        if (this.transform.position.z<=Player.CurrentTravel-20)
            return;
        transform.Translate(Vector3.down*Time.deltaTime*speed);
        if (this.transform.position.z <= Player.CurrentTravel && Player.gameObject.activeInHierarchy)
        {
            Player.transform.SetParent(this.transform);
        }
   }
   public void SetUpTarget(Player target)
   {
    this.Player = target;
   }
}
