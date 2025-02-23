using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    public int rowIdx;
    public int colIdx;


    private void OnTriggerStay2D(Collider2D collision)
    {
        var item = collision.GetComponent<Item>();
        if (item != null && SpinManager._ins.finishRolling)
        {
            SpinManager._ins.SetElementInTwoDimensions(rowIdx, colIdx, item.GetItemType());
        }
    }
}
