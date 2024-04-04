using System;
using Project;

namespace GraphTheory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var requirement1 = new Requirement1("Requirement1/windmill.txt");
            requirement1.Implement();

            var requirement2 = new Requirement_2("Requirement2/Matrix_2.txt");
            requirement2.Connection();

            var requirement3 = new Requirement_3("Requirement3/Matrix_3.txt");
            Console.Write("Dinh bat dau cua giai thuat Prim: ");
            int Source = int.Parse(Console.ReadLine());
            requirement3.Prim(Source);
            requirement3.KruskalAL();
        }
    }
}