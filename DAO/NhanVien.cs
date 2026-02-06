using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiVeTaiQuay.DAO
{
    internal class NhanVien
    {
        private static NhanVien instance;
        public static NhanVien Instance => instance ?? (instance = new NhanVien());

        public DataTable GetListNhanVien() => DataProvider.Instance.ExecuteQuery("SELECT * FROM NhanVien");
    }
}

