using System;

namespace SkipList
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(45);
            SkipList<int> list = new SkipList<int>(random);

            list.Add(1);
            list.Add(3);
            list.Add(5);
            list.Add(4);
            list.Add(2);

            list.Remove(5); 
            
        }
    }
}