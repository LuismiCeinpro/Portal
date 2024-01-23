using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Añadir este código al GameManager
public class Brazier : MonoBehaviour
{
    //Única variable, guarda el objeto del fuego. Si hay varios braseros, duplicar el código. La misma lógica de fuego desactivado
    //se puede usar para la antorcha, que la llama esté donde vaya a ir la antorcha y al terminar el puzzle que se active
    public GameObject Fire;
    //Única función
    private void Start()
    {
        Torch.OnComplete += eventHandler;
    }

    private void eventHandler(string a)
    {
        FireOn();
    }
        public void FireOn()
    {
        Fire.SetActive(true);
    }
}
