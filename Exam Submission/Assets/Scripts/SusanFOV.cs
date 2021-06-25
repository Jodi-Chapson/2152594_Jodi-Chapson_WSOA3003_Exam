using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SusanFOV : MonoBehaviour
{
    //reference for this detection was taken and adapted from the work of Sebastian Lague https://www.youtube.com/watch?v=rQG9aUWarwE 
    //and Ituozzo who did some 3D to 2D conversions of the original codes https://gist.github.com/ltuozzo/112138ad42c9530de15b5bc22226b739

    [Header("References")]
    public BasicEnemy susan;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    
    
    
    //rotates the field of view :D
    [Range(0,270)]
    public float anglemod;


    public LayerMask playermask;
    public LayerMask levelmask;
    public float meshRes;
    public MeshFilter viewmeshfilter;
    Mesh viewMesh;
    public float dst;

    public void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "Fov Mesh";
        viewmeshfilter.mesh = viewMesh;


        StartCoroutine(FindPlayerDelayed(0.1f));
    }
    public void FindPlayer()
    {
        Collider2D playerping = Physics2D.OverlapCircle(transform.position, viewRadius, playermask);

        if (playerping != null)
        {
            if (playerping.gameObject.tag == "Player")
            {
                Vector3 dirToPlayer = (playerping.transform.position - transform.position).normalized;
                

                // needs to check whether the player is in the FOV angle :)

                Transform playertarget = playerping.transform;

                

                if (Vector3.Angle(transform.up, dirToPlayer) < viewAngle / 2)
                {
                    float dstToPlayer = Vector3.Distance(transform.position, playertarget.transform.position);

                    //draw ray from enemy to when the player came on their radar
                    //checks if they are blocked by a shelf
                    if (!Physics2D.Raycast(transform.position, dirToPlayer, dstToPlayer, levelmask))
                    {

                        Vector3 playercurrentpos = playertarget.position;
                        
                        susan.Track(playercurrentpos);

                    }
                }
                else
                {
                    float dstToPlayer = Vector3.Distance(transform.position, playertarget.transform.position);

                    
                    if (!Physics2D.Raycast(transform.position, dirToPlayer, dstToPlayer, levelmask))
                    {

                        if (dstToPlayer < dst)
                        {
                            Vector3 playercurrentpos = playertarget.position;

                            susan.Track(playercurrentpos);
                        }

                    }
                }
            }
        }
    }

    public IEnumerator FindPlayerDelayed(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindPlayer();
        }
    }


    void LateUpdate()
    {
        DrawFOV();
    }

    public void DrawFOV()
    {
        //calculates all the end points of each ray in the fov
        int rayCount = Mathf.RoundToInt(viewAngle * meshRes);
        float rayAngleSize = viewAngle / rayCount;
        List<Vector3> viewpoints = new List<Vector3>();

        for (int i = 0; i<= rayCount; i++ )
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 + rayAngleSize * i;
            ViewCastInfo newviewcast = ViewCast(-angle);
            viewpoints.Add(newviewcast.point);  
        }


        //calculates all the triangles of the mesh
        int vertexcount = viewpoints.Count + 1;
        Vector3[] vertices = new Vector3 [vertexcount];
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

    ViewCastInfo ViewCast( float globalangle )
    {
        Vector3 dir = DirFromAngle(globalangle, true);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, susan.raymask);

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

        return new Vector3(Mathf.Sin((angleInDegrees + anglemod) * Mathf.Deg2Rad), Mathf.Cos((angleInDegrees + anglemod)* Mathf.Deg2Rad), 0);
    }
}
