using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration.Interfaces
{
    public interface IPayload<out T> where T: Resource
    {
        IEnumerable<T> GetPayload();
    }
}
