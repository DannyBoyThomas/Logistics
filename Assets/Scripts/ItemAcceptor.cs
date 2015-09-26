using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public interface ItemAcceptor
{
    bool CanAccept(Item item, int x, int y);

    bool AcceptItem(GameObject go);
}
