using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieHunt.ViewModels.Commands
{
    public class UnesiNovoOsobljeCommand
    {
        public AdminVM avm { get; set; }

        public UnesiNovoOsobljeCommand(AdminVM avm)
        {
            this.avm = avm;
        }

        public void UnesiNovoOsoblje()
        {


        }
    }
}
