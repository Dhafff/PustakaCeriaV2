using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ManajemenStokBuku;

namespace ManajemenStokBuku.Tests
{
    [TestClass]
    public class ManajemenStokBukuTests
    {
        private ManajemenStokBuku manajemenStokBuku;
        private string testFilePath = "TestBuku.json";

        [TestInitialize]
        public void Setup()
        {
            manajemenStokBuku = new ManajemenStokBuku();
            typeof(ManajemenStokBuku).GetField("filePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(manajemenStokBuku, testFilePath);
            // Create an empty file for testing
            File.WriteAllText(testFilePath, "[]");
        }

        [TestCleanup]
        public void Teardown()
        {
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [TestMethod]
        public void LihatBuku_FileKosong_ReturnsEmptyList()
        {
            var result = manajemenStokBuku.LihatBuku();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TambahBuku_ValidBuku_BukuDitambahkan()
        {
            var buku = new Buku { Judul = "Test Judul", Penulis = "Test Penulis", Genre = "Test Genre" };
            manajemenStokBuku.TambahBuku(buku);

            var result = manajemenStokBuku.LihatBuku();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(buku.Judul, result[0].Judul);
        }

        [TestMethod]
        public void HapusBuku_BukuAda_BukuDihapus()
        {
            var buku = new Buku { Judul = "Test Judul", Penulis = "Test Penulis", Genre = "Test Genre" };
            manajemenStokBuku.TambahBuku(buku);
            manajemenStokBuku.HapusBuku(buku.Judul);

            var result = manajemenStokBuku.LihatBuku();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void EditBuku_BukuAda_BukuDiedit()
        {
            var buku = new Buku { Judul = "Test Judul", Penulis = "Test Penulis", Genre = "Test Genre" };
            manajemenStokBuku.TambahBuku(buku);

            var newBukuData = new Buku { Judul = "Test Judul", Penulis = "Penulis Baru", Genre = "Genre Baru" };
            manajemenStokBuku.EditBuku(buku.Judul, newBukuData);

            var result = manajemenStokBuku.LihatBuku();
            Assert.AreEqual("Penulis Baru", result[0].Penulis);
            Assert.AreEqual("Genre Baru", result[0].Genre);
        }

        [TestMethod]
        public void TambahBuku_IdIncrementCorrectly()
        {
            var buku1 = new Buku { Judul = "Buku 1", Penulis = "Penulis 1", Genre = "Genre 1" };
            var buku2 = new Buku { Judul = "Buku 2", Penulis = "Penulis 2", Genre = "Genre 2" };
            manajemenStokBuku.TambahBuku(buku1);
            manajemenStokBuku.TambahBuku(buku2);

            var result = manajemenStokBuku.LihatBuku();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);
        }
    }
}
