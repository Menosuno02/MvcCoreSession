using Microsoft.AspNetCore.Mvc;
using MvcCoreSession.Helpers;
using MvcCoreSession.Models;

namespace MvcCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        int numero = 1;

        public IActionResult Index()
        {
            this.numero += 1;
            ViewData["NUMERO"] = "El valor del número es " + this.numero;
            return View();
        }

        // Action para visualizar datos en sesion de forma simple
        public IActionResult SessionSimple(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    // Guardamos datos en Session
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora", DateTime.Now.ToLongTimeString());
                }
                else if (accion.ToLower() == "mostrar")
                {
                    ViewData["USUARIO"] = HttpContext.Session.GetString("nombre");
                    ViewData["HORA"] = HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }

        public IActionResult SessionMascota(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Flounder",
                        Raza = "Pez",
                        Edad = 11
                    };
                    // Para almacenar el objeto en session debemos
                    // convertirlo a byte[]
                    byte[] data = HelperBinarySession.ObjectToByte(mascota);
                    HttpContext.Session.Set("MASCOTA", data);
                    ViewData["MENSAJE"] = "Datos almacenados correctamente";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    // Debemos recuperar los byte[] de Session
                    // que representan el objeto Mascota
                    byte[] data = HttpContext.Session.Get("MASCOTA");
                    // Convertimos los byte[] a nuestro objeto mascota
                    Mascota mascota = (Mascota)HelperBinarySession.ByteToObject(data);
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }
    }
}
