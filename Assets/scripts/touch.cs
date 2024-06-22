using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class touch : MonoBehaviour
{
    GameObject Modelo;
    public Texture2D Imagen, text1, text2, text3;
    Color c;
    int id;

    // Referencia al windowManager
    private windowManager windowManager;

    // Start is called before the first frame update
    void Start()
    {
        Modelo = GameObject.Find("relieve");
        if (Modelo == null)
        {
            Debug.LogError("No se encontró el objeto 'relieve'.");
        }

        // Encontrar el windowManager en la escena
        windowManager = FindObjectOfType<windowManager>();
        if (windowManager == null)
        {
            Debug.LogError("No se encontró el objeto 'windowManager'.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("Toque detectado, verificando estado de Vuforia.");

            var vuforiaBehaviour = FindObjectOfType<Vuforia.VuforiaBehaviour>();
            if (vuforiaBehaviour != null)
            {
                Debug.Log("Estado de VuforiaBehaviour antes del toque: " + vuforiaBehaviour.enabled);
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
                
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Objeto tocado: " + hit.transform.name);

                if (hit.transform.name == "relieve")
                {
                    Vector2 punto = hit.textureCoord;
                    c = Imagen.GetPixel(Mathf.FloorToInt(punto.x * Imagen.width), Mathf.FloorToInt(punto.y * Imagen.height));
                    id = Mathf.RoundToInt(c.r * 255 / 10);

                    Debug.Log("ID detectado: " + id);

                    Renderer renderer = Modelo.GetComponentInChildren<Renderer>();
                    if (renderer == null)
                    {
                        Debug.LogError("No se encontró el componente Renderer en 'relieve'.");
                        return;
                    }

                    if (id == 10)
                    {
                        renderer.material.SetTexture("_DecalTex", text1);
                        Debug.Log("Textura text1 aplicada.");
                        windowManager.ActivateDescripcion(1); // Activar la descripción 1
                        windowManager.EnableVerInformacionButton(1); // Habilitar botón con descripción 1
                    }
                    else if (id == 26)
                    {
                        renderer.material.SetTexture("_DecalTex", text2);
                        Debug.Log("Textura text2 aplicada.");
                        windowManager.ActivateDescripcion(2); // Activar la descripción 2
                        windowManager.EnableVerInformacionButton(2); // Habilitar botón con descripción 2
                    }
                    else if (id == 0)
                    {
                        renderer.material.SetTexture("_DecalTex", text3);
                        windowManager.ActivateDescripcion(3); // Activar la descripción 3
                        windowManager.EnableVerInformacionButton(3); // Habilitar botón con descripción 3
                    }
                    else
                    {
                        renderer.material.SetTexture("_DecalTex", null);
                        Debug.Log("Textura eliminada.");
                        windowManager.DeactivateDescripcion(); // Desactivar todas las descripciones si no se encuentra una textura válida
                    }
                }
            }
            else
            {
                Debug.Log("No se tocó ningún objeto.");
            }

            if (vuforiaBehaviour != null)
            {
                Debug.Log("Estado de VuforiaBehaviour después del toque: " + vuforiaBehaviour.enabled);
            }
        }
    }
}