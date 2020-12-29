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
    public class HousePartController : EasyOperateBaseController
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
        public NodeTreeItem ModelToTreeNode(HousePartModel model)
        {
            NodeTreeItem nodeTreeItem = new NodeTreeItem();
            nodeTreeItem.id = model.ID;
            nodeTreeItem.title = model.Name;
            nodeTreeItem.ptypeid = PTypeIdEnum.HousePart;
            nodeTreeItem.pid = model.HouseId;
            nodeTreeItem.index = 0;
            nodeTreeItem.ischecked = true;
            nodeTreeItem.TerminalNodes = null;
            nodeTreeItem.Childrens = null;
            return nodeTreeItem;
        }
        [HttpPost]
        public JsonResult Create(HousePartModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                HouseModel houseModel = efDbContext.House.Find(model.HouseId);
                if (houseModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定的项目信息", null));
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new ResponseInfo(0, "请输入单元名", null));
                }
                
                if (efDbContext.HousePart.Where(hp => hp.HouseId == model.HouseId && hp.Name == model.Name).FirstOrDefault()!=null)
                {
                    return Json(new ResponseInfo(0, "单元名不能重复", null));
                }
                NodeTreeItem nodeTreeItem = ModelToTreeNode(model);
                efDbContext.HousePart.Add(model);
                efDbContext.SaveChanges();
                if (CreateFloorAndRoom(model) == false)
                {
                    return Json(new ResponseInfo(0, "自动创建楼层和房间过程出现错误", null));
                }
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
                HousePartModel model = efDbContext.HousePart.Find(Id);
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
        public JsonResult Edit(HousePartModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                HouseModel houseModel = efDbContext.House.Find(model.HouseId);
                if (houseModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定的项目信息", null));
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new ResponseInfo(0, "请输入单元名", null));
                }
                if (efDbContext.HousePart.Where(hp => hp.HouseId == model.HouseId && hp.Name == model.Name&&hp.ID!= model.ID).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "单元名不能重复", null));
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
                HousePartModel model = efDbContext.HousePart.Find(Id);
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                AccessControlEquipmentModel accessControlEquipmentModel = efDbContext.AccessControlEquipment.Where(ace => ace.HousePartId == model.ID).FirstOrDefault();
                AccessControlEquipmentNodeModel accessControlEquipmentNodeModel = efDbContext.AccessControlEquipmentNode.Where(acen => acen.HousePartId == model.ID).FirstOrDefault();
                if (accessControlEquipmentModel != null|| accessControlEquipmentNodeModel!=null)
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
        public JsonResult GetHousePart(int Id)
        {
            if (Id == 0)
            {
                return Json(new ResponseInfo(0, "查询ID不能为0", null));
            }
            HousePartModel model = efDbContext.HousePart.Find(Id);
            if (model == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", model));
        }
        [HttpPost]
        public JsonResult GetHousePartList(int Id)
        {
            if (Id == 0)
            {
                return Json(new ResponseInfo(0, "查询ID不能为0", null));
            }
            List<HousePartModel> housePartModelList = efDbContext.HousePart.Where(h => h.HouseId == Id).ToList();
            if (housePartModelList == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", housePartModelList));
        }
        /// <summary>
        /// 自动创建楼层和房间
        /// </summary>
        /// <param name="housePartModel"></param>
        /// <returns></returns>
        public bool CreateFloorAndRoom(HousePartModel housePartModel)
        {
            try
            {
                for (int i = 0; i < housePartModel.FloorCount; i++)
                {
                    FloorModel floorModel = new FloorModel();
                    floorModel.HousePartId = housePartModel.ID;
                    floorModel.Name = housePartModel.FloorPrefix + (i+1) + housePartModel.FloorSuffix;
                    floorModel.FloorNumber = i;
                    efDbContext.Floor.Add(floorModel);
                    efDbContext.SaveChanges();
                    for(int j=0;j<housePartModel.SingleFloorRoomCount;j++)
                    {
                        RoomModel roomModel = new RoomModel();
                        roomModel.FloorId = floorModel.ID;
                        roomModel.Name = housePartModel.RoomPrefix + (j + 1) + housePartModel.RoomSuffix;
                        roomModel.RoomNumber = i;
                        efDbContext.Room.Add(roomModel);
                        efDbContext.SaveChanges();
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                Com.log.Error(ex);
                return false;
            } 
        }
    }
}