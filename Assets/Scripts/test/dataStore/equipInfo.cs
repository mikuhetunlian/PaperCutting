using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipInfo
{
    public int id;
    public int count;

    public equipInfo()
    {
        this.id = default(int);
        this.count = default(int);
    }

    public equipInfo(int id,int count)
    {
        this.id = id;
        this.count = count;
    }

}
