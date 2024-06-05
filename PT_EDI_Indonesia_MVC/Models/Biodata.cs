using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PT_EDI_Indonesia_MVC.Models
{
    public class Biodata
    {
        public int Id { get; set; }
        public string PosisiDilamar { get; set; }
        public string Nama { get; set; }
        public string NomorKTP { get; set; }
        public string TempatLahir { get; set; }
        public DateOnly TanggalLahir { get; set; }
        public JenisKelamin JenisKelamin { get; set; }
        public string Agama { get; set; }
        public string GolonganDarah { get; set; }
        public string Status { get; set; }
        public string AlamatKTP { get; set; }
        public string AlamatTinggal { get; set; }
        public string Email { get; set; }
        public string NoTelepon { get; set; }
        public string KontakOrangTerdekat { get; set; }
        public string Skill { get; set; }
        public bool BersediaDitempatkan { get; set; }
        public decimal PenghasilanDiharapkan { get; set; }

    }
}

public enum JenisKelamin
{
    Pria,
    Wanita
}