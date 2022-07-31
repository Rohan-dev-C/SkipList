using System;

namespace SkipList
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(45);
            SkipList<int> list = new SkipList<int>(random);

            list.Insert(1);
            list.Insert(2); 
        }
    }
}