using System;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class EmGraphController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int points = 100;
    public int pointTime = 10;

    public TextMeshProUGUI text;

    private float initalEm;

    // Start is called before the first frame update
    void Start()
    {
        initalEm = Convert.ToSingle(CalculateEm(0));
    }

    // Update is called once per frame
    void Update()
    {
        CalculateController[] calculateControllers = FindObjectsOfType<CalculateController>();

        if (calculateControllers[0].isCalculateCompleted)
        {
            Draw();
        }
    }

    void Draw()
    {
        CalculateController[] calculateControllers = FindObjectsOfType<CalculateController>();

        double progressTime = calculateControllers[0].progressTime;
        double interval = calculateControllers[0].interval;

        float xStart = 0;
        float xFinish = 150;

        lineRenderer.positionCount = points;

        float[] Ems = new float[150];

        for (int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            double t = ((progressTime - pointTime + (((double) pointTime / (double) points) * (currentPoint + 1))) / interval);

            if (t > 0)
            {
                Ems[currentPoint] = Convert.ToSingle(CalculateEm((int)t));
            }
            else
            {
                Ems[currentPoint] = Convert.ToSingle(CalculateEm(0));
            }
        }

        for (int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float progress = (float) currentPoint / (points-1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y;
            if (Ems.Min() != 0)
                y = (Ems[currentPoint] / Ems.Min()) * 100;
            else
                y = 0;

            lineRenderer.SetPosition(currentPoint, new Vector3 (x, y, 0));
        }

        text.text = (CalculateEm((int)(progressTime / interval)) / initalEm * 100).ToString("0.00") + "%";
    }

    double CalculateEm(int time)
    {
        double Em = 0;

        Attribute[] attributes = FindObjectsOfType<Attribute>();

        foreach (Attribute attribute in attributes)
        {
            double deltaEm = 0;

            Vector3 t = attribute.transformTable[time];
            double m = attribute.mass;
            double Vx = attribute.VxTable[time];
            double Vy = attribute.VyTable[time];

            deltaEm += (1 / 2) * m * ((Vx * Vx) + (Vy * Vy));
            
            foreach (Attribute entity in attributes)
            {
                if(attribute.name != entity.name)
                {
                    Vector3 t1 = entity.transformTable[time];

                    deltaEm += -Variables.gravityConstant * m * entity.mass / (Vector3.Distance(t, t1));
                }
            }

            Em += deltaEm;
        }

        return Em;
    }
}
