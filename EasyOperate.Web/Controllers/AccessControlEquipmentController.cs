using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EasyOperate.Web.Models;
using EasyOperate.Common.Enums;
using EasyOperate.Web.Models.AccessControl;

namespace EasyOperate.Web.Controllers
{
    [AllowAnonymous]
    public class AccessControlEquipmentController : EasyOperateBaseController
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
        public NodeTreeItem ModelToTreeNode(AccessControlEquipmentModel model)
        {
            NodeTreeItem nodeTreeItem = new NodeTreeItem();
            nodeTreeItem.id = model.ID;
            nodeTreeItem.title = model.Name;
            nodeTreeItem.ptypeid = PTypeIdEnum.HousePart;
            nodeTreeItem.pid = model.HousePartId;
            nodeTreeItem.index = 0;
            nodeTreeItem.ischecked = true;
            nodeTreeItem.TerminalNodes = null;
            nodeTreeItem.Childrens = null;
            return nodeTreeItem;
        }
        [HttpPost]
        public JsonResult Create(AccessControlEquipmentViewModel viewModel)
        {
            try
            {
                if (viewModel == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                if (viewModel.Port <= 0 || viewModel.Port > 65535 || viewModel.SubControllerPort <= 0 || viewModel.SubControllerPort > 65535)
                {
                    return Json(new ResponseInfo(0, "请添写正确的端口号", null));
                }
                HousePartModel housePartModel = efDbContext.HousePart.Find(viewModel.HousePartId);
                if (housePartModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定的项目信息", null));
                }
                if (string.IsNullOrEmpty(viewModel.Name)|| string.IsNullOrEmpty(viewModel.IP)|| string.IsNullOrEmpty(viewModel.SubControllerIP))
                {
                    return Json(new ResponseInfo(0, "请添写设备名和IP地址", null));
                }
                if(viewModel.NodeCount>64)
                {
                    return Json(new ResponseInfo(0, "门禁点数量最大64", null));
                }
                AccessControlEquipmentModel accessControlEquipmentModel = efDbContext.AccessControlEquipment.Where(ace=> ace.IP == viewModel.IP||ace.SubControllerIP== viewModel.SubControllerIP).FirstOrDefault();
                if (viewModel.IP== viewModel.SubControllerIP || accessControlEquipmentModel!=null )
                {
                    return Json(new ResponseInfo(0, "IP不能重复", null));
                }
                if (efDbContext.AccessControlEquipment.Where(ace => ace.HousePartId == viewModel.HousePartId && ace.Name == viewModel.Name).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "设备名不能重复", null));
                }
                HouseModel houseModel = db.House.Find(housePartModel.HouseId);
                if(houseModel==null)
                {
                    return Json(new ResponseInfo(0, "未找到建筑信息", null));
                }
                SubRegionModel subRegionModel = db.SubRegion.Find(houseModel.SubRegionId);
                if(subRegionModel==null)
                {
                    return Json(new ResponseInfo(0, "未找到分区信息", null));
                }
                AccessControlEquipmentModel model = new AccessControlEquipmentModel()
                {
                    HousePartId = housePartModel.ID,
                    HouseId = houseModel.ID,
                    SubRegionId = subRegionModel.ID,
                    HousePartName = housePartModel.Name,
                    HouseName = houseModel.Name,
                    SubRegionName = subRegionModel.Name,

                    ParentId = 0,
                    Name = viewModel.Name,
                    IP = viewModel.IP,
                    Port=viewModel.Port,
                    Direction=viewModel.Direction,
                    SubControllerIP=viewModel.SubControllerIP,
                    SubControllerPort=viewModel.SubControllerPort,
                    EquipmentType=viewModel.EquipmentType,
                    Serialno=viewModel.Serialno,
                    NodeCount=viewModel.NodeCount,
                    state=viewModel.state,                    
                };
                NodeTreeItem nodeTreeItem = ModelToTreeNode(model);
                    efDbContext.AccessControlEquipment.Add(model);
                    efDbContext.SaveChanges();

                    for (int i = 0; i < model.NodeCount; i++)
                    {
                        AccessControlEquipmentNodeModel accessControlEquipmentNodeModel = new AccessControlEquipmentNodeModel();
                        accessControlEquipmentNodeModel.AccessControlEquipmentId = model.ID;
                        accessControlEquipmentNodeModel.HousePartId = model.HousePartId;
                        accessControlEquipmentNodeModel.Name = model.EquipmentNodePrefix + (i+1) + model.EquipmentNodeSuffix;
                        accessControlEquipmentNodeModel.SetEquipmentNodeValue(i);
                        efDbContext.AccessControlEquipmentNode.Add(accessControlEquipmentNodeModel);
                        efDbContext.SaveChanges();
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
                AccessControlEquipmentModel model = efDbContext.AccessControlEquipment.Where(ace => ace.ID == Id).FirstOrDefault();
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
        public JsonResult Edit(AccessControlEquipmentModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                if (model.Port <= 0 || model.Port > 65535 || model.SubControllerPort <= 0 || model.SubControllerPort > 65535)
                {
                    return Json(new ResponseInfo(0, "请添写正确的端口号", null));
                }
                HousePartModel housePartModel = efDbContext.HousePart.Find(model.HousePartId);
                if (housePartModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定项", null));
                }
                if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.IP) || string.IsNullOrEmpty(model.SubControllerIP))
                {
                    return Json(new ResponseInfo(0, "请添写设备名和IP地址", null));
                }
                AccessControlEquipmentModel accessControlEquipmentModel = efDbContext.AccessControlEquipment.Where(ace => ace.IP == model.IP || ace.SubControllerIP == model.SubControllerIP&&ace.ID!=model.ID).FirstOrDefault();
                if (model.IP == model.SubControllerIP || accessControlEquipmentModel != null)
                {
                    return Json(new ResponseInfo(0, "IP不能重复", null));
                }
                if (efDbContext.AccessControlEquipment.Where(ace => ace.HousePartId == model.HousePartId && ace.Name == model.Name && ace.ID != model.ID).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "设备名不能重复", null));
                }
                AccessControlEquipmentModel newModel = efDbContext.AccessControlEquipment.Find(model.ID);
                if (newModel.IP!=model.IP)
                {
                    AccessControlEquipmentNodeModel equipmentNodeModel = efDbContext.AccessControlEquipmentNode.Where(acen => acen.AccessControlEquipmentId == model.ID).FirstOrDefault();
                    AccessControlCardAuthorizedModel accessControlCardAuthorizedModel = efDbContext.AccessControlCardAuthorized.Where(acca => acca.EquipmentId == model.ID).FirstOrDefault();
                    if (equipmentNodeModel != null)
                    {
                        return Json(new ResponseInfo(0, "IP修改,请先删除关联项", null));
                    }
                    else if (accessControlCardAuthorizedModel != null)
                    {
                        return Json(new ResponseInfo(0, "IP修改,请先删除客户授权。", null));
                    }
                    else
                    {
                        newModel.IP = model.IP;
                    }                     
                }
                newModel = model;
                NodeTreeItem nodeTreeItem = ModelToTreeNode(newModel);

                efDbContext.Entry(newModel).State = EntityState.Modified;
                efDbContext.SaveChanges();
                return Json(new ResponseInfo(1, "操作成功", nodeTreeItem));
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "操作过程发生异常", null));
            }
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                AccessControlEquipmentModel model = efDbContext.AccessControlEquipment.Find(Id);
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                HousePartModel housePartModel = efDbContext.HousePart.Find(model.HousePartId);
                if (housePartModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定信息", null));
                }
                List<AccessControlEquipmentNodeModel>  equipmentNodeList = efDbContext.AccessControlEquipmentNode.Where(acen => acen.AccessControlEquipmentId == model.ID).ToList();
                List<AccessControlCardAuthorizedModel>  accessControlCardAuthorizedList = efDbContext.AccessControlCardAuthorized.Where(acca => acca.EquipmentId == model.ID).ToList();
                if (equipmentNodeList.Count>0|| accessControlCardAuthorizedList.Count >0)
                {
                    return Json(new ResponseInfo(0, "设备还关联门禁点和客户授权，删除设备需删除所有关联项", null));
                }
                //删除操作
                foreach (AccessControlCardAuthorizedModel accessControlCardAuthorized in accessControlCardAuthorizedList)
                {
                    efDbContext.Entry(accessControlCardAuthorized).State = EntityState.Deleted;
                    efDbContext.SaveChanges();
                }
                foreach (AccessControlEquipmentNodeModel equipmentNode in equipmentNodeList)
                {
                    efDbContext.Entry(equipmentNode).State = EntityState.Deleted;
                    efDbContext.SaveChanges();
                }
                efDbContext.Entry(model).State = EntityState.Deleted;
                efDbContext.SaveChanges();
                return Json(new ResponseInfo(1, "删除成功", null));
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "删除过程发生异常", null));
            }
        }
        [HttpPost]
        public JsonResult GetAccessControlEquipment(int Id)
        {
            if(Id==0)
            {
                return Json(new ResponseInfo(0, "查询ID不能为0", null));
            }
            AccessControlEquipmentModel model = efDbContext.AccessControlEquipment.Find(Id);
            if (model == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", model));
        }
    }
}