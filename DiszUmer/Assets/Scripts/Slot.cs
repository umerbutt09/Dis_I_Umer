using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool Once;
    public bool Twice;
    public int Value;
    
    
    public bool CheckSlot()
    {/////////////////////////////////////////////////////////////////////////////////////////
        if (!Once)////////////////////////////////////////////////////////SLOTS FUNCTION//////
        {/////////////////////////////////////////////////////////////////////////////////////
            Once = true;
            return false;
        }
        else if (!Twice)
        {
            Twice = true;
            return true;
        }//////////////////////////////////////////////////////////////////////UMER_JAMIL_BUTT
        return true;
    }
    
    public void AssignValue (int _Value)
    {
        Value = _Value;
    }

    public int ReadValue ()
    {
        return Value;
    }
}
