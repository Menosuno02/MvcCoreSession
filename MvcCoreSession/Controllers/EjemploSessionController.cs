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

        public IActionResult SessionCollection(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    List<Mascota> mascotas = new List<Mascota>
                    {
                        new Mascota
                        {
                            Nombre = "Pumba",
                            Raza = "Jabalí",
                            Edad = 14
                        },
                        new Mascota
                        {
                            Nombre = "Rafiki",
                            Raza = "Mono",
                            Edad = 18
                        },
                        new Mascota
                        {
                            Nombre = "Olaf",
                            Raza = "Cosa",
                            Edad = 8
                        },
                        new Mascota
                        {
                            Nombre = "Nala",
                            Raza = "Leona",
                            Edad = 12
                        }
                    };
                    byte[] data = HelperBinarySession.ObjectToByte(mascotas);
                    HttpContext.Session.Set("MASCOTAS", data);
                    ViewData["MENSAJE"] = "Colección almacenada";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    List<Mascota> mascotas = (List<Mascota>)
                        HelperBinarySession.ByteToObject(data);
                    return View(mascotas);
                }
            }
            return View();
        }

        public IActionResult SessionMascotaJson(string accion)
        {
            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre = "Abu",
                        Raza = "Mono",
                        Edad = 15
                    };
                    // Serializamos con JSON
                    string jsonMascota =
                        HelperJsonSession.SerializeObject<Mascota>(mascota);
                    HttpContext.Session.SetString("MASCOTA", jsonMascota);
                    ViewData["MENSAJE"] = "Datos almacenados";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    string jsonMascota = HttpContext.Session.GetString("MASCOTA");
                    Mascota mascota =
                        HelperJsonSession.DeserializeObject<Mascota>(jsonMascota);
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }
    }
}
