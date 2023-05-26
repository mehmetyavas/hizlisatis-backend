using AutoMapper.Execution;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.Sale;
using Entities.DTOs.UserDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSaleDal : EfEntityRepositoryBase<Sale, HsDbContext>, ISaleDal
    {
        public EfSaleDal(HsDbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<SaleDTO>> GetSaleDetail(Expression<Func<SaleDTO, bool>> expression = null)
        {

            var result = _context.Sales.Select(s => new SaleDTO
            {
                amount = s.Amount,
                employee = _context.Employees.Where(e => e.Id == s.EmployeeId).SingleOrDefault()!,
                createdAt = s.CreatedAt,
                customerCode = s.CustomerCode,
                payment = _context.Payments.Where(p => p.Id == s.PaymentType).SingleOrDefault()!,
                price = s.Price,
                shift = _context.Shifts.Where(sh => sh.Id == s.ShiftId).SingleOrDefault()!,
                slipId = s.SlipId,
                stockCode = s.StockCode,
                stockName = s.StockName,
                transfer = s.Transfer,
                user = _context.Users.Where(u => u.Id == s.UserId).SingleOrDefault()!,
                VATGroup = s.VATGroup
            });

            return expression == null ? await result.ToListAsync() : await result.Where(expression).ToListAsync();


        }

        public async Task<IEnumerable<Sale>> GetSlipById(int id)
            => await _context.Sales.Where(s => s.SlipId == id).ToListAsync();



        public async Task<IResult> AddSlip(List<Sale> sale)
        {
            int getMaxSlip = 1;
            var ifSlipisExist = _context.Sales.Any();

            if (ifSlipisExist)
                getMaxSlip = _context.Sales.Max(x => x.SlipId) + 1;


            foreach (var sales in sale)
            {
                _context.Sales.Add(new Sale
                {
                    SlipId = getMaxSlip,
                    Amount = sales.Amount,
                    CustomerCode = sales.CustomerCode,
                    EmployeeId = sales.EmployeeId,
                    PaymentType = sales.PaymentType,
                    Price = sales.Price,
                    ShiftId = sales.ShiftId,
                    StockCode = sales.StockCode,
                    StockName = sales.StockName,
                    Transfer = sales.Transfer,
                    UserId = sales.UserId,
                    VATGroup = sales.VATGroup,
                    CustomerDescription = sales.CustomerDescription,
                    CreatedAt = DateTime.Now,
                });
            }

            await _context.SaveChangesAsync();

            return new SuccessResult("Satış Yapıldı!");
        }

        public async Task<int> GetMaxSlipNo()
        {

            var result = _context.Sales.Any();
            return result == false ? 1 : await _context.Sales.MaxAsync(x => x.SlipId);
        }

        public async Task<IEnumerable<SaleSummaryDTO>> GetSaleSummaries(DateTime? date, int userId)
        {

            var result = await _context.Sales.Where(s => s.UserId == userId).Select(s => new SaleSummaryDTO
            {
                id = s.Id,
                amount = s.Amount,
                createdAt = s.CreatedAt,
                customerCode = s.CustomerCode,
                CustomerDescription = s.CustomerDescription,
                employee = _context.Employees.Where(x => x.Id == s.EmployeeId).SingleOrDefault()!.Firstname,
                payment = _context.Payments.Where(x => x.Id == s.PaymentType).SingleOrDefault()!.PaymentName,
                price = s.Price,
                shift = _context.Shifts.Where(x => x.Id == s.ShiftId).SingleOrDefault()!.Name,
                slipId = s.SlipId,
                stockCode = s.StockCode,
                stockName = s.StockName,
                transfer = s.Transfer,
                user = _context.Users.Where(x => x.Id == s.UserId).SingleOrDefault()!.Username!,
                VATGroup = s.VATGroup
            }).Where(x => x.createdAt.Value >= date.Value).OrderByDescending(x => x.id).ToListAsync();

            return date != null
                ? result
                : await _context.Sales.Where(s => s.UserId == userId).Select(s => new SaleSummaryDTO
                {
                    id = s.Id,
                    amount = s.Amount,
                    createdAt = s.CreatedAt,
                    customerCode = s.CustomerCode,
                    CustomerDescription = s.CustomerDescription,
                    employee = _context.Employees.Where(x => x.Id == s.EmployeeId).SingleOrDefault()!.Firstname,
                    payment = _context.Payments.Where(x => x.Id == s.PaymentType).SingleOrDefault()!.PaymentName,
                    price = s.Price,
                    shift = _context.Shifts.Where(x => x.Id == s.ShiftId).SingleOrDefault()!.Name,
                    slipId = s.SlipId,
                    stockCode = s.StockCode,
                    stockName = s.StockName,
                    transfer = s.Transfer,
                    user = _context.Users.Where(x => x.Id == s.UserId).SingleOrDefault()!.Username!,
                    VATGroup = s.VATGroup
                }).Take(100).OrderByDescending(x => x.id).ToListAsync();
        }
    }
}
