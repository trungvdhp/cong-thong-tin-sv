﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CongThongTinSV
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<MARK_Diem_TC> MARK_Diem_TC { get; set; }
        public DbSet<MARK_DiemThanhPhan_TC> MARK_DiemThanhPhan_TC { get; set; }
        public DbSet<MARK_DiemThi_TC> MARK_DiemThi_TC { get; set; }
        public DbSet<MARK_MonHoc> MARK_MonHoc { get; set; }
        public DbSet<MARK_ThanhPhanMon_TC> MARK_ThanhPhanMon_TC { get; set; }
        public DbSet<MOD_DichVu> MOD_DichVu { get; set; }
        public DbSet<MOD_HocKy> MOD_HocKy { get; set; }
        public DbSet<MOD_LopTinChi_TC> MOD_LopTinChi_TC { get; set; }
        public DbSet<MOD_NguoiDung> MOD_NguoiDung { get; set; }
        public DbSet<MOD_NhomHocVien> MOD_NhomHocVien { get; set; }
        public DbSet<MOD_NhomNguoiDung> MOD_NhomNguoiDung { get; set; }
        public DbSet<MOD_ToNhom> MOD_ToNhom { get; set; }
        public DbSet<PLAN_ChuongTrinhDaoTao> PLAN_ChuongTrinhDaoTao { get; set; }
        public DbSet<PLAN_ChuongTrinhDaoTaoChiTiet> PLAN_ChuongTrinhDaoTaoChiTiet { get; set; }
        public DbSet<PLAN_GiaoVien> PLAN_GiaoVien { get; set; }
        public DbSet<PLAN_HocKyDangKy_TC> PLAN_HocKyDangKy_TC { get; set; }
        public DbSet<PLAN_LopTinChi_TC> PLAN_LopTinChi_TC { get; set; }
        public DbSet<PLAN_MonTinChi_TC> PLAN_MonTinChi_TC { get; set; }
        public DbSet<PLAN_SukiensTinChi_TC> PLAN_SukiensTinChi_TC { get; set; }
        public DbSet<POR_GiaoVien> POR_GiaoVien { get; set; }
        public DbSet<STU_ChuyenNganh> STU_ChuyenNganh { get; set; }
        public DbSet<STU_DanhSach> STU_DanhSach { get; set; }
        public DbSet<STU_GioiTinh> STU_GioiTinh { get; set; }
        public DbSet<STU_He> STU_He { get; set; }
        public DbSet<STU_HeChuyenNganh> STU_HeChuyenNganh { get; set; }
        public DbSet<STU_HoSoSinhVien> STU_HoSoSinhVien { get; set; }
        public DbSet<STU_Khoa> STU_Khoa { get; set; }
        public DbSet<STU_Lop> STU_Lop { get; set; }
        public DbSet<STU_Nganh> STU_Nganh { get; set; }
        public DbSet<ViewLopTC> ViewLopTCs { get; set; }
        public DbSet<ViewNamHoc> ViewNamHocs { get; set; }
        public DbSet<STU_DanhSachLopTinChi> STU_DanhSachLopTinChi { get; set; }
        public DbSet<MOD_DanhSachLopTinChi> MOD_DanhSachLopTinChi { get; set; }
        public DbSet<MOD_NguoiDung_VaiTro_LopTinChi> MOD_NguoiDung_VaiTro_LopTinChi { get; set; }
        public DbSet<MOD_NguoiDung_VaiTro_HeThong> MOD_NguoiDung_VaiTro_HeThong { get; set; }
    
        public virtual ObjectResult<SP_SinhVienLopTC_Result> SP_SinhVienLopTC(Nullable<int> id_lop_tc)
        {
            var id_lop_tcParameter = id_lop_tc.HasValue ?
                new ObjectParameter("Id_lop_tc", id_lop_tc) :
                new ObjectParameter("Id_lop_tc", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SinhVienLopTC_Result>("SP_SinhVienLopTC", id_lop_tcParameter);
        }
    
        public virtual ObjectResult<SP_SinhVien_Result> SP_SinhVien(string id_lop, string ma_sv, string ho_dem, string ten, string ngay_sinh, string gioi_tinh)
        {
            var id_lopParameter = id_lop != null ?
                new ObjectParameter("Id_lop", id_lop) :
                new ObjectParameter("Id_lop", typeof(string));
    
            var ma_svParameter = ma_sv != null ?
                new ObjectParameter("Ma_sv", ma_sv) :
                new ObjectParameter("Ma_sv", typeof(string));
    
            var ho_demParameter = ho_dem != null ?
                new ObjectParameter("Ho_dem", ho_dem) :
                new ObjectParameter("Ho_dem", typeof(string));
    
            var tenParameter = ten != null ?
                new ObjectParameter("Ten", ten) :
                new ObjectParameter("Ten", typeof(string));
    
            var ngay_sinhParameter = ngay_sinh != null ?
                new ObjectParameter("Ngay_sinh", ngay_sinh) :
                new ObjectParameter("Ngay_sinh", typeof(string));
    
            var gioi_tinhParameter = gioi_tinh != null ?
                new ObjectParameter("Gioi_tinh", gioi_tinh) :
                new ObjectParameter("Gioi_tinh", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_SinhVien_Result>("SP_SinhVien", id_lopParameter, ma_svParameter, ho_demParameter, tenParameter, ngay_sinhParameter, gioi_tinhParameter);
        }
    
        public virtual ObjectResult<SP_Moodle_SinhVien_Result> SP_Moodle_SinhVien(Nullable<int> id_chuyen_nganh)
        {
            var id_chuyen_nganhParameter = id_chuyen_nganh.HasValue ?
                new ObjectParameter("Id_chuyen_nganh", id_chuyen_nganh) :
                new ObjectParameter("Id_chuyen_nganh", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Moodle_SinhVien_Result>("SP_Moodle_SinhVien", id_chuyen_nganhParameter);
        }
    }
}
