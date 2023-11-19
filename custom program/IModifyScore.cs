using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custom_program
{
    //objects implementing this interface can modify player's score
    public interface IModifyScore
    {
        int Points
        {
            get;
        }
    }
}
