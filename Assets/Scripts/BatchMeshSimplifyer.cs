using UnityEngine;
using UnityEditor;
using System.IO;
using UnityMeshSimplifier;

public class BatchMeshSimplifier : MonoBehaviour
{
    [Header("Dossier source")]
    public string sourceFolder = "Assets/MeshesOriginals";
    [Header("Dossier destination")]
    public string destinationFolder = "Assets/MeshesSimplified";
    [Range(0.1f, 1f)]
    public float quality = 0.5f; // 0.5 = 50% triangles

    [ContextMenu("Simplifier tous les meshes")]

    void Start()
    {
        SimplifyAllMeshes();
    }
    public void SimplifyAllMeshes()
    {
        if (!Directory.Exists(destinationFolder))
            Directory.CreateDirectory(destinationFolder);

        string[] guids = AssetDatabase.FindAssets("t:Mesh", new[] { sourceFolder });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Mesh originalMesh = AssetDatabase.LoadAssetAtPath<Mesh>(path);

            if (originalMesh == null) continue;

            // Crée une copie pour simplification
            Mesh meshCopy = Object.Instantiate(originalMesh);
            MeshSimplifier meshSimplifier = new MeshSimplifier();
            meshSimplifier.Initialize(meshCopy);
            meshSimplifier.SimplifyMesh(quality);
            Mesh simplifiedMesh = meshSimplifier.ToMesh();

            // Sauvegarde le mesh simplifié
            string fileName = Path.GetFileNameWithoutExtension(path) + "_simplified.asset";
            string savePath = Path.Combine(destinationFolder, fileName);
            AssetDatabase.CreateAsset(simplifiedMesh, savePath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Simplification terminée !");
    }
}

