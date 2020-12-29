using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyOperate.Web.Models;
using System.Data.Entity;
using EasyOperate.Web.Models.AccessControl;
using EasyOperate.Common.Enums;

namespace EasyOperate.Web.Controllers
{
    [AllowAnonymous]
    public class FloorController : EasyOperateBaseController
    {
        EfDbContext efDbContext = new EfDbContext();
        // GET: HousePart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public NodeTreeItem ModelToTreeNode(FloorModel model)
        {
            NodeTreeItem nodeTreeItem = new NodeTreeItem();
            nodeTreeItem.id = model.ID;
            nodeTreeItem.title = model.Name;
            nodeTreeItem.ptypeid = PTypeIdEnum.Floor;
            nodeTreeItem.pid = model.HousePartId;
            nodeTreeItem.index = 0;
            nodeTreeItem.ischecked = true;
            nodeTreeItem.TerminalNodes = null;
            nodeTreeItem.Childrens = null;
            return nodeTreeItem;
        }
        [HttpPost]
        public JsonResult Create(FloorModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                HousePartModel housePartModel = efDbContext.HousePart.Find(model.HousePartId);
                if (housePartModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定的楼层信息", null));
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new ResponseInfo(0, "*为必添项", null));
                }
                
                if (efDbContext.Floor.Where(f => (f.HousePartId == model.HousePartId && f.Name == model.Name)||(f.HousePartId == model.HousePartId&&f.FloorNumber==model.FloorNumber)) .FirstOrDefault()!=null)
                {
                    return Json(new ResponseInfo(0, "楼层名和楼层号都不能重复", null));
                }
                NodeTreeItem nodeTreeItem = ModelToTreeNode(model);
                efDbContext.Floor.Add(model);
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
                FloorModel model = efDbContext.Floor.Find(Id);
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
        public JsonResult Edit(FloorModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                HousePartModel housePartModel = efDbContext.HousePart.Find(model.HousePartId);
                if (housePartModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定的项目信息", null));
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new ResponseInfo(0, "*为必添项", null));
                }
                if (efDbContext.Floor.Where(f => (f.HousePartId == model.HousePartId && f.Name == model.Name&&f.ID!=model.ID) || (f.HousePartId == model.HousePartId && f.FloorNumber == model.FloorNumber && f.ID != model.ID)).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "楼层名和楼层号都不能重复", null));
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
                FloorModel model = efDbContext.Floor.Find(Id);
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                RoomModel roomModel = db.Room.Where(r=>r.FloorId==model.ID).FirstOrDefault();
                if (roomModel != null)
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
        public JsonResult GetFloor(int Id)
        {
            if (Id == 0)
            {
                return Json(new ResponseInfo(0, "查询ID不能为0", null));
            }
            FloorModel model = efDbContext.Floor.Find(Id);
            if (model == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", model));
        }
        [HttpPost]
        public JsonResult GetFloorList(int Id)
        {
            if (Id == 0)
            {
                return Json(new ResponseInfo(0, "查询ID不能为0", null));
            }
            List<FloorModel> floorModellList = efDbContext.Floor.Where(f => f.HousePartId == Id).ToList();
            if (floorModellList == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", floorModellList));
        }
    }
}