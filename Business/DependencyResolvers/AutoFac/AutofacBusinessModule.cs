using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.Services.Abstract;
using Business.Services.Concrete;
using Business.Services.Concrete.AuthHandler;
using Business.Services.Concrete.EmployeeHandler;
using Business.Services.Concrete.SaleHandler;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Concrete.Netsis;
using Entities.DTOs;
using Entities.DTOs.Netsis;
using Entities.DTOs.Sale;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.AutoFac
{
    public class AutofacBusinessModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {


            
            builder.RegisterType<StockManager>().As<IStockService>().InstancePerDependency();
            builder.RegisterType<EfStockDal>().As<IStockDal>().InstancePerDependency();


            builder.RegisterType<EfKasaDal>().As<IKasaDal>().InstancePerDependency();

            builder.RegisterType<EfFatuirsDal>().As<IFatuirsDal>().InstancePerDependency();
            builder.RegisterType<TblFatuirsManager>().As<ITblFatuirsService>().InstancePerDependency();




            builder.RegisterType<SaleManager>().As<ISaleService>().InstancePerDependency();
            builder.RegisterType<EfSaleDal>().As<ISaleDal>().InstancePerDependency();

            builder.RegisterType<CustomerManager>().As<ICustomerService>().InstancePerDependency();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>().InstancePerDependency();

            builder.RegisterType<UserManager>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<EfUserDal>().As<IUserDal>().InstancePerDependency();

            builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerDependency();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().InstancePerDependency();

            builder.RegisterType<EfEmployeeDal>().As<IEmployeeDal>().SingleInstance().InstancePerDependency();
            builder.RegisterType<EmployeeManager>().As<IEmployeeService>().SingleInstance().InstancePerDependency();


            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().InstancePerDependency();
            builder.RegisterType<ClaimManager>().As<IUserOperationClaimService>().InstancePerDependency();

            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().InstancePerDependency();
            builder.RegisterType<RoleManager>().As<IRoleService>().InstancePerDependency();

            builder.RegisterType<EfShiftDal>().As<IShiftDal>().InstancePerDependency();
            builder.RegisterType<ShiftManager>().As<IShiftService>().InstancePerDependency();

            builder.RegisterType<EfPaymentDal>().As<IPaymentDal>().InstancePerDependency();

            builder.RegisterType<EfSettingDal>().As<ISettingDal>().InstancePerDependency();
            builder.RegisterType<SettingManager>().As<ISettingService>().InstancePerDependency();



            builder.RegisterType<EfOutGoingDal>().As<IOutgoingDal>().InstancePerDependency();
            builder.RegisterType<OutgoingManager>().As<IOutgoingService>().InstancePerDependency();
            builder.RegisterType<EfSaleOutgoingDal>().As<ISaleOutgoingDal>().InstancePerDependency();
            builder.RegisterType<SaleOutgoingManager>().As<ISaleOutgoingService>().InstancePerDependency();





            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sale, SaleDTO>();
                cfg.CreateMap<SaleDTO, Sale>();
                cfg.CreateMap<Sale, ReqListDTO>();
                cfg.CreateMap<ReqListDTO, Sale>();


                cfg.CreateMap<SaleReqDTO, Sale>();

                cfg.CreateMap<SaleShiftDTO, Sale>();
                cfg.CreateMap<Sale, SaleShiftDTO>();

                cfg.CreateMap<SaleOutgoing, SaleOutgoingDto>();
                cfg.CreateMap<SaleOutgoingDto, SaleOutgoing>();


                //cfg.CreateMap<SaleReqDTO, Sale>()
                //.ForPath(x => x.Payment.PaymentName, r => r.MapFrom(d => d.UserId));

                cfg.CreateMap<Sale, SaleReqDTO>();
                cfg.CreateMap<Sale, SaleSlipDTO>();
                cfg.CreateMap<Tblstsabit, TblstsabitDTO>();


                cfg.CreateMap<TblstsabitDTO, Tblstsabit>();

                cfg.CreateMap<Tblcasabit, TblcasabitDTO>();
                cfg.CreateMap<Tblkasa, TblKasaDTO>();
                cfg.CreateMap<TblKasaDTO, Tblkasa>();
            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();

                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);

            })
                .As<IMapper>()
                .InstancePerLifetimeScope();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();


            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();


        }

    }
}
