using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PassAway.Models
{
    public interface IRepository
    {
        IEnumerable<Register> RegisterRecords { get; }
        void AddRegisters(Register register);
    }
}
