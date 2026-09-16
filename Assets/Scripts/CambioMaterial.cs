using UnityEngine;

public class CambioMaterialHijo : MonoBehaviour
{
    [Header("Malla especifica a cambiar")]
    public Renderer mallaCuerpo;
    public Renderer mallaCabello;
    public Renderer mallaCabello2;

    [Header("Materiales")]
    public Material Skin1;
    public Material Skin2;
    public Material Skin3;
    public Material Skin4;
    public Material Skin5;

    public Material Hair1;
    public Material Hair2;
    public Material Hair3;

    private int estadoSkin = 1;

    public void CambiarSkin()
    {
        int estadoAnterior = estadoSkin;
        do
        {
            estadoSkin = Random.Range(1, 9);
        } while (estadoSkin == estadoAnterior);
        if (mallaCuerpo == null)
        {
            Debug.LogWarning("No hay malla del cuerpo asignada en el inspector.");
            return;
        }
        if (mallaCabello == null)
        {
            Debug.LogWarning("No hay malla del cabello asignada en el inspector.");
            return;
        }
        if (mallaCabello2 == null)
        {
            Debug.LogWarning("No hay malla del cabello asignada en el inspector.");
            return;
        }
        switch (estadoSkin)
        {
            case 1:
                mallaCuerpo.material = Skin1;
                break;
            case 2:
                mallaCuerpo.material = Skin2;
                break;
            case 3:
                mallaCuerpo.material = Skin3;
                break;
            case 4:
                mallaCuerpo.material = Skin4;
                break;
            case 5:
                mallaCuerpo.material = Skin5;
                break;
            case 6:
                mallaCabello.material = Hair1;
                mallaCabello2.material = Hair1;
                break;
            case 7:
                mallaCabello.material = Hair2;
                mallaCabello2.material = Hair2;
                break;
            case 8:
                mallaCabello.material = Hair3;
                mallaCabello2.material = Hair3;
                break;
            default:
                Debug.LogWarning("Valor de skin no válido.");
                break;
        }
    }
}