using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Netsis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Tblcasabit, Trn23Context>, ICustomerDal
    {
        public EfCustomerDal(Trn23Context context) : base(context)
        {
        }

        public List<Tblcasabit> GetTop100(int take)
        {
            return _context.Tblcasabits.Take(take).ToList();
        }

        public async Task<List<Tblcasabit>> GetCustomerByTurkishChar(string search)
        {
            using (Trn23Context context = new Trn23Context())
            {
                string[] original = { "Ý", "ý", "ý", "Þ", "þ", "ð", "Ð" };

                foreach (var item in original)
                {
                    search = search.Replace(item,"_");
                }
                return search != null
                    ? new List<Tblcasabit>(await context.Tblcasabits.FromSqlRaw<Tblcasabit>($"SELECT *  FROM TBLCASABIT_VW WHERE CARI_ISIM LIKE '%{search.ToUpper()}%'").ToListAsync())
                    : new List<Tblcasabit>(await context.Tblcasabits.FromSqlRaw<Tblcasabit>($"SELECT TOP 100 *  FROM TBLCASABIT_VW ").ToListAsync());
            }
        }
    }
}
