using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyOperate.Web.Models;
using EasyOperate.Web.Models.AccessControl;
using System.Data.Entity;
using EasyOperate.Common.Enums;

namespace EasyOperate.Web.Controllers
{
    [AllowAnonymous]
    public class SubRegionController : EasyOperateBaseController
    {
        EfDbContext efDbContext = new EfDbContext();
        // GET: SubRegion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public NodeTreeItem ModelToTreeNode(SubRegionModel model)
        {
            NodeTreeItem nodeTreeItem = new NodeTreeItem();
            nodeTreeItem.id = model.ID;
            nodeTreeItem.title = model.Name;
            nodeTreeItem.ptypeid = PTypeIdEnum.SubRegion;
            nodeTreeItem.pid = model.ProjectId;
            nodeTreeItem.index = 0;
            nodeTreeItem.ischecked = true;
            nodeTreeItem.TerminalNodes = null;
            nodeTreeItem.Childrens = null;
            return nodeTreeItem;
        }
        [HttpPost]
        public JsonResult Create(SubRegionModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                ProjectModel projectModel= efDbContext.Project.Where(p => p.ID == model.ProjectId).FirstOrDefault();
                if (projectModel==null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定项", null));
                }
                if (string.IsNullOrEmpty( model.Name))
                {
                    return Json(new ResponseInfo(0, "请输入分区名", null));
                }
                if (efDbContext.SubRegion.Where(s => s.ProjectId == model.ProjectId && s.Name == model.Name).FirstOrDefault()!=null)
                {
                    return Json(new ResponseInfo(0, "分区名不能重复", null));
                }
                NodeTreeItem nodeTreeItem = ModelToTreeNode(model);
                efDbContext.SubRegion.Add(model);
                efDbContext.SaveChanges();
                return Json(new ResponseInfo(1, "操作成功", nodeTreeItem));               
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "创建过程发生异常", null));
            }
        }
        //[HttpGet]
        //public JsonResult Edit(int? Id)
        //{
        //    try
        //    {
        //        if (Id == null)
        //        {
        //            return Json(new ResponseInfo(0, "请输入有效的信息", null));
        //        }
        //        SubRegionModel model = efDbContext.SubRegion.Where(s=>s.ID==Id).FirstOrDefault();
        //        if(model==null)
        //        {
        //            return Json(new ResponseInfo(0, "查找的项不存在", null));
        //        }
        //        return Json(new ResponseInfo(1, "操作成功", model));
        //    }
        //    catch (Exception ex)
        //    {
        //        Com.log.Error(ex);
        //        return Json(new ResponseInfo(0, "操作过程发生异常", null));
        //    }
        //}
        [HttpPost]
        public JsonResult Edit(SubRegionModel model)
        {
            try
            {
                //SubRegionModel model = new SubRegionModel();
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                ProjectModel projectModel = efDbContext.Project.Where(p => p.ID == model.ProjectId).FirstOrDefault();
                if (projectModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定项信息", null));
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new ResponseInfo(0, "请输入分区名", null));
                }
                if (efDbContext.SubRegion.Where(s => s.ProjectId == model.ProjectId && s.Name == model.Name&&s.ID!=model.ID).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "分区名不能重复", null));
                }
                NodeTreeItem nodeTreeItem = ModelToTreeNode(model);
                efDbContext.Entry(model).State = EntityState.Modified;
                efDbContext.SaveChanges();
                return Json(new ResponseInfo(1, "操作成功", nodeTreeItem));
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "编辑过程发生异常", null));
            }
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                SubRegionModel model = efDbContext.SubRegion.Find(Id);
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                HouseModel result = efDbContext.House.Where(h => h.SubRegionId == model.ID).FirstOrDefault();
                if (result != null)
                {
                    return Json(new ResponseInfo(0, "请先删除其它关联项", null));
                }
                efDbContext.Entry(model).State = EntityState.Deleted;
                efDbContext.SaveChanges();
                return Json(new ResponseInfo(1, "操作成功", null));
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "删除过程发生异常", null));
            }
        }
        [HttpPost]
        public JsonResult GetSubRegion(int Id)
        {
            if (Id == 0)
            {
                return Json(new ResponseInfo(0, "查询ID不能为0", null));
            }
            SubRegionModel model = efDbContext.SubRegion.Find(Id);
            if (model == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", model));
        }
        [HttpPost]
        public JsonResult GetSubRegionList(int Id)
        {
            //if (Id == 0)
            //{
            //    return Json(new ResponseInfo(0, "查询ID不能为0", null));
            //}
            List<SubRegionModel> subRegionModelList = efDbContext.SubRegion.ToList();
            if (subRegionModelList == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", subRegionModelList));
        }
    }
}