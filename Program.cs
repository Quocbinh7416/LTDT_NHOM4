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
            Console.WriteLine("ĐÒ ÁN MÔN HỌC NHÓM P14");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Vui lòng chọn yêu cầu");
            Console.WriteLine("Nhập 1 - Yêu cầu 1: Nhận diện một số dạng đồ thị đặc biệt");
            Console.WriteLine("Nhập 2 - Yêu cầu 2: Xác định thành phần liên thông mạnh");
            Console.WriteLine("Nhập 3 - Yêu cầu 3: Tìm cây khung lớn nhất");
            Console.WriteLine("Nhập 4 - Yêu cầu 4: Tìm đường đi ngắn nhất");
            Console.WriteLine("Nhập 5 - Yêu cầu 5: Tìm chu trình hoặc đường đi Euler");
            _ = int.TryParse(Console.ReadLine(), out int index);
            while (index < 1 || index > 5) {
                Console.WriteLine("Vui lòng nhập từ 1 - 5");
                _ = int.TryParse(Console.ReadLine(), out index);
            }
            Console.WriteLine("Nhập tên đồ thị (Ví dụ barbell.txt)");
            string graphName = Console.ReadLine();
            switch (index) {
                case 1:
                    graphName = "Requirement1/" + graphName;
                    Requirement1(graphName);
                    break;
                case 2:
                    graphName = "Requirement2/" + graphName;
                    Requirement2(graphName);
                    break;
                case 3:
                    graphName = "Requirement3/" + graphName;
                    Requirement3(graphName);
                    break;
                case 4:
                    graphName = "Requirement5/" + graphName;
                    Requirement4(graphName);
                    break;
                case 5:
                    graphName = "Requirement5/" + graphName;
                    Requirement5(graphName);
                    break;
            }

            Console.WriteLine("Tiếp tục kiểm tra ?");
            Console.WriteLine("1 - Tiếp tục");
            Console.WriteLine("2 - Dừng");
            _ = int.TryParse(Console.ReadLine(), out int index2);
            while (index2 < 1 || index2 > 2) {
                Console.WriteLine("Vui lòng nhập từ 1 - 2");
                _ = int.TryParse(Console.ReadLine(), out index);
            }
            if (index2 == 1) initializeProgram();

        }

        public static void Requirement1(string graphName) {
            var requirement1 = new Requirement1(graphName);
            requirement1.Implement();
        }
        public static void Requirement2(string graphName) {
            var requirement2 = new Requirement_2(graphName);
            requirement2.Connection();
        }
        public static void Requirement3(string graphName) {
            var requirement3 = new Requirement_3(graphName);
            Console.Write("Dinh bat dau cua giai thuat Prim: ");
            int Source = int.Parse(Console.ReadLine());
            requirement3.Prim(Source);
            requirement3.KruskalAL();
        }
        public static void Requirement4(string graphName) 
        {
            var requirement4 = new Requirement4();
            requirement4.Floyd(graphName);
        }
        public static void Requirement5(string graphName) {
            var requirement5 = new Requirement5(graphName);
            requirement5.KiemTraEuler();
        }
    }
}
