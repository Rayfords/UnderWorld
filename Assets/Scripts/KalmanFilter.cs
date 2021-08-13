using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalmanFilter : MonoBehaviour
{
    private const float VAL = 500F;
    KalmanFilterVector2 kfv2 = new KalmanFilterVector2(VAL);
    public Vector2 Vector2DFilterUpdate(Vector2 fv)
    {
        return kfv2.GetUpdated(fv);
    }
}

class KalmanFilterVector2
{
    struct KFData
    {
        public float xPrev;
        public float pPrev;
        public float xCurr;
        public float pCurr;
        public float K;
    }

    float F = 1;
    float H = 1;
    float Q = 1;
    float R = 1;

    KFData x;
    KFData y; 

    public KalmanFilterVector2(float R)
    {
        this.R = R;
    }

    public Vector2 GetUpdated(Vector2 z)
    {
        Calculate(ref x, z.x);
        Calculate(ref y, z.y);

        return new Vector2(x.xCurr, y.xCurr);
    }

    void Calculate(ref KFData data, float value)
    {
        // --- MEASURMENT UPDATE ---

        data.xPrev = F * data.xCurr;
        data.pPrev = F * data.pCurr * F + Q;

        // --- STATE PREDICTION ---

        data.K = H * data.pPrev / (H * data.pPrev * H + R);
        data.xCurr = data.xPrev + data.K * (value - H * data.xPrev);
        data.pCurr = (1 - data.K * H) * data.pPrev;
    }
}
