using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex12
{
    class TreeElement
    {
        public readonly int Data;
        public TreeElement Left;
        public TreeElement Right;

        public TreeElement(int data, TreeElement left = null, TreeElement right = null)
        {
            Data = data;
            Left = left;
            Right = right;
        }
    }
}
