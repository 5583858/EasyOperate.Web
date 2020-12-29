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
    public class RoomController : EasyOperateBaseController
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
        public NodeTreeItem ModelToTreeNode(RoomModel model)
        {
            NodeTreeItem nodeTreeItem = new NodeTreeItem();
            nodeTreeItem.id = model.ID;
            nodeTreeItem.title = model.Name;
            nodeTreeItem.ptypeid = PTypeIdEnum.Floor;
            nodeTreeItem.pid = model.FloorId;
            nodeTreeItem.index = 0;
            nodeTreeItem.ischecked = true;
            nodeTreeItem.TerminalNodes = null;
            nodeTreeItem.Childrens = null;
            return nodeTreeItem;
        }
        [HttpPost]
        public JsonResult Create(RoomModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                FloorModel floorModel = efDbContext.Floor.Find(model.FloorId);
                if (floorModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定的楼层信息", null));
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new ResponseInfo(0, "请输入房间名", null));
                }

                if (efDbContext.Room.Where(r=>(r.FloorId == model.FloorId && r.Name == model.Name) || (r.FloorId == model.FloorId && r.RoomNumber == model.RoomNumber)).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "房间名和房间号都不能重复", null));
                }
                NodeTreeItem nodeTreeItem = ModelToTreeNode(model);
                efDbContext.Room.Add(model);
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
                RoomModel model = efDbContext.Room.Find(Id);
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
        public JsonResult Edit(RoomModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                FloorModel floorModel = efDbContext.Floor.Find(model.FloorId);
                if (floorModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定的楼层信息", null));
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new ResponseInfo(0, "请输入房间名", null));
                }
                if (efDbContext.Room.Where(r => (r.FloorId == model.FloorId && r.Name == model.Name&&r.ID!= model.ID)||(r.FloorId==model.FloorId&&r.RoomNumber==model.RoomNumber&&r.ID!=model.ID)).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "房间名不能重复", null));
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
                RoomModel model = efDbContext.Room.Find(Id);
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                BaseUserModel baseUserModel = efDbContext.BaseUser.Where(bu=>bu.RoomId==model.ID).FirstOrDefault();
                if (baseUserModel != null)
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
        public JsonResult GetRoom(int Id)
        {
            if (Id == 0)
            {
                return Json(new ResponseInfo(0, "查询ID不能为0", null));
            }
            RoomModel model = efDbContext.Room.Find(Id);
            if (model == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", model));
        }
        [HttpPost]
        public JsonResult GetRoomList(int Id)
        {
            if (Id == 0)
            {
                return Json(new ResponseInfo(0, "查询ID不能为0", null));
            }
            List<RoomModel> roomModelllList = efDbContext.Room.Where(r=>r.FloorId == Id).ToList();
            if (roomModelllList == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", roomModelllList));
        }
    }
}