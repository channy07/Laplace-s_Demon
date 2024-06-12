using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour
{
    public double mass;
    public double Vx;
    public double Vy;

    public Dictionary<int, Vector3> transformTable = new Dictionary<int, Vector3>();
    public Dictionary<int, double> VxTable = new Dictionary<int, double>();
    public Dictionary<int, double> VyTable = new Dictionary<int, double>();
}
