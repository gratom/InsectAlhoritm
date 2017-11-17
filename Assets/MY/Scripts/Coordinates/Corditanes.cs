using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corditanes
{
    public float SDegree;
    public float DDegree;

    public static Corditanes GetRandom()
    {
        Corditanes c = new Corditanes();
        c.SetRandom();
        return c;
    }

    public static float Distance(Corditanes position1, Corditanes position2)
    {
        // перевести координаты в радианы
        float lat1 = position1.SDegree / 57.2958f;
        float lat2 = position2.SDegree / 57.2958f;
        float long1 = position1.DDegree / 57.2958f;
        float long2 = position2.DDegree / 57.2958f;
 
        // косинусы и синусы широт и разницы долгот
        float cl1 = Mathf.Cos(lat1);
        float cl2 = Mathf.Cos(lat2);
        float sl1 = Mathf.Sin(lat1);
        float sl2 = Mathf.Sin(lat2);
        float delta = long2 - long1;
        float cdelta = Mathf.Cos(delta);
        float sdelta = Mathf.Sin(delta);
 
        // вычисления длины большого круга
        float y = Mathf.Sqrt(Mathf.Pow(cl2 * sdelta, 2) + Mathf.Pow(cl1 * sl2 - sl1 * cl2 * cdelta, 2));
        float x = sl1 * sl2 + cl1 * cl2 * cdelta;
 
        //
        float ad = Mathf.Atan2(y, x);
        float dist = ad * Setting.Instance.EARTH_RADIUS;
        return dist / 1000;
    }

    public void SetRandom()
    {
        SDegree = Random.Range(-55, 55);
        DDegree = Random.Range(-180, 180);
    }

    public void SetCoordinates(float ShirotaDegree, float DolgotaDegree)
    {
        SDegree = ShirotaDegree;
        DDegree = DolgotaDegree;
    }

    public Vector3 ToPosition(Vector3 Center, float Radius)
    {
        Vector3 RealCoorditanes = new Vector3(0,0,0);
        RealCoorditanes.y = (Center.y + (Mathf.Sin(SDegree / 57.2958f) * Radius));
        float p = Mathf.Cos(SDegree / 57.2958f) * Radius;
        RealCoorditanes.z = (Center.z + (Mathf.Sin(DDegree / 57.2958f) * p));
        RealCoorditanes.x = (Center.x + (Mathf.Cos(DDegree / 57.2958f) * p));
        return RealCoorditanes;
    }

    public void SetFromVector3(Vector3 Position)
    {

    }
}
