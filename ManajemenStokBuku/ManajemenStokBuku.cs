using Newtonsoft.Json;
using System.Diagnostics.Contracts;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace ManajemenStokBuku
{
    public class ManajemenStokBuku
    {
        private string filePath = "../../../Buku.json";

        public List<Buku> LihatBuku()
        {
            return LoadBuku();
        }

        public void TambahBuku(Buku buku)
        { 
            List<Buku> bukus = LoadBuku();

            int highestId = bukus.Count > 0 ? bukus.Max(b => b.Id) : 0;
            buku.Id = highestId + 1;

            bukus.Add(buku);
            SaveBuku(bukus);

            Contract.Ensures(LihatBuku().Contains(buku), "Buku harus ditambahkan");
        }

        private Buku InputBuku()
        {
            string judul, penulis, genre;

            do
            {
                Console.Write("Judul: ");
                judul = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(judul))
                {
                    Console.WriteLine("Judul tidak boleh kosong atau hanya berisi spasi. Silakan coba lagi.");
                }
            } while (string.IsNullOrWhiteSpace(judul));

            do
            {
                Console.Write("Penulis: ");
                penulis = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(penulis))
                {
                    Console.WriteLine("Penulis tidak boleh kosong atau hanya berisi spasi. Silakan coba lagi.");
                }
            } while (string.IsNullOrWhiteSpace(penulis));

            do
            {
                Console.Write("Genre: ");
                genre = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(genre))
                {
                    Console.WriteLine("Genre tidak boleh kosong atau hanya berisi spasi. Silakan coba lagi.");
                }
            } while (string.IsNullOrWhiteSpace(genre));

            return new Buku { Judul = judul, Penulis = penulis, Genre = genre };
        }

        public void HapusBuku(string judul)
        {
            List<Buku> bukus = LoadBuku();
            Buku bukuToRemove = bukus.Find(b => b.Judul.Equals(judul));
            if (bukuToRemove != null)
            {
                bukus.Remove(bukuToRemove);
                SaveBuku(bukus);
            }
            else
            {
                Console.WriteLine("Buku tidak ditemukan.");
                Console.WriteLine();
            }
        }

        public void EditBuku(string judul, Buku newBukuData)
        {
            List<Buku> bukus = LoadBuku();
            Buku bukuToEdit = bukus.Find(b => b.Judul.Equals(judul));
            if (bukuToEdit != null)
            {
                bukuToEdit.Penulis = newBukuData.Penulis;
                bukuToEdit.Genre = newBukuData.Genre;
                SaveBuku(bukus);
            }
            else
            {
                Console.WriteLine("Buku tidak ditemukan.");
                Console.WriteLine();
            }
        }

        private List<Buku> LoadBuku()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    List<Buku> bukus = JsonConvert.DeserializeObject<List<Buku>>(json);
                    return bukus ?? new List<Buku>();
                }
                else
                {
                    Console.WriteLine("File Buku.json tidak ditemukan.");
                    Console.WriteLine();
                    return new List<Buku>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan saat membaca file: {ex.Message}");
                Console.WriteLine();
                return new List<Buku>();
            }
        }

        private void SaveBuku(List<Buku> bukus)
        {
            try
            {
                string json = JsonConvert.SerializeObject(bukus, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan saat menyimpan file: {ex.Message}");
                Console.WriteLine();
            }
        }


        public void TampilkanMenu()
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Console.WriteLine("===== Menu Manejemen Stok Buku =====");
                Console.WriteLine("1. Lihat Daftar Buku");
                Console.WriteLine("2. Tambah Buku Baru");
                Console.WriteLine("3. Hapus Buku");
                Console.WriteLine("4. Edit Informasi Buku");
                Console.WriteLine("0. Kembali ke Menu Utama");
                Console.WriteLine("=====================================");
                Console.WriteLine();

                int pilihMenu = BacaInputMenu("Masukkan nomor menu yang dipilih: ");
                switch (pilihMenu)
                {
                    case 1:
                        // Lihat buku
                        Console.WriteLine();
                        List<Buku> daftarBuku = LihatBuku();
                        if (daftarBuku.Count > 0)
                        {
                            foreach (var buku in daftarBuku)
                            {
                                Console.WriteLine($"No: {buku.Id}");
                                Console.WriteLine($"Judul: {buku.Judul}");
                                Console.WriteLine($"Penulis: {buku.Penulis}");
                                Console.WriteLine($"Genre: {buku.Genre}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Daftar buku kosong.");
                            Console.WriteLine();
                        }
                        break;
                    case 2:
                        // Menambah buku baru
                        Console.WriteLine();
                        Console.WriteLine("Masukkan informasi buku baru");
                        Buku bukuBaru = InputBuku();
                        TambahBuku(bukuBaru);
                        Console.WriteLine("Buku berhasil ditambahkan.");
                        Console.WriteLine();
                        break;
                    case 3:
                        // Menghapus buku
                        Console.WriteLine();
                        Console.Write("Masukkan judul buku yang akan dihapus: ");
                        string judulBukuDihapus = Console.ReadLine();
                        HapusBuku(judulBukuDihapus);
                        Console.WriteLine("Buku berhasil dihapus.");
                        Console.WriteLine();
                        break;
                    case 4:
                        // Mengedit informasi buku
                        Console.WriteLine();
                        Console.Write("Masukkan judul buku yang akan diedit: ");
                        string judulBukuDiubah = Console.ReadLine();
                        Console.WriteLine("Masukkan informasi baru untuk buku");
                        Buku dataBukuBaru = InputBuku();
                        EditBuku(judulBukuDiubah, dataBukuBaru);
                        Console.WriteLine("Informasi buku berhasil diperbarui.");
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

    public class Buku
    {
        public int Id { get; set; }
        public required string Judul { get; set; }
        public required string Penulis { get; set; }
        public required string Genre { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Judul: {Judul}, Penulis: {Penulis}, Genre: {Genre}";
        }
    }
}
