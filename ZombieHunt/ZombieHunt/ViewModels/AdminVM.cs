using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieHunt.ViewModels.Commands;

namespace ZombieHunt.ViewModels
{
    public class AdminVM
    {


        public List<string> kategorije { get; set; }

        public UnesiNovoOsobljeCommand unesiNovoOsobljeCommand { get; set; }

        public AdminVM()
        {
            kategorije = new List<string>() { "Hired Gun", "Medic", "Driver", "Mechanic" };
            unesiNovoOsobljeCommand = new UnesiNovoOsobljeCommand(this);
        }

        public void UnesiNovoOsoblje()
        {
            
        }
    }
}
