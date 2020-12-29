using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyOperate.Web.Models;
using System.Data.Entity;
using EasyOperate.Common.Enums;

namespace EasyOperate.Web.Controllers
{
    [AllowAnonymous]
    public class HouseController : EasyOperateBaseController
    {
        EfDbContext efDbContext = new EfDbContext();
        // GET: House
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public NodeTreeItem ModelToTreeNode(HouseModel model)
        {
            NodeTreeItem nodeTreeItem = new NodeTreeItem();
            nodeTreeItem.id = model.ID;
            nodeTreeItem.title = model.Name;
            nodeTreeItem.ptypeid = PTypeIdEnum.House;
            nodeTreeItem.pid = model.SubRegionId;
            nodeTreeItem.index = 0;
            nodeTreeItem.ischecked = true;
            nodeTreeItem.TerminalNodes = null;
            nodeTreeItem.Childrens = null;
            return nodeTreeItem;
        }
        [HttpPost]
        public JsonResult Create(HouseModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                SubRegionModel subRegionModel = efDbContext.SubRegion.Find(model.SubRegionId);
                if (subRegionModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定的项目信息", null));
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new ResponseInfo(0, "请输入建筑名", null));
                }
                if (efDbContext.House.Where(h => h.SubRegionId == model.SubRegionId && h.Name == model.Name ).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "建筑名不能重复", null));
                }
                NodeTreeItem nodeTreeItem = ModelToTreeNode(model);
                efDbContext.House.Add(model);
                efDbContext.SaveChanges();
                return Json(new ResponseInfo(1, "操作成功", nodeTreeItem));
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "创建过程发生异常", null));
            }
        }
        [HttpGet]
        public JsonResult Edit(int? Id)
        {
            try
            {
                if (Id == null)
                {
                    return Json(new ResponseInfo(0, "请输入有效的信息", null));
                }
                HouseModel model = efDbContext.House.Find(Id);
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                return Json(new ResponseInfo(1, "操作成功", model));
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "操作过程发生异常", null));
            }
        }
        [HttpPost]
        public JsonResult Edit(HouseModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                SubRegionModel subRegionModel = efDbContext.SubRegion.Find(model.SubRegionId);
                if (subRegionModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定的项目信息", null));
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new ResponseInfo(0, "请输入建筑名", null));
                }
                if (efDbContext.House.Where(h => h.SubRegionId == model.SubRegionId && h.Name == model.Name && h.ID != model.ID).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "建筑名不能重复", null));
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
                HouseModel model = efDbContext.House.Find(Id);
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                HousePartModel result = efDbContext.HousePart.Where(hp => hp.HouseId == model.ID).FirstOrDefault();
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
        public JsonResult GetHouse(int Id)
        {
            if (Id == 0)
            {
                return Json(new ResponseInfo(0, "查询ID不能为0", null));
            }
            HouseModel model = efDbContext.House.Find(Id);
            if (model == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", model));
        }
        [HttpPost]
        public JsonResult GetHouseList(int Id)
        {
            if (Id == 0)
            {
                return Json(new ResponseInfo(0, "查询ID不能为0", null));
            }
            List<HouseModel> houseModelList = efDbContext.House.Where(h=>h.SubRegionId== Id).ToList();
            if (houseModelList == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", houseModelList));
        }
    }
}