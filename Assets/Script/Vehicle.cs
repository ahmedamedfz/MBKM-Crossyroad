using UnityEngine;
using DG.Tweening;


public class Vehicle : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float VehicleDestroyedTime = 0;
    
    int extent;
    protected bool isVehicleDestroyed = false;
    private void Update(){
         if (isVehicleDestroyed == true)
        {
            if (VehicleDestroyedTime> 2.0f)
            {
                Destroy(this.gameObject);
            }
            VehicleDestroyedTime += Time.deltaTime;
            return;
        }
       
            Geser();
            if(this.transform.position.x < -(extent+1) || this.transform.position.x>extent + 1)
            {
                Destroy(this.gameObject);
            }
    }
    private void FixedUpdate()
    {
        
    }
    public void SetUp(int extent)
    {
        this.extent =extent; 
        
    }
    public void Geser()
    {
        float speedacak = Random.Range(1.0f,5.0f);
        transform.Translate(Vector3.forward*Time.deltaTime*speed*speedacak);
    }

    public void Stop()
    {
        isVehicleDestroyed = true;
    }
}
