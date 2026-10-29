using System;
using System.Collections.Generic;
using System.Text;

namespace shopBL
{
    public interface IPasswordHasher
    {
        byte[] Hash(string password, Guid salt);
        bool Verify(string password, Guid salt, byte[] expectedHash);
    }
}