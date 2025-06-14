using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Utils 
{
    // Big 인티저를 이용해서 가져오는 포맷값
    public static string FormatBigInteger(BigInteger number)
    {
        if (number < 1000) return number.ToString();

        string[] suffixes = { "", "K", "M", "B", "T", "Q" };
        int i = 0;

        while (number >= 1000 && i < suffixes.Length - 1)
        {
            number /= 1000;
            i++;
        }

        return number.ToString() + suffixes[i];
    }

}
