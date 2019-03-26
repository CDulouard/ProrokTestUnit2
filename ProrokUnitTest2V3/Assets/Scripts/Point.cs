﻿public class Point
{
    public Point(float hAngle, float vAngle, float distance)
    {
        HorizontalAngle = hAngle;
        VerticalAngle = vAngle;
        Distance = distance;
    }

    public float Distance { get; set; }

    public float VerticalAngle { get; set; }

    public float HorizontalAngle { get; set; }
}