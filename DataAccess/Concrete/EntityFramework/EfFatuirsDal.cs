using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete.Netsis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfFatuirsDal : EfEntityRepositoryBase<Tblfatuir, Trn23Context>, IFatuirsDal
    {
        public EfFatuirsDal(Trn23Context context) : base(context)
        {
        }

        public async Task<string> GetMaxFat(string ftIrsip, string fatNo)
        {
            return await _context.Tblfatuirs.Where(x => x.Ftirsip == ftIrsip && x.FatirsNo.Contains(fatNo)).MaxAsync(x => x.FatirsNo)!;
        }



        public async Task<string> Identifier(string cari)
        {
            var isIdentifierExists = await _context.EfatCariVws.Where(x => x.CariKod == cari).AnyAsync();

            if (!isIdentifierExists)
                return null!;

            var cariKod = await _context.EfatCariVws.Where(x => x.CariKod == cari).SingleOrDefaultAsync();

            return cariKod!.Identifier;


        }
    }
}
