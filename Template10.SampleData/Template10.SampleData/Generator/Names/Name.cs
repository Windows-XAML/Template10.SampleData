using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template10.SampleData.Generator.Names
{
    public class Name
    {
        internal Name(string name)
        {
            FullName = name;
            FirstName = name.Split(' ')[0];
            LastName = name.Split(' ')[1];
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string FullName { get; }

        public override string ToString()
        {
            return FullName;
        }
    }
}
