using System;
using System.Collections;
using UnityEngine;
using Vuforia;

public class Movimiento : MonoBehaviour
{
    public GameObject Astra;
    public ObserverBehaviour[] marcadores;
    public int marcadorActivo = 0;
    public float offsetAngulo = 0f;
    public float velocidad = 1.0f;

    [Header("Configuración Extra")]
    public float velocidadRotacion = 3.0f;

    private bool isMoving = false;
    private Animator animator;

    void Start()
    {
        if (Astra != null)
        {
            animator = Astra.GetComponentInChildren<Animator>();

            if (animator == null)
            {
                Debug.LogWarning("No se encontró un Animator en el modelo Astra.");
            }
        }
    }

    public void MoverseAlSiguienteMarcador()
    {
        if (!isMoving)
        {
            StartCoroutine(MoverModelo());
        }
    }

    private IEnumerator MoverModelo()
    {
        isMoving = true;
        ObserverBehaviour marcador = GetNextMarcador();

        if (marcador == null)
        {
            isMoving = false;
            yield break;
        }

        Vector3 posicionInicial = Astra.transform.position;
        Vector3 posicionFinal = marcador.transform.position;

        // Calcula la dirección hacia el objetivo anulando el eje Y
        Vector3 direccionDeMirada = posicionFinal - posicionInicial;
        direccionDeMirada.y = 0;

        Quaternion rotacionInicial = Astra.transform.rotation;
        Quaternion rotacionFinal;

        if (direccionDeMirada != Vector3.zero)
        {
            rotacionFinal = Quaternion.LookRotation(direccionDeMirada);
            if (offsetAngulo != 0f)
            {
                rotacionFinal *= Quaternion.Euler(0, offsetAngulo, 0);
            }
        }
        else
        {
            rotacionFinal = rotacionInicial;
        }

        // Hacer la rotación antes de moverse
        float viajeRotacion = 0;
        while (viajeRotacion <= 1f)
        {
            viajeRotacion += Time.deltaTime * velocidadRotacion;
            Astra.transform.rotation = Quaternion.Slerp(rotacionInicial, rotacionFinal, viajeRotacion);
            yield return null;
        }
        Astra.transform.rotation = rotacionFinal;

        // Inicia la animación de caminar
        if (animator != null)
        {
            animator.SetBool("Caminando", true);
        }

        float viajeMovimiento = 0;
        while (viajeMovimiento <= 1f)
        {
            viajeMovimiento += Time.deltaTime * velocidad;
            Astra.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, viajeMovimiento);
            yield return null;
        }
        Astra.transform.position = posicionFinal;

        if (animator != null)
        {
            animator.SetBool("Caminando", false);
        }

        marcadorActivo = (marcadorActivo + 1) % marcadores.Length;
        isMoving = false;
    }

    private ObserverBehaviour GetNextMarcador()
    {
        int siguienteMarcador = marcadorActivo;
        ObserverBehaviour SiguienteTarget = marcadores[siguienteMarcador];

        if (SiguienteTarget != null && (SiguienteTarget.TargetStatus.Status == Status.TRACKED || SiguienteTarget.TargetStatus.Status == Status.EXTENDED_TRACKED))
        {
            return SiguienteTarget;
        }

        return null;
    }
}