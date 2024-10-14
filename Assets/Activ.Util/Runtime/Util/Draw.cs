using System.Collections.Generic;
using UnityEngine;
using v3 = UnityEngine.Vector3;

namespace Activ.Util{
public static class Draw{

    // NOTE - stores drawing time in rewind mode (transitional?)
    public static float time;

    public static void Arrow(
        v3 start, v3 end, Color col,
        float margin = 0.05f, float head = 0.15f, float duration = 0f
    ){
        end = start + (end - start) * (1 - margin);
        var u = start - end;
        var d = v3.Distance(start, end) - head;
        var P = v3.MoveTowards(start, end, d);
        var L = P + u.Left() * head * 0.25f;
        var R = P + u.Right() * head * 0.25f;
        Debug.DrawLine(start, P, col, duration);
        Debug.DrawLine(L, R, col, duration);
        Debug.DrawLine(L, end, col, duration);
        Debug.DrawLine(R, end, col, duration);
    }

    public static void Rect(
        v3 min, v3 max, Color col, float duration=0f
    ){
        var A = new v3(min.x, min.y, min.z);
        var B = new v3(max.x, min.y, min.z);
        var C = new v3(max.x, min.y, max.z);
        var D = new v3(min.x, min.y, max.z);
        Poly(new v3[]{A, B, C, D}, col, duration);
    }

    public static void RectXZ(
        v3 pos, Color col, float s=0.05f, float offset=0.02f,
        float duration = 0f
    ){
        var A = pos + new v3(-s, 0f, -s);
        var B = pos + new v3( s, 0f, -s);
        var C = pos + new v3( s, 0f,  s);
        var D = pos + new v3(-s, 0f,  s);
        Poly(new v3[]{A, B, C, D}, col, duration);
    }

    public static void Circle(v3 pos, float radius)
    => CircleXZ(pos, radius, Color.white);

    public static void CircleXZ(
        Component pos, float radius, Color col,
        int count=12, float offset=0.05f, float duration = 0f
    ) => CircleXZ(
        pos.transform.position, radius, col, count, offset, duration
    );

    public static void CircleXZ(
        v3 pos, float radius, Color col,
        int count=12, float offset=0.05f, float duration = 0f
    ){
        pos = pos.Raise(offset);
        var α = 2 * Mathf.PI / count;
        for(var i = 0f; i < 360f; i += α){
            var j = i + α;
            var A = pos + new v3(Mathf.Cos(i), 0f, Mathf.Sin(i))
                  * radius;
            var B = pos + new v3(Mathf.Cos(j), 0f, Mathf.Sin(j))
                  * radius;
            Debug.DrawLine(A, B, col, duration);
        }
    }

    public static void Pie(
        v3 center, v3 direction, float radius, float angle,
        Color col, int segPer360 = 24
    ){
        direction.y = 0f;
        direction = direction.normalized * radius;
        int n = (int) ((angle * segPer360) / 360f);
        if(n < 2) n = 2;
        var α = 2 * angle / n;
        var r = Quaternion.AngleAxis(-angle, v3.up);
        var s = Quaternion.AngleAxis( α, v3.up);
        var u = r * direction;
        var C = center + v3.up * 0.05f;
        Debug.DrawRay(C, u, col);
        for(int i = 0; i < n; i++){
            var v = s * u;
            Debug.DrawLine(C + u, C + v, col);
            u = v;
            if(i == n - 1){
                Debug.DrawRay(C, u, col);
            }
        }
    }

    public static void Point(
        Component c, Color col, float s=0.05f, float offset=0.02f,
        float duration=0f
    ) => Draw.PointXZ(c.transform.position, col, s, offset, duration);

    public static void PointXZ(
        v3 pos, Color col, float s=0.05f, float offset=0.02f,
        float duration=0f
    ){
        Debug.DrawLine(
            pos + v3.left * s, pos + v3.right * s, col, duration
        );
        Debug.DrawLine(
            pos + v3.back * s, pos + v3.forward * s, col, duration
        );
    }

    public static void Path(
        UnityEngine.AI.NavMeshPath path,
        Color col, float duration=0f, float yOffset=0.05f,
        bool drawPoints = false, Color? pointColor = null
    ){
        var up = v3.up * yOffset;
        for (int i = 0; i < path.corners.Length - 1; i++){
            Debug.DrawLine(
                path.corners[i] + up,
                path.corners[i + 1] + up, col,
                duration
            );
        }
    }

    public static void Path(
        Transform path, bool loop = false, Color? col = null
    ){
        if(path == null) return;
        var P = path.LastChild().position;
        foreach(Transform child in path){
            var Q = child.position;
            if(col == null) Debug.DrawLine(P, Q);
            else Debug.DrawLine(P, Q, col.Value);
            P = Q;
        }
    }

    public static void Path(
        IEnumerable<v3> points, Color col, float yOffset=0.05f,
        float duration=0f, bool drawPoints = false,
        Color? pointColor = null
    ){
        v3? P = null;
        foreach(var Q in points){
            var Q1 = Q + v3.up * yOffset;
            if(drawPoints) Draw.PointXZ(
                Q1 + v3.up * yOffset, pointColor ?? col, s:0.1f);
            if(P.HasValue) Debug.DrawLine(P.Value , Q1, col, duration);
            P = Q1;
        }
    }

    public static void Path<T>(
        IEnumerable<T> path, System.Func<T, v3> conv, Color col,
        float duration=0f
    ){
        v3? P = null;
        foreach(var elem in path){
            var Q = conv(elem);
            if(P.HasValue) Debug.DrawLine(P.Value, Q, col, duration);
            P = Q;
        }
    }

    public static void Path<T>(
        IEnumerable<T> path,
        System.Func<T, v3> conv,
        System.Func<T, Color> colfunc,
        float duration=0f
    ){
        v3? P = null;
        foreach(var elem in path){
            var Q = conv(elem) + v3.up * 0.01f;
            var col = colfunc(elem);
            if(P.HasValue) Debug.DrawLine(P.Value, Q, col, duration);
            P = Q;
        }
    }

    public static void Poly(
        v3[] points, Color col, float duration=0f
    ){
        var n = points.Length;
        for(int i = 0; i < n; i++) Debug.DrawLine(
            points[i], points[(i + 1) % n], col, duration
        );
    }

}}
