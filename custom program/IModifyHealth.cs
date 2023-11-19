using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custom_program
{
    //objects implementing this interface can modify player's health
    public interface IModifyHealth
    {
        int Hp
        {
            get;
        }
    }
}
