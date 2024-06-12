using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CalculateControlloer : MonoBehaviour
{
    public double calculateTime;
    public double interval;

    private bool isCalculateCompleted = false;
    private double progressTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Calculate Started");
        StartCoroutine(CALCULATE(calculateTime));

        /*foreach (Attribute attribute in FindObjectsOfType<Attribute>())
        {
            Debug.Log(calculateGravityForceAx(attribute));
            //Debug.Log(attribute.transformTable[attribute.transformTable.Keys.Last()].ToString());
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (isCalculateCompleted)
        {
            if(progressTime < calculateTime)
            {
                foreach (Attribute attribute in FindObjectsOfType<Attribute>())
                {
                    attribute.transform.position = attribute.transformTable[(int)(progressTime / interval)];
                }

                progressTime += Time.deltaTime;

                foreach (Attribute attribute in FindObjectsOfType<Attribute>())
                {
                    //Debug.Log(attribute.transformTable[attribute.transformTable.Keys.Last()].ToString());
                }
            }
            else
            {
                Debug.Log("Completed");
            }
        }
        else
        {
            
        }
    }

    IEnumerator CALCULATE(double time)
    {
        Attribute[] attributes = FindObjectsOfType<Attribute>();

        foreach (Attribute attribute in attributes)
        {
            attribute.transformTable.Add(0, attribute.transform.position);
            attribute.VxTable.Add(0, attribute.Vx);
            attribute.VyTable.Add(0, attribute.Vy);
        }

        for (int i = 1; i<= time/interval; i++)
        {
            foreach (Attribute attribute in attributes)
            {
                double ax = calculateGravityForceAx(attribute, i - 1);
                double ay = calculateGravityForceAy(attribute, i - 1);

                Vector3 vec = attribute.transformTable[i - 1];
                double Vx = attribute.VxTable[i - 1];
                double Vy = attribute.VyTable[i - 1];

                double dVx = interval * ax;
                double dVy = interval * ay;

                double dx = 0.5 * (2 * Vx + dVx) * interval;
                double dy = 0.5 * (2 * Vy + dVy) * interval;

                Vector3 newVec = new Vector3();
                newVec = vec;
                newVec.Set((float)(vec.x + dx), (float)(vec.y + dy), 0);

                attribute.transformTable.Add(i, newVec);
                attribute.VxTable.Add(i, Vx + dVx);
                attribute.VyTable.Add(i, Vy + dVy);
            }

            if(i % (1 / interval)  == 0)
            {
                Debug.Log((i / (time / interval) * 100 + "%"));
                yield return null;
            }
        }

        Debug.Log("Calculate Finished");
        isCalculateCompleted = true;

        yield break;
    }

    double calculateGravityForceAx(Attribute attribute, int key)
    {
        double ax = 0;

        Attribute[] attributes = FindObjectsOfType<Attribute>();

        foreach (Attribute obj in attributes)
        {
            if(obj.name == attribute.name)
            {
                continue;
            }

            double dx = obj.transformTable[key].x - attribute.transformTable[key].x;
            double dy = obj.transformTable[key].y - attribute.transformTable[key].y;
            double rangeSquare = (dx * dx) + (dy * dy);

            ax += (Variables.gravityConstant * obj.mass / rangeSquare) * dx / math.pow(rangeSquare, 0.5);
        }

        return ax;
    }

    double calculateGravityForceAy(Attribute attribute, int key)
    {
        double ay = 0;

        Attribute[] attributes = FindObjectsOfType<Attribute>();

        foreach (Attribute obj in attributes)
        {
            if (obj.name == attribute.name)
            {
                continue;
            }

            double dx = obj.transformTable[key].x - attribute.transformTable[key].x;
            double dy = obj.transformTable[key].y - attribute.transformTable[key].y;
            double rangeSquare = (dx * dx) + (dy * dy);

            ay += (Variables.gravityConstant * obj.mass / rangeSquare) * dy / math.pow(rangeSquare, 0.5);
        }

        return ay;
    }
}
