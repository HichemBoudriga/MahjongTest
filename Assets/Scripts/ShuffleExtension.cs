using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShuffleExtension 
{
    public static void Shuffle<T> (this List<T> list , int shuffleAccuracy)
    {
        for(int i=0; i<shuffleAccuracy;i++)
        {
            int randomIndex = Random.Range(1, list.Count);

            T temp = list[randomIndex];
            list[randomIndex] = list[0];


            list[0] = temp;
        }
    }
   
}
