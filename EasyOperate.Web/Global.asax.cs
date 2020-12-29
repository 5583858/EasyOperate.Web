using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EasyOperate.Common;
using System.Data.Entity;
using System.IO;
using EasyOperate.Web.Models;
using EasyOperate.Web.Models.AccessControl;


namespace EasyOperate.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Application_Start()
        {
            logger.Info("Application_Start");
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/log4net.Config.xml")));
            CommonParameters.ServerRootDir = Server.MapPath("~/");
            Parameters.ServerRootDir = Server.MapPath("~/");

            DatabaseInit();
        }
        void Application_End(object sender, EventArgs e)
        {
            logger.Info("Application_End");
        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            logger.Info("----------- 未处理的异常 -----------");

            Exception error = (Exception)e.ExceptionObject;
            logger.Error(error.Message);
            logger.Error(error.StackTrace);
        }
        void DatabaseInit()
        {
            Database.SetInitializer(new ApplicationUserInit());
            //Database.SetInitializer(new BaseUserInit());

            new ProjectDbContext().Project.Create();
            new SubRegionModelDbContext().SubRegion.Create();
            new HouseModelDbContext().House.Create();
            new HousePartModelDbContext().HousePart.Create();
            new FloorModelModelDbContext().Floor.Create();
            new RoomModelDbContext().Room.Create();
            new BaseUserModelDbContext().BaseUser.Create();
            new UserPhotoModelDbContext().UserPhoto.Create();

            new AccessControlEquipmentDbContext().AccessControlEquipment.Create();
            new AccessControlCardDbContext().AccessControlCard.Create();
            new AccessControlCardAuthorizedDbContext().AccessControlCardAuthorized.Create();
            new AccessControlAuthorizedRecordDbContext().AccessControlAuthorizedRecord.Create();
            new AccessControlRecordDbContext().AccessControlRecord.Create();
            new AccessControlEquipmentNodeDbContext().AccessControlEquipmentNode.Create();
            new AccessControlAuthorizationTemplateDbContext().AccessControlAuthorizationTemplate.Create();
            new AccessControlAuthorizationsTemplateNodeDbContext().AccessControlAuthorizationsTemplateNode.Create();
        }
    }
}
