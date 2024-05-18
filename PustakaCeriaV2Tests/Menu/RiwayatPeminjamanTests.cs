using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using PustakaCeriaV2.Menu;

namespace PustakaCeriaV2Tests.Menu
{
    [TestClass]
    public class RiwayatPeminjamanTests
    {
        [TestMethod]
        public void TambahRiwayat_ShouldAddCorrectEntries()
        {
            // Arrange
            RiwayatPeminjaman riwayat = new RiwayatPeminjaman();

            // Act
            var result = GetRiwayatPeminjaman(riwayat);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("John Doe", result[0].NamaPeminjam);
            Assert.AreEqual("Introduction to Programming", result[0].JudulBuku);
            Assert.AreEqual(new DateTime(2024, 4, 1), result[0].TanggalPeminjaman);

            Assert.AreEqual("Jane Smith", result[1].NamaPeminjam);
            Assert.AreEqual("Data Structures and Algorithms", result[1].JudulBuku);
            Assert.AreEqual(new DateTime(2024, 4, 5), result[1].TanggalPeminjaman);

            Assert.AreEqual("Alice Johnson", result[2].NamaPeminjam);
            Assert.AreEqual("Database Management Systems", result[2].JudulBuku);
            Assert.AreEqual(new DateTime(2024, 4, 10), result[2].TanggalPeminjaman);
        }

        [TestMethod]
        public void Peminjaman_Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            string namaPeminjam = "John Doe";
            string judulBuku = "Introduction to Programming";
            DateTime tanggalPeminjaman = new DateTime(2024, 4, 1);

            // Act
            Peminjaman peminjaman = new Peminjaman(namaPeminjam, judulBuku, tanggalPeminjaman);

            // Assert
            Assert.AreEqual(namaPeminjam, peminjaman.NamaPeminjam);
            Assert.AreEqual(judulBuku, peminjaman.JudulBuku);
            Assert.AreEqual(tanggalPeminjaman, peminjaman.TanggalPeminjaman);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Judul buku tidak boleh kosong")]
        public void Peminjaman_Constructor_ShouldThrowException_ForEmptyJudulBuku()
        {
            // Arrange
            string namaPeminjam = "John Doe";
            string judulBuku = "";
            DateTime tanggalPeminjaman = new DateTime(2024, 4, 1);

            // Act
            Peminjaman peminjaman = new Peminjaman(namaPeminjam, judulBuku, tanggalPeminjaman);

            // Assert handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Tanggal peminjaman tidak valid")]
        public void Peminjaman_Constructor_ShouldThrowException_ForFutureTanggalPeminjaman()
        {
            // Arrange
            string namaPeminjam = "John Doe";
            string judulBuku = "Introduction to Programming";
            DateTime tanggalPeminjaman = DateTime.Now.AddDays(1); // Tanggal di masa depan

            // Act
            Peminjaman peminjaman = new Peminjaman(namaPeminjam, judulBuku, tanggalPeminjaman);

            // Assert handled by ExpectedException
        }

        private List<Peminjaman> GetRiwayatPeminjaman(RiwayatPeminjaman riwayat)
        {
            // Helper method to extract the private field for testing purposes
            var field = typeof(RiwayatPeminjaman).GetField("riwayatPeminjaman", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (List<Peminjaman>)field.GetValue(riwayat);
        }
    }
}
