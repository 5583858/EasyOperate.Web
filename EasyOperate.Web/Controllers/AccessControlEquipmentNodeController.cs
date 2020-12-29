using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EasyOperate.Web.Models;
using EasyOperate.Web.Models.AccessControl;
using EasyOperate.Common.Enums;

namespace EasyOperate.Web.Controllers
{
    [AllowAnonymous]
    public class AccessControlEquipmentNodeController : EasyOperateBaseController
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
        public TerminalNode ModelToTreeNode(AccessControlEquipmentNodeModel model)
        {
            TerminalNode nodeTreeItem = new TerminalNode();
            nodeTreeItem.id = model.ID;
            nodeTreeItem.title = model.Name;
            nodeTreeItem.ptypeid = PTypeIdEnum.AccessControlEquipmentNode;
            nodeTreeItem.pid = model.AccessControlEquipmentId;
            nodeTreeItem.index = 0;
            nodeTreeItem.ischecked = true;
            return nodeTreeItem;
        }
        [HttpPost]
        public JsonResult Create(AccessControlEquipmentNodeModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                AccessControlEquipmentModel EquipmentModel = efDbContext.AccessControlEquipment.Find(model.AccessControlEquipmentId);
                if (EquipmentModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定的设备信息", null));
                }
                if (string.IsNullOrEmpty(model.Name)||model.NodeNumber<1)
                {
                    return Json(new ResponseInfo(0, "请输入门禁名", null));
                }
                AccessControlEquipmentNodeModel baseDataNode = efDbContext.AccessControlEquipmentNode.Where(acen => acen.AccessControlEquipmentId == model.AccessControlEquipmentId && acen.NodeNumber == model.NodeNumber).FirstOrDefault();
                if (baseDataNode != null)
                {
                    return Json(new ResponseInfo(0, "门禁点已存在", null));
                }
                if (efDbContext.AccessControlEquipmentNode.Where(acen => acen.AccessControlEquipmentId == model.AccessControlEquipmentId && acen.Name == model.Name && acen.ID != model.ID).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "门禁名不能重复", null));
                }              
                if(model.NodeNumber>=64)//有异常消息超过最大点
                {
                    return Json(new ResponseInfo(0, "门禁序号最大64", null));
                }
                
                model.SetEquipmentNodeValue(model.NodeNumber);
                TerminalNode terminalNode = ModelToTreeNode(model);
                efDbContext.AccessControlEquipmentNode.Add(model);
                efDbContext.SaveChanges();
                //同时设备的节点数量也要+1
                EquipmentModel.NodeCount++;
                efDbContext.Entry(EquipmentModel).State = EntityState.Modified;
                efDbContext.SaveChanges();
                return Json(new ResponseInfo(1, "操作成功", terminalNode));
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
                AccessControlEquipmentNodeModel model = efDbContext.AccessControlEquipmentNode.Find(Id);
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
        public JsonResult Edit(AccessControlEquipmentNodeModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                AccessControlEquipmentModel equipmentModel = efDbContext.AccessControlEquipment.Find(model.AccessControlEquipmentId);
                if (equipmentModel == null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定的设备信息", null));
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new ResponseInfo(0, "请输入门禁名", null));
                }
                if (efDbContext.AccessControlEquipmentNode.Where(acen => acen.AccessControlEquipmentId == model.AccessControlEquipmentId && acen.Name == model.Name && acen.ID != model.ID).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "门禁名不能重复", null));
                }
                if (model.NodeNumber >= 64)//有异常消息超过最大点
                {
                    return Json(new ResponseInfo(0, "门禁序号最大64", null));
                }
                AccessControlEquipmentNodeModel newModel = efDbContext.AccessControlEquipmentNode.Find(model.ID);
                newModel = model;
                //newModel.Name = model.Name;
                TerminalNode terminalNode = ModelToTreeNode(newModel);
                efDbContext.Entry(newModel).State = EntityState.Modified;
                efDbContext.SaveChanges();
                return Json(new ResponseInfo(1, "操作成功", terminalNode));
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
                AccessControlEquipmentNodeModel model = efDbContext.AccessControlEquipmentNode.Find(Id);
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                AccessControlEquipmentModel  accessControlEquipmentModel = efDbContext.AccessControlEquipment.Find(model.AccessControlEquipmentId);
                if(accessControlEquipmentModel==null)
                {
                    return Json(new ResponseInfo(0, "没有找到指定的设备信息", null));
                }
                if(accessControlEquipmentModel.NodeCount-1!= model.NodeNumber)
                {
                    return Json(new ResponseInfo(0, "删除门禁点，必须从后向前删除", null));
                }
                List<AccessControlCardAuthorizedModel> CardAuthorizedList = efDbContext.AccessControlCardAuthorized.Where(acca => acca.EquipmentId == model.AccessControlEquipmentId).ToList();
                if (CardAuthorizedList.Count>0)
                {
                    return Json(new ResponseInfo(0, "删除门禁点，请先删除所有关联的用户授权", null));
                }
                #region
                //异步方法重新同步设备上的数据

                //再修改数据库中的数据
                foreach (AccessControlCardAuthorizedModel CardAuthorized in CardAuthorizedList)
                {
                    //清除设备授权
                    CardAuthorized.Nodes &= ~model.Node;                    
                    efDbContext.Entry(CardAuthorized).State = EntityState.Deleted;
                    efDbContext.SaveChanges();
                }
                accessControlEquipmentModel.NodeCount--;
                efDbContext.Entry(accessControlEquipmentModel).State = EntityState.Deleted;
                efDbContext.SaveChanges();
                #endregion                
                return Json(new ResponseInfo(1, "操作成功", null));
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "删除过程发生异常", null));
            }
        }
        [HttpPost]
        public JsonResult GetAccessControlEquipmentNode(int Id)
        {
            if (Id == 0)
            {
                return Json(new ResponseInfo(0, "查询ID不能为0", null));
            }
            AccessControlEquipmentNodeModel model = efDbContext.AccessControlEquipmentNode.Find(Id);
            if (model == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", model));
        }
    }
}