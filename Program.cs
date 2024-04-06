using System;
using System.Text;
using Project;

namespace GraphTheory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            initializeProgram();

        }

        public static void initializeProgram() {
            Console.WriteLine("ĐÒ ÁN MÔN HỌC NHÓM 4");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Vui lòng chọn yêu cầu");
            Console.WriteLine("Nhấm 1 - Yêu cầu 1: Nhận diện một số dạng đồ thị đặc biệt");
            Console.WriteLine("Nhấm 2 - Yêu cầu 2: Xác định thành phần liên thông mạnh");
            Console.WriteLine("Nhấm 3 - Yêu cầu 3: Tìm cây khung nhỏ nhất");
            Console.WriteLine("Nhấm 4 - Yêu cầu 4: Tìm đường đi ngắn nhấ");
            Console.WriteLine("Nhấm 5 - Yêu cầu 5: Tìm chu trình hoặc đường đi Euler");
            _ = int.TryParse(Console.ReadLine(), out int index);
            while (index < 1 || index > 5) {
                Console.WriteLine("Vui lòng nhập từ 1 - 5");
                _ = int.TryParse(Console.ReadLine(), out index);
            }
            switch (index) {
                case 1:
                    Requirement1();
                    break;
                case 2:
                    Requirement2();
                    break;
                case 3:
                    Requirement3();
                    break;
                case 4:
                    Requirement4();
                    break;
                case 5:
                    Requirement5();
                    break;
            }
        }

        public static void Requirement1() {
            var requirement1 = new Requirement1("Requirement1/windmill.txt");
            requirement1.Implement();
        }
        public static void Requirement2() {
            var requirement2 = new Requirement_2("Requirement2/Matrix_2.txt");
            requirement2.Connection();
        }
        public static void Requirement3() {
            var requirement3 = new Requirement_3("Requirement3/Matrix_3.txt");
            Console.Write("Dinh bat dau cua giai thuat Prim: ");
            int Source = int.Parse(Console.ReadLine());
            requirement3.Prim(Source);
            requirement3.KruskalAL();
        }
        public static void Requirement4() 
        {
            var requirement4 = new Requirement4();
            requirement4.Floyd("Requirement4/Matrix_4.txt");
        }
        public static void Requirement5() { }
    }
}
