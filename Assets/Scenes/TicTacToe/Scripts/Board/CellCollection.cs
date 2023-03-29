using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCollection : MonoBehaviour
{
    [SerializeField] private List<GameObject> collection;

    public List<GameObject> Collection
    {
        get
        {
            return collection;
        }
        private set
        {
            collection = value;
        }
    }

    public string Datagram()
    {
        string datagram = string.Empty;

        foreach( GameObject obj in collection )
        {
            CellController cell = obj.GetComponent<CellController>();

            switch (cell.State)
            {
                case CellController.CellStates.Cross:
                    {
                        datagram += "x";
                    } break;
                case CellController.CellStates.Circle:
                    {
                        datagram += "o";
                    }
                    break;
                default:
                    {
                        datagram += ".";
                    }
                    break;
            }
        }

        return datagram;
    }
}
