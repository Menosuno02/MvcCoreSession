using Newtonsoft.Json;

namespace MvcCoreSession.Helpers
{
    public class HelperJsonSession
    {
        // Internamente existe un método en Session para
        // trabajar con string, no con bytes
        // HttpContext.Session.GetString
        // HttpContext.Session.SetString
        // Almacenamos objetos: { Nombre: "Mascot", Raza: "Perro", Edad: 15 }
        // Necesitamos un método para almacenar el objeto
        public static string SerializeObject<T>(T data)
        {
            // Convertimos el objeto a string utilizando Newton
            string json = JsonConvert.SerializeObject(data);
            return json;
        }

        public static T DeserializeObject<T>(string data)
        {
            // Mediante Newton deserializamos
            T objeto = JsonConvert.DeserializeObject<T>(data);
            return objeto;
        }
    }
}
