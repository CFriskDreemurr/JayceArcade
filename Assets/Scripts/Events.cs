using UnityEngine;
using System;

public class Events : MonoBehaviour
{
    public static Action OnTicketsChange;
    public static Action OnItemBought;
    public static void ChangeTicket()
    {
        OnTicketsChange?.Invoke();
    }

    public static void ItemBought()
    {
        OnItemBought?.Invoke();
    }

}
