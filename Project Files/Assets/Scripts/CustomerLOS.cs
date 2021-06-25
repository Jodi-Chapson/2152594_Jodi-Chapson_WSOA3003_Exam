using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLOS : MonoBehaviour
{
    //reference for this detection was taken and adapted from the work of Sebastian Lague https://www.youtube.com/watch?v=rQG9aUWarwE 
    //and Ituozzo who did some 3D to 2D conversions of the original codes https://gist.github.com/ltuozzo/112138ad42c9530de15b5bc22226b739

    [Header("References")]
    public float viewRadius;
    public CustomerAI customer;
    


    public LayerMask playermask;
    public LayerMask levelmask;
    public float meshRes;
    public MeshFilter viewmeshfilter;
    Mesh viewMesh;

    public void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "Fov Mesh";
        viewmeshfilter.mesh = viewMesh;
        StartCoroutine(FindPlayerDelayed(0.2f));
    }
    public void FindPlayer()
    {
        Collider2D playerping = Physics2D.OverlapCircle(transform.position, viewRadius, playermask);

        if (playerping != null)
        {
            if (playerping.gameObject.tag == "Player")
            {
                Vector3 dirToPlayer = (playerping.transform.position - transform.position).normalized;
                float dstToPlayer = Vector3.Distance(transform.position, playerping.transform.position);

                //draw ray from enemy to when the player came on their radar
                //checks if they are blocked by a shelf
                if (!Physics2D.Raycast(transform.position, dirToPlayer, dstToPlayer, levelmask ))
                {
                    //makes customer stop what they are doing and move half the distance towards player :)

                    Vector3 halfwaypoint = new Vector3((this.transform.position.x + playerping.transform.position.x) / 2, (this.transform.position.y + playerping.transform.position.y) / 2, 0);

                    //Vector3 newpos = new Vector3((-enemylastPos.x + 2*playerlastPos.x), (-enemylastPos.y + 2*playerlastPos.y), 0);



                    customer.ApproachPlayer(halfwaypoint);

                }
                
            }

        }

        



    }

    public void LateUpdate()
    {
        DrawCircle();
    }


    public void DrawCircle()
    {
        //calculates all the end points of each ray in the fov
        int rayCount = Mathf.RoundToInt(360 * meshRes);
        float rayAngleSize = 360 / rayCount;
        List<Vector3> viewpoints = new List<Vector3>();

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.z - 360 / 2 + rayAngleSize * i;
            ViewCastInfo newviewcast = ViewCast(-angle);
            viewpoints.Add(newviewcast.point);
        }


        //calculates all the triangles of the mesh
        int vertexcount = viewpoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexcount];
        int[] triangles = new int[(vertexcount - 2) * 3];



        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertexcount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewpoints[i]);



            if (i < vertexcount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }

        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    public IEnumerator FindPlayerDelayed (float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindPlayer();
        }
    }



    ViewCastInfo ViewCast(float globalangle)
    {
        Vector3 dir = DirFromAngle(globalangle, true);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, customer.raymask);

        if (hit.collider != null)
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalangle);

        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalangle);
        }
    }



    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }

    }



    //method for converting angles to vector3 direction 
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
   

}
