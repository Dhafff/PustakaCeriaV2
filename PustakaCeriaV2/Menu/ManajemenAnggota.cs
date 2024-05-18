using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PustakaCeriaV2.Menu
{
    public class ManajemenAnggota
    {
        private string filePath = "../../../Anggota.json";

        public List<Anggota> LihatAnggota()
        {
            return LoadAnggota();
        }

        public void TambahAnggota(Anggota anggota)
        {
            Contract.Requires<ArgumentNullException>(anggota != null, "Data tidak boleh kosong");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(anggota.Nama), "Nama tidak boleh kosong");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(anggota.Nohp), "No HP tidak boleh kosong");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(anggota.Alamat), "Alamat buku tidak boleh kosong");

            List<Anggota> anggotas = LoadAnggota();
            anggotas.Add(anggota);
            SaveAnggota(anggotas);

            Contract.Ensures(LihatAnggota().Contains(anggota), "Data harus ditambahkan");
        }

        private Anggota InputAnggota()
        {
            Console.Write("Nama: ");
            string nama = Console.ReadLine();
            Console.Write("Nohp: ");
            string nohp = Console.ReadLine();
            Console.Write("Alamat: ");
            string alamat = Console.ReadLine();

            return new Anggota { Nama = nama, Nohp = nohp, Alamat = alamat };
        }

        public void HapusAnggota(string nama)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(nama), "Nama tidak boleh kosong");

            List<Anggota> anggotas = LoadAnggota();
            Anggota anggotaToRemove = anggotas.Find(a => a.Nama.Equals(nama));
            if (anggotaToRemove != null)
            {
                anggotas.Remove(anggotaToRemove);
                SaveAnggota(anggotas);
            }
            else
            {
                Console.WriteLine("Data tidak ditemukan.");
                Console.WriteLine();
            }
        }

        public void EditAnggota(string nama, Anggota newDataAnggota)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(nama), "Data  tidak boleh kosong");
            Contract.Requires<ArgumentNullException>(newDataAnggota != null, "Data baru tidak boleh kosong");

            List<Anggota> anggotas = LoadAnggota();
            Anggota anggotaToEdit = anggotas.Find(b => b.Nama.Equals(nama));
            if (anggotaToEdit != null)
            {
                anggotaToEdit.Nama = newDataAnggota.Nama;
                anggotaToEdit.Alamat = newDataAnggota.Alamat;
                SaveAnggota(anggotas);
            }
            else
            {
                Console.WriteLine("Data tidak ditemukan.");
                Console.WriteLine();
            }
        }

        private List<Anggota> LoadAnggota()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<Anggota> anggotas = JsonConvert.DeserializeObject<List<Anggota>>(json);
                for (int i = 0; i < anggotas.Count; i++)
                {
                    anggotas[i].Id = i + 1;
                }
                return anggotas;
            }
            else
            {
                Console.WriteLine("File Anggota.json tidak ditemukan.");
                Console.WriteLine();
                return new List<Anggota>();
            }
        }

        private void SaveAnggota(List<Anggota> anggotas)
        {
            string json = JsonConvert.SerializeObject(anggotas, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void TampilMenu()
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Console.WriteLine("===== Menu Manejemen Anggota =====");
                Console.WriteLine("1. Lihat Daftar Anggota");
                Console.WriteLine("2. Tambah Anggota Baru");
                Console.WriteLine("3. Hapus Anggota");
                Console.WriteLine("4. Edit Informasi Anggota");
                Console.WriteLine("0. Kembali ke Menu Utama");
                Console.WriteLine("=====================================");
                Console.WriteLine();

                int pilihMenu = BacaInputMenu("Masukkan nomor menu yang dipilih: ");
                switch (pilihMenu)
                {
                    case 1:
                        // Lihat anggota
                        Console.WriteLine();
                        List<Anggota> daftarAnggota = LihatAnggota();
                        if (daftarAnggota.Count > 0)
                        {
                            foreach (var anggota in daftarAnggota)
                            {
                                Console.WriteLine($"No: {anggota.Id}");
                                Console.WriteLine($"Nama: {anggota.Nama}");
                                Console.WriteLine($"Nohp: {anggota.Nohp}");
                                Console.WriteLine($"Alamat: {anggota.Alamat}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Daftar anggota kosong.");
                            Console.WriteLine();
                        }
                        break;
                    case 2:
                        // Menambah anggota baru
                        Console.WriteLine();
                        Console.WriteLine("Masukkan informasi anggota baru");
                        Anggota anggotaBaru = InputAnggota();
                        TambahAnggota(anggotaBaru);
                        Console.WriteLine("Data berhasil ditambahkan.");
                        Console.WriteLine();
                        break;
                    case 3:
                        // Menghapus anggota
                        Console.WriteLine();
                        Console.Write("Masukkan nama anggota yang akan dihapus: ");
                        string namaAnggotaDihapus = Console.ReadLine();
                        HapusAnggota(namaAnggotaDihapus);
                        Console.WriteLine("Anggota berhasil dihapus.");
                        Console.WriteLine();
                        break;
                    case 4:
                        // Edit informasi anggota
                        Console.WriteLine();
                        Console.Write("Masukkan nama anggota yang akan diedit: ");
                        string namaAnggotaDiubah = Console.ReadLine();
                        Console.WriteLine("Masukkan informasi baru untuk anggota");
                        Anggota dataAnggotaBaru = InputAnggota();
                        EditAnggota(namaAnggotaDiubah, dataAnggotaBaru);
                        Console.WriteLine("Informasi anggota berhasil diperbarui.");
                        Console.WriteLine();
                        break;
                    case 0:
                        exitMenu = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                        Console.WriteLine();
                        break;
                }
            }
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
                    Console.WriteLine();
                    Console.WriteLine("Input tidak valid. Mohon masukkan angka yang tersedia.");
                    Console.WriteLine();
                }
            }
            return input;
        }
    }
    public class Anggota
    {
        public int Id { get; set; }
        public required string Nama { get; set; }
        public required string Nohp { get; set; }
        public required string Alamat { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Nama: {Nama}, Nohp: {Nohp}, Alamat: {Alamat}";
        }
    }
}
