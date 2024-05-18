using PustakaCeriaV2.Menu;
internal class Program
{
    private static void Main(string[] args)
    {
        // ... Instance per kelas
        ManajemenStokBuku.ManajemenStokBuku manajemenBuku = new ManajemenStokBuku.ManajemenStokBuku();
        ManajemenAnggota manajemenAnggota = new ManajemenAnggota();
        NotifikasiKeterlambatanPengembalian notifikasi = new NotifikasiKeterlambatanPengembalian();
        PeminjamanPengembalianBuku peminjamanBuku = new PeminjamanPengembalianBuku();
        PencarianBuku cariBuku = new PencarianBuku();
        Pengaturan pengaturan = Pengaturan.Instance;
        RiwayatPeminjaman riwayat = new RiwayatPeminjaman();

        // ... Ambil pesan welcome
        string welcomeMsg = GetWelcomeMessage();


        Console.WriteLine(welcomeMsg);

        Console.WriteLine();

        bool exitReq = false;

        while (!exitReq)
        {
            string bahasa = pengaturan.GetBahasaSaatIni();
            if (bahasa == "Indonesia")
            {
                Console.WriteLine($"Bahasa saat ini adalah: {bahasa}");
                Console.WriteLine("===== Menu Utama =====");
                Console.WriteLine("1. Pencarian Buku");
                Console.WriteLine("2. Peminjaman dan Pengembalian Buku");
                Console.WriteLine("3. Manajemen Anggota Perpustakaan");
                Console.WriteLine("4. Riwayat Peminjaman");
                Console.WriteLine("5. Notifikasi Keterlambatan Pengembalian");
                Console.WriteLine("6. Statistik Penggunaan Buku");
                Console.WriteLine("7. Manajemen Stok Buku");
                Console.WriteLine("8. Pengaturan Aplikasi");
                Console.WriteLine("0. Keluar");
            }
            else if (bahasa == "English")
            {
                Console.WriteLine($"Current Language: {bahasa}");
                Console.WriteLine("===== Main Menu =====");
                Console.WriteLine("1. Book Search");
                Console.WriteLine("2. Book Borrowing and Returning");
                Console.WriteLine("3. Library Member Management");
                Console.WriteLine("4. Borrowing History");
                Console.WriteLine("5. Overdue Notification");
                Console.WriteLine("6. Book Usage Statistics");
                Console.WriteLine("7. Book Stock Management");
                Console.WriteLine("8. Application Settings");
                Console.WriteLine("0. Exit");
            }
            var bahasaSaatIni = Pengaturan.Instance.GetBahasaSaatIni();

            string pesanInput;
            if (bahasaSaatIni == "Indonesia")
            {
                pesanInput = "Masukkan nomor menu yang dipilih: ";
            }
            else if (bahasaSaatIni == "English")
            {
                pesanInput = "Enter the number of the selected menu: ";
            }
            else
            {
                // Bahasa default jika diperlukan
                pesanInput = "Masukkan nomor menu yang dipilih: ";
            }

            int pilih = BacaInputMenu(pesanInput);
            // Console.Clear();
            switch (pilih)
            {
                case 1:
                    // ... Menu pencarian buku
                    Console.WriteLine("===== Menu Pencarian Buku =====");
                    Console.WriteLine();
                    // ... Class menu pencarian buku
                    Console.WriteLine();
                    Console.WriteLine("===============================");
                    Console.WriteLine();
                    Console.WriteLine("Tekan tombol apa pun untuk kembali ke menu utama.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 2:
                    // ... Menu peminjaman dan pengembalian buku
                    Console.WriteLine("===== Menu Peminjaman dan Pengembalian Buku =====");
                    Console.WriteLine();
                    // ... Class menu peminjaman dan pengembalian buku
                    Console.WriteLine();
                    Console.WriteLine("=================================================");
                    Console.WriteLine();
                    Console.WriteLine("Tekan tombol apa pun untuk kembali ke menu utama.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 3:
                    // ... Menu manajemen anggota
                    Console.WriteLine("===== Menu Manajemen Anggota Perpustakaan =====");
                    Console.WriteLine();
                    manajemenAnggota.TampilMenu();
                    Console.WriteLine();
                    Console.WriteLine("===============================================");
                    Console.WriteLine();
                    Console.WriteLine("Tekan tombol apa pun untuk kembali ke menu utama.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 4:
                    // ... Menu riwayat peminjaman
                    Console.WriteLine("===== Menu Riwayat Peminjaman =====");
                    Console.WriteLine();
                    riwayat.PrintRiwayat();
                    Console.WriteLine();
                    Console.WriteLine("===================================");
                    Console.WriteLine();
                    Console.WriteLine("Tekan tombol apa pun untuk kembali ke menu utama.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 5:
                    // ... Menu notifikasi keterlambatan
                    Console.WriteLine("===== Menu Notifikasi Keterlambatan Peminjaman =====");
                    Console.WriteLine();
                    // ... Class menu notifikasi keterlambatan
                    Console.WriteLine();
                    Console.WriteLine("====================================================");
                    Console.WriteLine();
                    Console.WriteLine("Tekan tombol apa pun untuk kembali ke menu utama.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 6:
                    // ... Menu statistik penggunaan buku
                    Console.WriteLine("===== Menu Statistik Penggunaan Buku =====");
                    Console.WriteLine();
                    StatistikPenggunaanBuku<string>.MainMenu();
                    Console.WriteLine();
                    Console.WriteLine("==========================================");
                    Console.WriteLine();
                    Console.WriteLine("Tekan tombol apa pun untuk kembali ke menu utama.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 7:
                    // ... Menu manajemen stok buku
                    Console.Clear();
                    manajemenBuku.TampilkanMenu();
                    Console.WriteLine();
                    Console.WriteLine("Tekan tombol apa pun untuk kembali ke menu utama.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 8:
                    // ... Menu pengaturan aplikasi
                    Console.WriteLine("===== Menu Pengaturan Aplikasi =====");
                    Console.WriteLine();
                    pengaturan.TampilkanMenu();
                    Console.WriteLine();
                    Console.WriteLine("====================================");
                    Console.WriteLine();
                    Console.WriteLine("Tekan tombol apa pun untuk kembali ke menu utama.");


                    break;
                case 0:
                    // ... Keluar aplikasi
                    exitReq = true;
                    Console.Clear();
                    break;
                default:
                    // ... Jika menu tidak ada
                    Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                    Console.Clear();
                    break;
            }
        }
    }

    static string GetWelcomeMessage()
    {
        DateTime waktuSekarang = DateTime.Now;

        string ucapan = "Selamat ";
        if (waktuSekarang.Hour < 12)
        {
            ucapan += "pagi";
        }
        else if (waktuSekarang.Hour < 15)
        {
            ucapan += "siang";
        }
        else if (waktuSekarang.Hour < 18)
        {
            ucapan += "sore";
        }
        else
        {
            ucapan += "malam";
        }

        return $"{ucapan} dan selamat datang di Pustaka Ceria!";
    }

    static int BacaInputMenu(string message)
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
                Console.WriteLine("Input tidak valid. Mohon masukkan angka yang tersedia.");
            }
        }
        return input;
    }
}