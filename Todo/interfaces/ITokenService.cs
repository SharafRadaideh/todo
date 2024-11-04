using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefaultNamespace;


namespace api.interfaces
{
    public interface ITokenService
    {
    string CreateToken(User user);

    }
}