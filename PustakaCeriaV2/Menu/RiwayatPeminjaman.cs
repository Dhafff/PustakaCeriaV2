using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PustakaCeriaV2.Menu
{
    public class RiwayatPeminjaman
    {
        private List<Peminjaman> riwayatPeminjaman = new List<Peminjaman>();

        public RiwayatPeminjaman()
        {
            TambahRiwayat();
        }

        private void TambahRiwayat()
        {
            // Membuat tabel peminjaman
            string[,] dataPeminjaman = new string[,]
            {
                { "John Doe", "Introduction to Programming", "2024-04-01" },
                { "Jane Smith", "Data Structures and Algorithms", "2024-04-05" },
                { "Alice Johnson", "Database Management Systems", "2024-04-10" }
            };

            // Menambahkan entri riwayat peminjaman dari tabel
            for (int i = 0; i < dataPeminjaman.GetLength(0); i++)
            {
                string namaPeminjam = dataPeminjaman[i, 0];
                string judulBuku = dataPeminjaman[i, 1];
                DateTime tanggalPeminjaman = DateTime.Parse(dataPeminjaman[i, 2]);

                // Precondition: Tanggal peminjaman harus dalam rentang waktu yang masuk akal
                Contract.Requires(tanggalPeminjaman <= DateTime.Now, "Tanggal peminjaman tidak valid");

                // Postcondition: Setiap peminjaman yang ditambahkan harus memiliki judul buku yang tidak kosong
                Contract.Ensures(!string.IsNullOrEmpty(judulBuku), "Judul buku tidak boleh kosong");

                riwayatPeminjaman.Add(new Peminjaman(namaPeminjam, judulBuku, tanggalPeminjaman));
            }
        }

        public void PrintRiwayat()
        {
            Console.WriteLine("Riwayat Peminjaman Buku:");
            foreach (var peminjaman in riwayatPeminjaman)
            {
                Console.WriteLine($"Peminjam: {peminjaman.NamaPeminjam}, Buku: {peminjaman.JudulBuku}, Tanggal Peminjaman: {peminjaman.TanggalPeminjaman.ToShortDateString()}");
            }
        }
    }

    public class Peminjaman
    {
        public string NamaPeminjam { get; set; }
        public string JudulBuku { get; set; }
        public DateTime TanggalPeminjaman { get; set; }

        public Peminjaman(string namaPeminjam, string judulBuku, DateTime tanggalPeminjaman)
        {
            NamaPeminjam = namaPeminjam;

            // Precondition: Judul buku tidak boleh kosong
            if (string.IsNullOrEmpty(judulBuku))
            {
                throw new ArgumentException("Judul buku tidak boleh kosong");
            }

            JudulBuku = judulBuku;

            // Precondition: Tanggal peminjaman tidak boleh di masa depan
            if (tanggalPeminjaman > DateTime.Now)
            {
                throw new ArgumentException("Tanggal peminjaman tidak valid");
            }

            TanggalPeminjaman = tanggalPeminjaman;
        }
    }
}
