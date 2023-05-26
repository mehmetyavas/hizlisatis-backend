using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Netsis;
using Entities.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace DataAccess.Concrete.EntityFramework
{
    public class EfStockDal : EfEntityRepositoryBase<Tblstsabit, Trn23Context>, IStockDal
    {
        public EfStockDal(Trn23Context context) : base(context)
        {
        }

        public async Task<IDataResult<List<Tblstsabit>>> GetByQuery(string search)
        {
            using (Trn23Context context = new Trn23Context())
            {
                return new SuccessDataResult<List<Tblstsabit>>(await context.Tblstsabits.FromSqlRaw<Tblstsabit>($"SELECT *  FROM TBLSTSABIT_VW WHERE STOK_ADI LIKE '%{search.ToUpper()}'").ToListAsync());
            }
        }

        public Task<List<Tblstsabit>> GetTop100(int take)
        {
            return _context.Tblstsabits.Take(take).ToListAsync();
        }
    }
}
