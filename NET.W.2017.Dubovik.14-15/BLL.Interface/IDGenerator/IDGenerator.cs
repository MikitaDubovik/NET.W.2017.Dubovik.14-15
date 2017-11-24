using System;

namespace BLL.Interface.IDGenerator
{
    public class IdGenerator : IIdGenerator
    {
        public string GenerateAccountId() => Guid.NewGuid().ToString();
    }
}
