using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Nohara : Base
{
    public Score score;
    // Start is called before the first frame update
    protected override void OnStart()
    {
        Work.gameScore = score;
    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        if(GetPhaseTime() == 100)
        {
            Exit();
        }
    }
}
