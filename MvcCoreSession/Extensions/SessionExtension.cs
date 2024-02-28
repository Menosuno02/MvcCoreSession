using MvcCoreSession.Helpers;
using Newtonsoft.Json;

namespace MvcCoreSession.Extensions
{
    public static class SessionExtension
    {
        // Queremos un método para recuperar cualquier objeto
        // HttpContext.Session.GetObject(key)
        public static T GetObject<T>(this ISession session, string key)
        {
            // Necesitamos recuperar los datos que tenemos
            // almacenados en Session mediante un key
            // Recuperamos el JSON
            string json = session.GetString(key);
            // Cuando no existe
            if (json == null)
            {
                // Si no existe, devolvemos valor por defecto del objeto (T)
                return default(T);
            }
            T data = JsonConvert.DeserializeObject<T>(json);
            return data;
        }

        // Queremos un método para almacenar cualquier objeto
        // dentro de Session
        // HttpContext.Session.SetObject(key, value)
        public static void SetObject
            (this ISession session, string key, Object value)
        {
            // Serializamos objeto a JSON
            string data = JsonConvert.SerializeObject(value);
            // Almacenamos en Session el string JSON
            session.SetString(key, data);
        }
    }
}
