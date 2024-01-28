using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPlant : MonoBehaviour
{
    public List<carrot> Carrots;
    public List<Tomato> Tomatoes;
    public List<Corn> Corns;
    public Transform target;
    public bool activate=false;
    public int[] AttacksProb=new int[3];
    public int TimeAttack;

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            StartCoroutine(ChooseAttack());
            activate = false;
        }

      

    }


    IEnumerator ChooseAttack()
    {
        while(true)
        {
            yield return new WaitForSeconds(TimeAttack);

            int r = Random.Range(0, 100);
            if (r < AttacksProb[0])
            {
                foreach (carrot car in Carrots)
                {
                    car.Attack(target);
                }
            }
            else if (r < AttacksProb[1])
            {
                foreach(Tomato tom in Tomatoes)
                {
                    tom.Attack(target);
                }
            }
            else if(r<AttacksProb[2])
            {
                r = Random.Range(0, Corns.Count - 1);

                Corns[r].Attack(target);
            }
            else {
                GetComponentInChildren<EggplantSlam>().Slam();
            }


        }
        yield return null;
    }



    
}
