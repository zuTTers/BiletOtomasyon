﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace BiletOtomasyon
{
    public class VeriTabani
    {
        public SqlConnection baglan()
        {
            SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["BiletOtomasyonConnectionString"].ConnectionString);
            return baglanti;
        }
        public DataTable tabloCagir(string tabloAd)
        {
            string sorgu = "select * from " + tabloAd;
            SqlConnection baglan = this.baglan();
            DataTable dt = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter(sorgu, baglan);
            try
            {
                adaptor.Fill(dt);
            }
            catch (Exception)
            {

                throw;
            }
            adaptor.Dispose();
            baglan.Close();
            return dt;

        }
        public int BiletIptal(bool BiletNo)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            komut.CommandText = "update Biletler set AktifMi=0 where BiletNo=@BiletNo";
            komut.Parameters.AddWithValue("@BiletNo", BiletNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            baglan.Close();
            komut.Dispose();
            return sonuc;
        }
        public int YolcuEkleDuzenle(int YolcuNo, string TC, string Ad, string Soyad, string TelefonNo, string Mail, bool Cinsiyet)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            if (YolcuNo > 0)
                komut.CommandText = "update Yolcular set TC=@TC, Ad=@Ad, Soyad=@Soyad, TelefonNo=@TelefonNo, Mail=@Mail, Cinsiyet=@Cinsiyet where YolcuNo=@YolcuNo";
            else
                komut.CommandText = "insert into Yolcular(TC, Ad, Soyad, TelefonNo, Mail, Cinsiyet) values (@TC, @Ad, @Soyad, @TelefonNo, @Mail, @Cinsiyet)";
            komut.Parameters.AddWithValue("@TC", TC);
            komut.Parameters.AddWithValue("@Ad", Ad);
            komut.Parameters.AddWithValue("@Soyad", Soyad);
            komut.Parameters.AddWithValue("@TelefonNo", TelefonNo);
            komut.Parameters.AddWithValue("@Mail", Mail);
            komut.Parameters.AddWithValue("@Cinsiyet", Cinsiyet);
            if (YolcuNo > 0)
                komut.Parameters.AddWithValue("@YolcuNo", YolcuNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
                if (YolcuNo <= 0)
                {
                    komut.CommandText = "select @@IDENTITY as sonuc order by sonuc desc";
                    SqlDataReader dr = komut.ExecuteReader();
                    dr.Read();
                    sonuc = Convert.ToInt32(dr["sonuc"]);
                    dr.Close();
                }
                else if (sonuc < 0)
                    sonuc = YolcuNo;
            }
            catch (Exception)
            {
                throw;
            }
            baglan.Close();
            baglan.Dispose();
            komut.Dispose();
            return sonuc;
        }
        public int BiletEkleDuzenle(int BiletNo, int SeferNo, int YolcuNo, DateTime OlusturulmaTarihi, int FiyatNo, int OdemeTipi, bool AktifMi, int KoltukNumarasi, int DuzenleyenNo)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            if (BiletNo > 0)
                komut.CommandText = "update Biletler set SeferNo=@SeferNo, YolcuNo=@YolcuNo, OlusturulmaTarihi=@OlusturulmaTarihi, FiyatNo=@FiyatNo, OdemeTipi=@OdemeTipi, AktifMi=@AktifMi,KoltukNumarasi=@KoltukNumarasi, DuzenleyenNo=@DuzenleyenNo  where BiletNo=@BiletNo";
            else
                komut.CommandText = "insert into Biletler(SeferNo, YolcuNo, OlusturulmaTarihi, FiyatNo, OdemeTipi, AktifMi, KoltukNumarasi, DuzenleyenNo) values (@SeferNo, @YolcuNo, @OlusturulmaTarihi, @FiyatNo, @OdemeTipi, @AktifMi, @KoltukNumarasi, @DuzenleyenNo)";
            komut.Parameters.AddWithValue("@SeferNo", SeferNo);
            komut.Parameters.AddWithValue("@YolcuNo", YolcuNo);
            komut.Parameters.AddWithValue("@OlusturulmaTarihi", OlusturulmaTarihi);
            komut.Parameters.AddWithValue("@FiyatNo", FiyatNo);
            komut.Parameters.AddWithValue("@OdemeTipi", OdemeTipi);
            komut.Parameters.AddWithValue("@AktifMi", AktifMi);
            komut.Parameters.AddWithValue("@KoltukNumarasi", KoltukNumarasi);
            komut.Parameters.AddWithValue("@DuzenleyenNo", DuzenleyenNo);
            if (BiletNo > 0)
                komut.Parameters.AddWithValue("@BiletNo", BiletNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
                if (BiletNo < 0)
                {
                    komut.CommandText = "select @@IDENTITY as sonuc order by sonuc desc";
                    SqlDataReader dr = komut.ExecuteReader();
                    dr.Read();
                    sonuc = Convert.ToInt32(dr["sonuc"]);
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            baglan.Close();
            baglan.Dispose();
            komut.Dispose();
            return sonuc;
        }
        public int SoforEkleGuncelle(int SoforNo, string TC, string Ad, string Soyad, string TelefonNo, string Adres)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            if (SoforNo > 0)
                komut.CommandText = "update Soforler set TC=@TC, Ad=@Ad, Soyad=@Soyad, TelefonNo=@TelefonNo, Adres=@Adres where SoforNo=@SoforNo";
            else
                komut.CommandText = "insert into Soforler(TC, Ad, Soyad, TelefonNo, Adres) values (@TC, @Ad, @Soyad, @TelefonNo, @Adres)";
            komut.Parameters.AddWithValue("@TC", TC);
            komut.Parameters.AddWithValue("@Ad", Ad);
            komut.Parameters.AddWithValue("@Soyad", Soyad);
            komut.Parameters.AddWithValue("@TelefonNo", TelefonNo);
            komut.Parameters.AddWithValue("@Adres", Adres);
            if (SoforNo > 0)
                komut.Parameters.AddWithValue("@SoforNo", SoforNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
                if (SoforNo < 0)
                {
                    komut.CommandText = "select @@IDENTITY as sonuc order by sonuc desc";
                    SqlDataReader dr = komut.ExecuteReader();
                    dr.Read();
                    sonuc = Convert.ToInt32(dr["sonuc"]);
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            baglan.Close();
            baglan.Dispose();
            komut.Dispose();
            return sonuc;
        }

        public int MuavinEkleGuncelle(int MuavinNo, string TC, string Ad, string Soyad, string TelefonNo, string Adres, bool Cinsiyet)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            if (MuavinNo > 0)
                komut.CommandText = "update Muavinler set TC=@TC, Ad=@Ad, Soyad=@Soyad, TelefonNo=@TelefonNo, Adres=@Adres, Cinsiyet=@Cinsiyet where SoforNo=@SoforNo";
            else
                komut.CommandText = "insert into Muavinler(TC, Ad, Soyad, TelefonNo, Adres, Cinsiyet) values (@TC, @Ad, @Soyad, @TelefonNo, @Adres, @Cinsiyet)";
            komut.Parameters.AddWithValue("@TC", TC);
            komut.Parameters.AddWithValue("@Ad", Ad);
            komut.Parameters.AddWithValue("@Soyad", Soyad);
            komut.Parameters.AddWithValue("@TelefonNo", TelefonNo);
            komut.Parameters.AddWithValue("@Adres", Adres);
            komut.Parameters.AddWithValue("@Cinsiyet", Cinsiyet);
            if (MuavinNo > 0)
                komut.Parameters.AddWithValue("@MuavinNo", MuavinNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
                if (MuavinNo < 0)
                {
                    komut.CommandText = "select @@IDENTITY as sonuc order by sonuc desc";
                    SqlDataReader dr = komut.ExecuteReader();
                    dr.Read();
                    sonuc = Convert.ToInt32(dr["sonuc"]);
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            baglan.Close();
            baglan.Dispose();
            komut.Dispose();
            return sonuc;
        }

        public int SeferEkleGuncelle(int SeferNo, int OtobusNo, int GuzergahNo, DateTime SeferTarihi)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            if (SeferNo > 0)
                komut.CommandText = "update Seferler set OtobusNo=@OtobusNo, GuzergahNo=@GuzergahNo, SeferTarihi=@SeferTarihi where SeferNo=@SeferNo";
            else
                komut.CommandText = "insert into Seferler(OtobusNo, GuzergahNo, SeferTarihi) values (@OtobusNo, @GuzergahNo, @SeferTarihi)";
            komut.Parameters.AddWithValue("@OtobusNo", OtobusNo);
            komut.Parameters.AddWithValue("@GuzergahNo", GuzergahNo);
            komut.Parameters.AddWithValue("@SeferTarihi", SeferTarihi);
            if (SeferNo > 0)
                komut.Parameters.AddWithValue("@SeferNo", SeferNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
                if (SeferNo < 0)
                {
                    komut.CommandText = "select @@IDENTITY as sonuc order by sonuc desc";
                    SqlDataReader dr = komut.ExecuteReader();
                    dr.Read();
                    sonuc = Convert.ToInt32(dr["sonuc"]);
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            baglan.Close();
            baglan.Dispose();
            komut.Dispose();
            return sonuc;
        }

        public int GuzergahEkleGuncelle(int GuzergahNo, string GuzergahAdi)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            komut.CommandText = "insert into Guzergahlar(GuzergahAdi, GuzergahNo ) values (@GuzergahAdi, @GuzergahNo)";
            komut.Parameters.AddWithValue("@GuzergahAdi", GuzergahAdi);
            komut.Parameters.AddWithValue("@GuzergahNo", GuzergahNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
                if (GuzergahNo < 0)
                {
                    komut.CommandText = "select @@IDENTITY as sonuc order by sonuc desc";
                    SqlDataReader dr = komut.ExecuteReader();
                    dr.Read();
                    sonuc = Convert.ToInt32(dr["sonuc"]);
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            baglan.Close();
            baglan.Dispose();
            komut.Dispose();
            return sonuc;
        }
        public int OtobusEkleGuncelle(int OtobusNo, string Model, int KoltukSayisi, string KoltukTipi, string PlakaNumarasi)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            if (OtobusNo > 0)
                komut.CommandText = "update Otobusler set Model=@Model, KoltukSayisi=@KoltukSayisi, KoltukTipi=@KoltukTipi, PlakaNumarasi=@PlakaNumarasi where OtobusNo=@OtobusNo";
            else
                komut.CommandText = "insert into Otobusler(Model, KoltukSayisi, KoltukTipi, PlakaNumarasi) values (@Model, @KoltukSayisi, @KoltukTipi, @PlakaNumarasi)";
            komut.Parameters.AddWithValue("@Model", Model);
            komut.Parameters.AddWithValue("@KoltukSayisi", KoltukSayisi);
            komut.Parameters.AddWithValue("@KoltukTipi", KoltukTipi);
            komut.Parameters.AddWithValue("@PlakaNumarasi", PlakaNumarasi);
            if (OtobusNo > 0)
                komut.Parameters.AddWithValue("@OtobusNo", OtobusNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
                if (OtobusNo < 0)
                {
                    komut.CommandText = "select @@IDENTITY as sonuc order by sonuc desc";
                    SqlDataReader dr = komut.ExecuteReader();
                    dr.Read();
                    sonuc = Convert.ToInt32(dr["sonuc"]);
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            baglan.Close();
            baglan.Dispose();
            komut.Dispose();
            return sonuc;
        }

        public DataTable SorguCalistir(string sorgu)
        {
            SqlConnection baglan = this.baglan();
            DataTable dt = new DataTable();
            SqlDataAdapter adaptor = new SqlDataAdapter(sorgu, baglan);
            try
            {
                adaptor.Fill(dt);
            }
            catch (Exception)
            {
                throw;
            }
            adaptor.Dispose();
            baglan.Close();
            return dt;
        }
        public DataSet SorguCalistirDataSet(string sorgu)
        {
            SqlConnection baglan = this.baglan();
            DataSet dt = new DataSet();
            SqlDataAdapter adaptor = new SqlDataAdapter(sorgu, baglan);
            try
            {
                adaptor.Fill(dt);
            }
            catch (Exception)
            {
                throw;
            }
            adaptor.Dispose();
            baglan.Close();
            return dt;

        }
        public int TerminalEkleGuncelle(int TerminalNo, string TerminalAdi, int IlKodu)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            if (TerminalNo > 0)
                komut.CommandText = "update Terminaller set TerminalAdi=@TerminalAdi, IlKodu=@IlKodu where TerminalNo=@TerminalNo";
            else
                komut.CommandText = "insert into Terminaller(TerminalAdi, IlKodu) values (@TerminalAdi, @IlKodu)";
            komut.Parameters.AddWithValue("@TerminalAdi", TerminalAdi);
            komut.Parameters.AddWithValue("@IlKodu", IlKodu);
            if (TerminalNo > 0)
                komut.Parameters.AddWithValue("@TerminalNo", TerminalNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
                if (TerminalNo < 0)
                {
                    komut.CommandText = "select @@IDENTITY as sonuc order by sonuc desc";
                    SqlDataReader dr = komut.ExecuteReader();
                    dr.Read();
                    sonuc = Convert.ToInt32(dr["sonuc"]);
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            baglan.Close();
            baglan.Dispose();
            komut.Dispose();
            return sonuc;
        }
        public int DuzenleyenEkleGuncelle(int DuzenleyenNo, string TC, string Ad, string Soyad, string TelefonNo, string Mail, bool Cinsiyet)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            if (DuzenleyenNo > 0)
                komut.CommandText = "update Düzenleyenler set TC=@TC, Ad=@Ad, Soyad=@Soyad, TelefonNo=@TelefonNo, Mail=@Mail, Cinsiyet=@Cinsiyet where SoforNo=@SoforNo";
            else
                komut.CommandText = "insert into Düzenleyenler(TC, Ad, Soyad, TelefonNo, Mail, Cinsiyet) values (@TC, @Ad, @Soyad, @TelefonNo, @Mail, @Cinsiyet)";
            komut.Parameters.AddWithValue("@TC", TC);
            komut.Parameters.AddWithValue("@Ad", Ad);
            komut.Parameters.AddWithValue("@Soyad", Soyad);
            komut.Parameters.AddWithValue("@TelefonNo", TelefonNo);
            komut.Parameters.AddWithValue("@Mail", Mail);
            komut.Parameters.AddWithValue("@Cinsiyet", Cinsiyet);
            if (DuzenleyenNo > 0)
                komut.Parameters.AddWithValue("@DuzenleyenNo", DuzenleyenNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
                if (DuzenleyenNo < 0)
                {
                    komut.CommandText = "select @@IDENTITY as sonuc order by sonuc desc";
                    SqlDataReader dr = komut.ExecuteReader();
                    dr.Read();
                    sonuc = Convert.ToInt32(dr["sonuc"]);
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            baglan.Close();
            baglan.Dispose();
            komut.Dispose();
            return sonuc;
        }
        public int FiyatEkleGuncelle(int FiyatNo, int BaslamaTerminali, int BitisTerminali, int Ucret)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            if (FiyatNo > 0)
                komut.CommandText = "update Fiyatlar set BaslamaTerminali=@BaslamaTerminali, BitisTerminali=@BitisTerminali, Ucret=@Ucret where FiyatNo=@FiyatNo";
            else
                komut.CommandText = "insert into Fiyatlar(BaslamaTerminali, BitisTerminali,Ucret) values (@TerminalAdi, @IlKodu,@Ucret)";
            komut.Parameters.AddWithValue("@BaslamaTerminali", BaslamaTerminali);
            komut.Parameters.AddWithValue("@BitisTerminali", BitisTerminali);
            komut.Parameters.AddWithValue("@Ucret", Ucret);
            if (FiyatNo > 0)
                komut.Parameters.AddWithValue("@FiyatNo", FiyatNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
                if (FiyatNo < 0)
                {
                    komut.CommandText = "select @@IDENTITY as sonuc order by sonuc desc";
                    SqlDataReader dr = komut.ExecuteReader();
                    dr.Read();
                    sonuc = Convert.ToInt32(dr["sonuc"]);
                    dr.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            baglan.Close();
            baglan.Dispose();
            komut.Dispose();
            return sonuc;
        }
        public int MuavinAtama(int SeferNo, int MuavinNo)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            komut.CommandText = "insert into SeferMuavin(SeferNo, MuavinNo) values (@SeferNo, @MuavinNo)";
            komut.Parameters.AddWithValue("@SeferNo", SeferNo);
            komut.Parameters.AddWithValue("@MuavinNo", MuavinNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            baglan.Close();
            baglan.Dispose();
            komut.Dispose();
            return sonuc;
        }
        public int SoforAtama(int SeferNo, int SoforNo)
        {
            SqlConnection baglan = this.baglan();
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglan;
            komut.CommandText = "insert into SeferSofor(SeferNo, SoforNo) values (@SeferNo, @SoforNo)";
            komut.Parameters.AddWithValue("@SeferNo", SeferNo);
            komut.Parameters.AddWithValue("@SoforNo", SoforNo);
            int sonuc = 0;
            try
            {
                baglan.Open();
                sonuc = komut.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            baglan.Close();
            baglan.Dispose();
            komut.Dispose();
            return sonuc;
        }
    }
}