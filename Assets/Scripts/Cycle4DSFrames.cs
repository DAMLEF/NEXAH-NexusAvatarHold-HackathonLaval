using UnityEngine;

public class Cycle4DSFrames : MonoBehaviour
{
    public Mesh[] meshes;
    public Material[] materials;

    [Range(0.05f, 1f)]
    public float frameDelay = 0.2f;

    int _index;
    float _timer;

    void Update()
    {
        if (meshes.Length == 0 || materials.Length == 0)
            return;

        _timer += Time.deltaTime;
        if (_timer < frameDelay)
            return;

        _timer = 0f;

        _index = (_index + 1) % meshes.Length;

        MeshFilter mf = GetComponent<MeshFilter>();
        MeshRenderer mr = GetComponent<MeshRenderer>();

        mf.sharedMesh = meshes[_index];
        mr.sharedMaterial = materials[_index];
    }
}
