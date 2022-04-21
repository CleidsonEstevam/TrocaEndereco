using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaEndereco.Models;

namespace TrocaEndereco.Sessions
{
    public static class ISessionsExtensions
    {
        public static void Set<T>(this ISession session, string key, T value) 
        {
            session.SetString(key, JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto
            }));

        }

        public static Movimentacao[] Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            var report = (Movimentacao[])Newtonsoft.Json.JsonConvert.DeserializeObject(value, typeof(Movimentacao[]));
            return report;

        }
    }
}
