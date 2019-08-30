using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class ScreenMesh : MonoBehaviour
{
    [SerializeField] Material _rainMat;

    // 三角形の個数
    [SerializeField] int triangleCount = 1500;

    // 三角形の大きさ
    private float triangleScale = 0.002f;

    // メッシュの大きさ
    private Vector3 meshScale = new Vector3(4f, 4f, 4f);

    private async void Start()
    {
        this.Initialize();
        await ShakeXZ();
    }

    /// <summary>
    /// 初期化 
    /// </summary>
    public void Initialize()
    {
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        int pos = 0;
        for (int i = 0; i < this.triangleCount; i++)
        {
            var v1 = Vector3.Scale(new Vector3(Random.value, Random.value, Random.value) - Vector3.one * 0.5f, this.meshScale);
            var v2 = v1 + new Vector3(Random.value - 0.5f, 0f, Random.value - 0.5f) * triangleScale;
            var v3 = v1 + new Vector3(Random.value - 0.5f, 0f, Random.value - 0.5f) * triangleScale;

            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);

            triangles.Add(pos + 0);
            triangles.Add(pos + 1);
            triangles.Add(pos + 2);
            pos += 3;
        }

        //メッシュ生成
        var mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        var meshFilter = this.GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }
    
    private async Task ShakeXZ()
    {
        while (true)
        {
            _rainMat.SetFloat("_X_Offset", Random.Range(-0.5f, 0.5f));
            _rainMat.SetFloat("_Z_Offset", Random.Range(-0.5f, 0.5f));
            
            await new WaitForEndOfFrame();
        }
    }
}