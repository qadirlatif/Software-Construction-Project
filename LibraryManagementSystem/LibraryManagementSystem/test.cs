using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    internal class test
    {
        public bool admincheck(string a, string b)
        {
            //cycolmatic complexity value intitalling 1 
            bool z = false;
            if(a=="" && b == "")
            {
                z = false;
            }
            else if (a=="" || b == "")
            {
                z = false;

            }
            else if (a!="" && b != "")
            {
                z = true;
            }

            return z;

        }
    }
}
