using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetanusTargetPointRotation : MonoBehaviour
{
    Transform player;
    Transform pneumoniae;

    float initDistance;

    private void Awake()
    {
        LoadObjects();
    }

    void Start()
    {
        initialize();
    }

    void Update()
    {
        Vector3 playerXZ = new Vector3(player.position.x, 0, player.position.z);
        Vector3 pneumoniaeXZ = new Vector3(pneumoniae.position.x, 0, pneumoniae.position.z);
        Vector3 thisXZ = new Vector3(transform.position.x, 0, transform.position.z);

        float angle = Vector3.Angle(pneumoniaeXZ, playerXZ);
        angle -= 180;

        if(Vector3.Angle(thisXZ, playerXZ) != angle)
            transform.RotateAround(player.position, Vector3.up, 10 * Time.deltaTime);

        //StayDistance();
    }

    void LoadObjects()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        pneumoniae = GameObject.Find("Bacteria_Pneumoniae").GetComponent<Transform>();
    }

    void initialize()
    {
        initDistance = Vector3.Distance(player.position, transform.position);
    }

    void StayDistance()
    {
        float currentDistance = Vector3.Distance(player.position, transform.position);

        // ´Ã¾î³­ °Å¸®
        float dist = currentDistance - initDistance;

        Vector3 dir = player.position - transform.position;
        dir.y = 0;

        dir = dir.normalized;

        transform.Translate(dir * dist, Space.World);
    }
}
