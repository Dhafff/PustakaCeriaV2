using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PustakaCeriaV2.Menu
{
    public class StatistikPenggunaanBuku<T>
    {
        private Dictionary<T, int> rules = new Dictionary<T, int>();

        public void SetRule(T item, int frequency)
        {
            rules[item] = frequency;
        }

        public Dictionary<T, int> HitungFrekuensi(List<T> data)
        {
            // Precondition: data harus tidak null
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Data tidak boleh null.");
            }

            // Inisialisasi frekuensi
            Dictionary<T, int> frekuensi = new Dictionary<T, int>();

            // Hitung frekuensi
            foreach (T item in data)
            {
                if (frekuensi.TryGetValue(item, out int count))
                {
                    frekuensi[item] = count + rules.GetValueOrDefault(item, 1);
                }
                else
                {
                    frekuensi[item] = rules.GetValueOrDefault(item, 1);
                }
            }

            // Postcondition: jumlah entri dalam hasil harus sama dengan jumlah entri dalam data
            if (frekuensi.Count != data.Count)
            {
                // Jika jumlah entri dalam hasil tidak sama dengan jumlah entri dalam data, sesuaikan hasil
                foreach (T item in data)
                {
                    if (!frekuensi.ContainsKey(item))
                    {
                        frekuensi[item] = 0;
                    }
                }
            }

            return frekuensi;
        }

        public void PrintStatistik(Dictionary<T, int> statistik)
        {
            Console.WriteLine("Statistik Penggunaan Buku:");
            foreach (var entry in statistik)
            {
                Console.WriteLine($"Item: {entry.Key}, Frekuensi: {entry.Value}");
            }
        }

        public static List<string> GetGenres()
        {
            List<string> genres = new List<string>
        {
            "Fiksi", "Fiksi", "Fiksi", "Fiksi", "Fiksi",
            "Non-Fiksi", "Non-Fiksi", "Non-Fiksi", "Non-Fiksi", "Non-Fiksi"
        };
            return genres;
        }

        public static void SetRuntimeConfiguration(StatistikPenggunaanBuku<string> statistik)
        {
            var genres = GetGenres();
            foreach (var genre in genres)
            {
                statistik.SetRule("Fiksi", 2); // Menetapkan aturan frekuensi untuk genre Fiksi
                statistik.SetRule("Non-Fiksi", 1);
            }
        }

        public static void MainMenu()
        {
            StatistikPenggunaanBuku<string> statistik = new StatistikPenggunaanBuku<string>();
            SetRuntimeConfiguration(statistik);

            List<string> data = new List<string>
            {
                "Fiksi", "Fiksi", "Fiksi", "Fiksi", "Fiksi",
                "Non-Fiksi", "Non-Fiksi", "Non-Fiksi", "Non-Fiksi", "Non-Fiksi"
            };
            Dictionary<string, int> frekuensi = statistik.HitungFrekuensi(data);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Lihat Statistik");
                Console.WriteLine("2. Keluar");
                Console.Write("Pilih opsi: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Precondition: genres tidak boleh null
                        Contract.Requires(data != null);

                        // Hitung statistik
                        Dictionary<string, int> frekuensiBuku = statistik.HitungFrekuensi(data);

                        // Postcondition: jumlah entri dalam hasil harus sama dengan jumlah entri dalam data
                        Contract.Ensures(frekuensiBuku.Count == data.Count);

                        // Menampilkan hasil statistik
                        Console.WriteLine("Statistik Penggunaan Buku:");
                        foreach (var kvp in frekuensiBuku)
                        {
                            Console.WriteLine($"Genre '{kvp.Key}': {kvp.Value} buku.");
                        }
                        break;
                    case "2":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opsi tidak valid. Silakan pilih lagi.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
