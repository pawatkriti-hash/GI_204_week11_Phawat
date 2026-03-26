using UnityEngine;
using System.Collections.Generic;

public class Gravitation : MonoBehaviour
{
    public static List<Gravitation> otherObj;
    private Rigidbody _rb;
    const float G = 6.67f; // ปรับตามต้องการโดยใส่ 0.00 

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (otherObj == null)
        {
            // หา Class Gravitation ในวัตถุอื่นๆ และเก็บใน List
            otherObj = new List<Gravitation>();
        }
        otherObj.Add(this);
    }
    void FixedUpdate()
    {
        foreach (Gravitation obj in otherObj)
        {
            if (obj != this) // ป้องกันไม่ให้วัตถุโดนแรงดึงดูดตัวเอง
            {
                Attract(obj);
            }
        }
    }
    void Attract(Gravitation other)
    {
        Rigidbody otherRb = other._rb; // ดึงค่ามวล m
        Vector3 direction = _rb.position - otherRb.position; // ทิศทางจากวัตถุมวล M ไป m

        float distance = direction.magnitude; // หาระยะห่าง r
        if (distance == 0f) return; // ป้องกันไม่ให้มีแรงดึงดูด เมื่อวัตถุทั้งสองอยู่ตำแหน่งเดียวกัน

        // F = G(m1 * m2) / r^2 
        float forceMagnitude = G * (_rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationForce = forceMagnitude * direction.normalized; // นำแรงที่ได้มาใส่ทิศทาง
        otherRb.AddForce(gravitationForce); // ใส่แรงดึงดูดพร้อมทิศทางให้กับวัตถุ
    }
}
