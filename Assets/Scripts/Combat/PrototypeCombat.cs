using UnityEngine;
namespace EthiopiaLegends.Combat {
 public class PrototypeCombat:MonoBehaviour {
  public float reach=2.5f;public int damage=25;public float cooldown=.65f;float nextAttack;
  public void Attack(){if(Time.time<nextAttack)return;nextAttack=Time.time+cooldown;
   foreach(var enemy in FindObjectsByType<PrototypeEnemy>(FindObjectsSortMode.None)){
    Vector3 offset=enemy.transform.position-transform.position;
    if(offset.magnitude<=reach&&Vector3.Dot(transform.forward,offset.normalized)>.2f){enemy.TakeDamage(damage);break;}}
  }
 }
 public class PrototypeEnemy:MonoBehaviour {
  public int health=75;public float chaseRange=7f,speed=1.3f;public Transform player;
  void Update(){if(!player)return;Vector3 delta=player.position-transform.position;delta.y=0;
   if(delta.magnitude<chaseRange&&delta.magnitude>1.5f){transform.position+=delta.normalized*speed*Time.deltaTime;transform.rotation=Quaternion.LookRotation(delta);}}
  public void TakeDamage(int amount){health-=amount;if(health<=0)Destroy(gameObject);}
 }
}