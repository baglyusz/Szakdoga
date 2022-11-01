using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class NodeLinkData : MonoBehaviour
{
    public string baseNodeGuid;
    public string portName;
    public string targetNodeGuid;
}