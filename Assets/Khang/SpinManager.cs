using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpinManager : MonoBehaviour
{
    public static SpinManager _ins;

    [SerializeField] private List<List<ItemType>> listItems = new List<List<ItemType>>();
    [SerializeField] private int col;
    [SerializeField] private int row;


    [SerializeField] private List<Slot> slots;
    public bool finishRolling;

    [SerializeField] private List<Ways> ways;

    private void Awake()
    {
        _ins = this;
    }

    private void Start()
    {
        InitTwoDimensions();
        StartCoroutine(Rolling());
        finishRolling = false;


    }

    public IEnumerator Rolling()
    {
        finishRolling = false;
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Rolling();
        }
        yield return new WaitUntil(() => slots.All(slot => !slot.isRolling));
        finishRolling = true;
        yield return new WaitForSeconds(0.5f);
        CheckSuccess();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PrintTwoDImensions();
        }
    }

    private void InitTwoDimensions()
    {
        for (int i = 0; i < this.row; i++)
        {
            List<ItemType> row = new List<ItemType>();
            for (int j = 0; j < this.col; j++)
            {
                row.Add(ItemType.None);
            }
            listItems.Add(row);
        }
    }

    private void PrintTwoDImensions()
    {
        for (int i = 0; i < this.row; i++)
        {
            for (int j = 0; j < this.col; j++)
            {
                Debug.Log(listItems[i][j]);
            }
        }
    }

    public void SetElementInTwoDimensions(int rowIdx, int colIdx, ItemType itemType)
    {
        if (listItems[rowIdx][colIdx] != null)
        {
            listItems[rowIdx][colIdx] = itemType;
        }
    }

    public void CheckSuccess()
    {
        ItemType itemType;
        bool isMatch;
        List<ItemType> listItemsType = new List<ItemType>();

        for (int i = 0; i < ways.Count; i++)
        {
            var wayCheck = ways[i].wayCheck;
            listItemsType.Clear();
            for (int j = 0; j < col - 1; j++)
            {
                Debug.Log($"row: {(int)wayCheck[j]}, col: {j}, item 1: {listItems[(int)wayCheck[j]][j]}, item 2: {listItems[(int)wayCheck[j + 1]][j + 1]}");
                itemType = listItems[(int)wayCheck[j]][j];

                if (listItems[(int)wayCheck[j]][j] == listItems[(int)wayCheck[j + 1]][j + 1])
                {
                    isMatch = true;
                    listItemsType.Add(listItems[(int)wayCheck[j]][j]);
                }
                else break;

                if (isMatch && j + 1 == col - 1)
                {
                    listItemsType.Add(listItems[(int)wayCheck[j + 1]][j + 1]);

                    string bingoRow = string.Join(", ", listItemsType);
                    Debug.Log($"🎉 Bingo Row: [{bingoRow}]");

                    Debug.Log("----------------------------------------");
                }
            }
        }

    }

}