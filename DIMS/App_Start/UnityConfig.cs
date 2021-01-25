// Decompiled with JetBrains decompiler
// Type: DIMS.App_Start.UnityConfig
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System.Web.Mvc;
using DIMS.Services.Abstract;
using DIMS.Services.Implementation;
using Repository.Base;
using Repository.Core;
using Repository.Database;
using Repository.Database.Base;
using Unity;
using Unity.Mvc5;

namespace DIMS
{
  public class UnityConfig
  {
      public static void RegisterComponents(UnityContainer container)
      {
          container.RegisterType<IConnectionFactory, SqlDbConnectionFactory>();
          container.RegisterType<IUnitOfWork, UnitOfWork>();
          container.RegisterType<IDbContext, DapperContext>();
          container.RegisterType<IUserService, UserService>();
          container.RegisterType<IOPDPatientRegistrationService, OPDPatientRegistrationService>();
          container.RegisterType<IOMRCasesheetService, OMRCasesheetService>();
          container.RegisterType<IPERIOCasesheetService, PERIOCasesheetService>();
            container.RegisterType<IPERIODiagnosisCasesheetService, PERIODiagnosisCasesheetService>();
            container.RegisterType<IORTHOCasesheetService, ORTHOCasesheetService>();
          container.RegisterType<IORTHOAnalysisService, ORTHOAnalysisService>();
          container.RegisterType<ICONSCasesheetService, CONSCasesheetService>();
          container.RegisterType<IORPATHRequisitionService, ORPATHRequisitionService>();
          container.RegisterType<IPEDOCasesheetService, PEDOCasesheetService>();
          container.RegisterType<IPROSCasesheetService, PROSCasesheetService>();
          container.RegisterType<IPHDCasesheetService, PHDCasesheetService>();
          container.RegisterType<IOMFSCasesheetService, OMFSCasesheetService>();
          container.RegisterType<IFollowUpService, FollowUpService>();
          container.RegisterType<IOPDRevisitRegistrationService, OPDRevisitRegistrationService>();
          container.RegisterType<IReportService, ReportService>();
          container.RegisterType<IMASBillingServicesService, MasBillingServicesService>();
          container.RegisterType<IMASDepartmentService, MasDepartmentService>();
          container.RegisterType<IMASDesignationService, MasDesignationServices>();
          container.RegisterType<IMASDoctorService, MasDoctorService>();
          container.RegisterType<IBillQueueService, BillQueueService>();
          container.RegisterType<IBillingService, BillingService>();
          container.RegisterType<IStudentAllotmentService, StudentAllotmentService>();
          container.RegisterType<IMASStudentRegistrationService, MASStudentRegistrationService>();
          container.RegisterType<IStudentScheduleService, StudentScheduleService>();
          container.RegisterType<IMASCodeService, MASCodeService>();
          container.RegisterType<IFileUploadService, FileUploadService>();
          container.RegisterType<IMASGroupService, MASGroupService>();
          container.RegisterType<ITemplateService, TemplateService>();
          container.RegisterType<IRadioRegistrationService, RadioRegistrationService>();
          container.RegisterType<ILaboratoryRegistrationService, LaboratoryRegistrationService>();
          container.RegisterType<IOrpathCasesheetService, OrpathCasesheetService>();
          container.RegisterType<ICasesheetNoService, CasesheetNoService>();
          container.RegisterType<IPrescriptionsService, PrescriptionsService>();
          container.RegisterType<IErrorLogService, ErrorLogService>();
          container.RegisterType<IChartService, ChartService>();
          container.RegisterType<IApprovalService, ApprovalService>();
          container.RegisterType<IDCardsService, DCardsService>();
          container.RegisterType<IItemCategory, ItemCategoryService>();
          container.RegisterType<IItemStore, StoreService>();
          container.RegisterType<IItemSupplier, ItemSupplierService>();
          container.RegisterType<IItem, ItemService>();
          container.RegisterType<IItemStock, ItemStockService>();
          container.RegisterType<IItemIssue, ItemIssueService>();
          container.RegisterType<IBooks, BooksService>();
          container.RegisterType<IBookIssues, BooksIssueService>();
          DependencyResolver.SetResolver(
              (IDependencyResolver) new UnityDependencyResolver((IUnityContainer) container));
      }
  }
}
