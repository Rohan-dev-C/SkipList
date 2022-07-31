using System;
using System.Collections.Generic;
using System.Text;

namespace SkipList
{
    interface IMyInterface
    {
        int Number { get; set; }
        void DoStuff();
    }

    class TestClass : IMyInterface
    {
        public int Number { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void DoStuff()
        {
            throw new NotImplementedException();
        }
    }
}
