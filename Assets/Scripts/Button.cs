using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

// Assign on the moving button part
public class Button : MonoBehaviour
{
    /*
    private enum Mode
    {
        INSTANTIATION,
        GRENADE,
        REBOND_PLUS,
        VITESSE_PLUS,
        DEGAT_PLUS
    }
    [Header("Fonction du bouton")]
    [SerializeField] private Mode buttonMode = Mode.INSTANTIATION;
    [Header("Objet créé (si l'instantiation d'objet est choisi)")]
    public GameObject instantiatedObject;
    [Header("Lieu d'apparition")]
    public Transform instantiationSpawnPoint;
    */


    [Header("Paramètres de positions limites du bouton")]
    public float minY = 0.015f;
    public float maxY = 0.079f;
    [HideInInspector] public float thresholdY;
    [Header("Temps pour valider l'appui")]
    public int pressFramesToActivate = 200;
    private Rigidbody rb;
    

    void Start()
    {
        thresholdY = minY + 0.6f * (maxY - minY);

        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        // Clamp position
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, minY, maxY), transform.localPosition.z);
    }

}
