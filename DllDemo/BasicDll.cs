using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllDemo
{
    public class BasicDll
    {
        int _i;
         public BasicDll(int i)
        {
            _i =0- i;
        }

        public BasicDll()
        {
            
        }


        public int CountFirst()
        {
            return 100;
        }
        public override string ToString()
        {
            return _i.ToString();
        }

        public string ToString(int i)
        {
            return i.ToString();
        }

        public string ToString(int i,string s)
        {
            return i.ToString()+s;
        }
    }
}
