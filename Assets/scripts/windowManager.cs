using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class windowManager : MonoBehaviour
{
    // Ventanas en el canvas
    public GameObject menuPrincipal;
    public GameObject informacionApp;
    public GameObject escanear;
    public GameObject informacionObjetos;

    // Canvases de descripción
    public GameObject descripcion1;
    public GameObject descripcion2;
    public GameObject descripcion3;
    
    // ARCamera y ModelTarget fuera del canvas
    public GameObject arCamera;
    public GameObject modelTarget;

    // Botón en la ventana de escanear
    public Button botonVerInformacion;

    void Start()
    {
        // Mostrar el menú principal al inicio
        ShowMenuPrincipal();

        // Inicialmente, desactivar el botón
        if (botonVerInformacion != null)
        {
            botonVerInformacion.gameObject.SetActive(false);
            botonVerInformacion.onClick.AddListener(OnBotonVerInformacionClicked);
        }
    }

    // Método para mostrar el menú principal
    public void ShowMenuPrincipal()
    {
        menuPrincipal.SetActive(true);
        informacionApp.SetActive(false);
        escanear.SetActive(false);
        informacionObjetos.SetActive(false);
        DeactivateDescripcion();
        DeactivateAR();
    }

    // Método para mostrar la información de la app
    public void ShowInformacionApp()
    {
        menuPrincipal.SetActive(false);
        informacionApp.SetActive(true);
        escanear.SetActive(false);
        informacionObjetos.SetActive(false);
        DeactivateDescripcion();
        DeactivateAR();
    }

    // Método para mostrar la ventana de escanear
    public void ShowEscanear()
    {
        menuPrincipal.SetActive(false);
        informacionApp.SetActive(false);
        escanear.SetActive(true);
        informacionObjetos.SetActive(false);
        DeactivateDescripcion();
        ActivateAR();
        // Desactivar el botón al entrar en la ventana de escanear
        if (botonVerInformacion != null)
        {
            botonVerInformacion.gameObject.SetActive(false);
        }
    }

    // Método para mostrar la información de los objetos
    public void ShowInformacionObjetos()
    {
        menuPrincipal.SetActive(false);
        informacionApp.SetActive(false);
        escanear.SetActive(false);
        informacionObjetos.SetActive(true);
        DeactivateDescripcion();
        DeactivateAR();
    }

    // Método para activar ARCamera y ModelTarget
    private void ActivateAR()
    {
        if (arCamera != null)
        {
            arCamera.SetActive(true);
            Debug.Log("ARCamera activada.");
        }
        if (modelTarget != null)
        {
            modelTarget.SetActive(true);
            Debug.Log("ModelTarget activado.");
        }
    }

    // Método para desactivar ARCamera y ModelTarget
    private void DeactivateAR()
    {
        if (arCamera != null)
        {
            arCamera.SetActive(false);
            Debug.Log("ARCamera desactivada.");
        }
        if (modelTarget != null)
        {
            modelTarget.SetActive(false);
            Debug.Log("ModelTarget desactivado.");
        }
    }

    // Método para desactivar todos los canvases de descripción
    public void DeactivateDescripcion()
    {
        descripcion1.SetActive(false);
        descripcion2.SetActive(false);
        descripcion3.SetActive(false);
    }

    // Método para activar el canvas de descripción correspondiente
    public void ActivateDescripcion(int descripcionID)
    {
        DeactivateDescripcion(); // Desactivar todos los demás canvases de descripción
        switch (descripcionID)
        {
            case 1:
                descripcion1.SetActive(true);
                break;
            case 2:
                descripcion2.SetActive(true);
                break;
            case 3:
                descripcion3.SetActive(true);
                break;
            default:
                Debug.LogWarning("Descripción ID no válida: " + descripcionID);
                break;
        }
    }

    // Método llamado cuando se presiona el botón Ver Información
    private void OnBotonVerInformacionClicked()
    {
        // Cambiar a la ventana de información de objetos y mostrar la descripción correspondiente
        ShowInformacionObjetos();
        ActivateDescripcion(lastDescripcionID);
    }

    // Variable para almacenar el ID de la última descripción activa
    private int lastDescripcionID = -1;

    // Método para activar el botón cuando se aplica una textura
    public void EnableVerInformacionButton(int descripcionID)
    {
        lastDescripcionID = descripcionID;
        if (botonVerInformacion != null)
        {
            botonVerInformacion.gameObject.SetActive(true);
        }
    }
}
