using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PustakaCeriaV2.Menu
{
    public class Pengaturan
    {
        private static Pengaturan _instance;
        public static Pengaturan Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Pengaturan();
                }
                return _instance;
            }
        }

        private string _bahasaSaatIni = "Indonesia";

        // Private constructor to prevent direct instantiation
        private Pengaturan() { }

        public void TampilkanMenu()
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                if (_bahasaSaatIni == "Indonesia")
                {
                    Console.WriteLine("===== Menu Pengaturan =====");
                    Console.WriteLine("1. Ubah Bahasa");
                    Console.WriteLine("0. Kembali ke Menu Utama");
                    Console.WriteLine("===========================");
                }
                else if (_bahasaSaatIni == "English")
                {
                    Console.WriteLine("===== Settings Menu =====");
                    Console.WriteLine("1. Change Language");
                    Console.WriteLine("0. Return to Main Menu");
                    Console.WriteLine("===========================");
                }

                Console.WriteLine();
                int pilihMenu = BacaInputMenu(_bahasaSaatIni == "Indonesia" ? "Masukkan nomor menu yang dipilih: " : "Enter the menu number you selected: ");
                switch (pilihMenu)
                {
                    case 1:
                        UbahBahasa();
                        break;
                    case 0:
                        exitMenu = true;
                        break;
                    default:
                        if (_bahasaSaatIni == "Indonesia")
                        {
                            Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                        }
                        else if (_bahasaSaatIni == "English")
                        {
                            Console.WriteLine("Invalid choice. Please try again.");
                        }
                        break;
                }
            }
        }

        private void UbahBahasa()
        {
            if (_bahasaSaatIni == "Indonesia")
            {
                Console.WriteLine("===== Ubah Bahasa =====");
                Console.WriteLine("1. Bahasa Indonesia");
                Console.WriteLine("2. English");
                Console.WriteLine("=======================");
            }
            else if (_bahasaSaatIni == "English")
            {
                Console.WriteLine("===== Change Language =====");
                Console.WriteLine("1. Bahasa Indonesia");
                Console.WriteLine("2. English");
                Console.WriteLine("===========================");
            }

            Console.WriteLine();
            int pilihBahasa = BacaInputMenu(_bahasaSaatIni == "Indonesia" ? "Masukkan nomor bahasa yang dipilih: " : "Enter the selected language number: ");

            switch (pilihBahasa)
            {
                case 1:
                    _bahasaSaatIni = "Indonesia";
                    Console.WriteLine("Bahasa diubah ke Bahasa Indonesia.");
                    break;
                case 2:
                    _bahasaSaatIni = "English";
                    Console.WriteLine("Language switched to English.");
                    break;
                default:
                    if (_bahasaSaatIni == "Indonesia")
                    {
                        Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                    }
                    else if (_bahasaSaatIni == "English")
                    {
                        Console.WriteLine("Invalid choice. Please try again.");
                    }
                    break;
            }

            Console.WriteLine(_bahasaSaatIni == "Indonesia" ? $"Bahasa saat ini adalah: {_bahasaSaatIni}" : $"Current language is: {_bahasaSaatIni}");
            Console.WriteLine(_bahasaSaatIni == "Indonesia" ? "Tekan Enter untuk kembali ke menu pengaturan." : "Press Enter to return to the settings menu.");
            Console.ReadLine();
        }

        private int BacaInputMenu(string message)
        {
            int input;
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(_bahasaSaatIni == "Indonesia" ? "Input tidak valid. Mohon masukkan angka yang tersedia." : "Invalid input. Please enter a valid number.");
                }
            }
            return input;
        }

        public string GetBahasaSaatIni()
        {
            return _bahasaSaatIni;
        }
    }
}
