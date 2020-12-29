using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EasyOperate.Web.Models;
using EasyOperate.Web.Models.AccessControl;
using EasyOperate.Common.Enums;
using System.Net;

namespace EasyOperate.Web.Controllers
{
    [AllowAnonymous]
    public class ProjectController : EasyOperateBaseController
    {
        EfDbContext efDbContext = new EfDbContext();

        // GET: Project
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public NodeTreeItem ModelToTreeNode(ProjectModel model)
        {
            NodeTreeItem nodeTreeItem = new NodeTreeItem();
            nodeTreeItem.id = model.ID;
            nodeTreeItem.title = model.Name;
            nodeTreeItem.ptypeid = PTypeIdEnum.Project;
            nodeTreeItem.pid = 0;
            nodeTreeItem.index = 0;
            nodeTreeItem.ischecked = true;            
            nodeTreeItem.TerminalNodes = null;
            nodeTreeItem.Childrens = null;
            return nodeTreeItem;
        }
        [HttpPost]
        public JsonResult Create(ProjectModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new ResponseInfo(0, "请输入项目名", null));
                }
                if (efDbContext.Project.Where(p =>p.Name == model.Name).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "项目名不能重复", null));
                }
                NodeTreeItem nodeTreeItem= ModelToTreeNode(model);
                efDbContext.Project.Add(model);
                efDbContext.SaveChanges();
                return Json(new ResponseInfo(1, "操作成功", nodeTreeItem));
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "创建过程发生异常", null));
            }           
        }
        [HttpPost]
        public JsonResult Edit(ProjectModel model)
        {
            try
            {
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                if (string.IsNullOrEmpty(model.Name))
                {
                    return Json(new ResponseInfo(0, "请输入项目名", null));
                }
                if (efDbContext.Project.Where(p => p.Name == model.Name&&p.ID!=model.ID).FirstOrDefault() != null)
                {
                    return Json(new ResponseInfo(0, "项目名不能重复", null));
                }
                NodeTreeItem nodeTreeItem = ModelToTreeNode(model);
                efDbContext.Entry(model).State = EntityState.Modified;
                efDbContext.SaveChanges();
                return Json(new ResponseInfo(1, "操作成功", nodeTreeItem));
            }
            catch(Exception ex)
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
                ProjectModel model = efDbContext.Project.Find(Id);
                if (model == null)
                {
                    return Json(new ResponseInfo(0, "查找的项不存在", null));
                }
                SubRegionModel result = efDbContext.SubRegion.Where(s => s.ProjectId == Id).FirstOrDefault();
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
        public JsonResult GetControllerNames()
        {
            try
            {
                string[] ControllerNames = Enum.GetNames(typeof(PTypeIdEnum));
                return Json(new ResponseInfo(1, "操作成功", ControllerNames));
            }
            catch(Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "操作过程发生异常", null));
            }
            
        }
        //[HttpPost]
        //public JsonResult GetControllerNames(int Id)
        //{
        //    string[] ControllerNames = Enum.GetNames(typeof(PTypeIdEnum));
        //    if (Id >= ControllerNames.Length)
        //    {
        //        return Json(new ResponseInfo(0, "参数错误", null));
        //    }
        //    return Json(new ResponseInfo(1, "操作成功", ControllerNames[Id]));
        //}
        [HttpPost]
        public JsonResult GetProject(int Id)
        {
            if (Id == 0)
            {
                return Json(new ResponseInfo(0, "查询ID不能为0", null));
            }
            ProjectModel projectModel = efDbContext.Project.Find(Id);
            if (projectModel == null)
            {
                return Json(new ResponseInfo(0, "查找的项不存在", null));
            }
            return Json(new ResponseInfo(1, "操作成功", projectModel));
        }
        [HttpPost]
        public JsonResult GetProjectTree()
        {
            try
            {
                List<NodeTreeItem> ProjectTree = new List<NodeTreeItem>();
                List<ProjectModel> ProjectList = efDbContext.Project.ToList();
                foreach (var project in ProjectList)
                {
                    NodeTreeItem ProjectNode = new NodeTreeItem();
                    ProjectNode.id = project.ID;
                    ProjectNode.pid = 0;
                    ProjectNode.ptypeid = PTypeIdEnum.Project;
                    ProjectNode.title = project.Name;
                    ProjectNode.index = 0;
                    ProjectNode.ischecked = false;
                    ProjectNode.Childrens = new List<NodeTreeItem>();
                    List<SubRegionModel> SubRegionList = efDbContext.SubRegion.Where(s => s.ProjectId == project.ID).ToList();
                    foreach (var subRegion in SubRegionList)
                    {
                        NodeTreeItem SubRegionNode = new NodeTreeItem();
                        SubRegionNode.id = subRegion.ID;
                        SubRegionNode.pid = project.ID;
                        SubRegionNode.ptypeid = PTypeIdEnum.SubRegion;
                        SubRegionNode.title = subRegion.Name;
                        SubRegionNode.index = 0;
                        SubRegionNode.ischecked = false;
                        SubRegionNode.Childrens = new List<NodeTreeItem>();
                        List<HouseModel> HouseList = efDbContext.House.Where(h => h.SubRegionId == subRegion.ID).ToList();
                        foreach (var house in HouseList)
                        {
                            NodeTreeItem HouseNode = new NodeTreeItem();
                            HouseNode.id = house.ID;
                            HouseNode.pid = subRegion.ID;
                            HouseNode.ptypeid = PTypeIdEnum.House;
                            HouseNode.title = house.Name;
                            HouseNode.index = 0;
                            HouseNode.ischecked = false;
                            HouseNode.Childrens = new List<NodeTreeItem>();
                            List<HousePartModel> HousePartList = efDbContext.HousePart.Where(hp => hp.HouseId == house.ID).ToList();
                            foreach (var housePart in HousePartList)
                            {
                                NodeTreeItem HousePartNode = new NodeTreeItem();
                                HousePartNode.id = housePart.ID;
                                HousePartNode.pid = house.ID;
                                HousePartNode.ptypeid = PTypeIdEnum.HousePart;
                                HousePartNode.title = housePart.Name;
                                HousePartNode.index = 0;
                                HousePartNode.ischecked = false;
                                HousePartNode.Childrens = new List<NodeTreeItem>();
                                List<AccessControlEquipmentModel> AccessControlEquipmentList = efDbContext.AccessControlEquipment.Where(e => e.HousePartId == housePart.ID).ToList();
                                foreach (var accessControlEquipment in AccessControlEquipmentList)
                                {
                                    NodeTreeItem EquipmentItem = new NodeTreeItem();
                                    EquipmentItem.id = accessControlEquipment.ID;
                                    EquipmentItem.pid = housePart.ID;
                                    EquipmentItem.ptypeid = PTypeIdEnum.AccessControlEquipment ;
                                    EquipmentItem.title = accessControlEquipment.Name;
                                    EquipmentItem.index = 0;
                                    EquipmentItem.ischecked = false;
                                    EquipmentItem.TerminalNodes = new List<TerminalNode>();
                                    List<AccessControlEquipmentNodeModel> AccessControlEquipmentNodeList = efDbContext.AccessControlEquipmentNode.Where(en => en.AccessControlEquipmentId == accessControlEquipment.ID).ToList();
                                    foreach (var accessControlEquipmentNode in AccessControlEquipmentNodeList)
                                    {
                                        TerminalNode terminalNode = new TerminalNode();
                                        terminalNode.id = accessControlEquipmentNode.ID;
                                        terminalNode.pid = accessControlEquipment.ID;
                                        terminalNode.ptypeid = PTypeIdEnum.AccessControlEquipmentNode ;
                                        terminalNode.title = accessControlEquipmentNode.Name;
                                        terminalNode.index = 0;
                                        terminalNode.ischecked = false;
                                        EquipmentItem.TerminalNodes.Add(terminalNode);
                                    }
                                    HousePartNode.Childrens.Add(EquipmentItem);
                                }
                                HouseNode.Childrens.Add(HousePartNode);
                            }
                            SubRegionNode.Childrens.Add(HouseNode);
                        }
                        ProjectNode.Childrens.Add(SubRegionNode);
                    }
                    ProjectTree.Add(ProjectNode);
                }
                return Json(new ResponseInfo(1, "成功", ProjectTree));
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "查询过程发生异常", null));
            }            
        }
        [HttpPost]
        public JsonResult GetProjectTreeAll()
        {
            try
            {
                List<NodeTreeItem> ProjectTree = new List<NodeTreeItem>();
                List<ProjectModel> ProjectList = efDbContext.Project.ToList();
                foreach (var project in ProjectList)
                {
                    NodeTreeItem ProjectNode = new NodeTreeItem();
                    ProjectNode.id = project.ID;
                    ProjectNode.pid = 0;
                    ProjectNode.ptypeid = PTypeIdEnum.Project;
                    ProjectNode.title = project.Name;
                    ProjectNode.index = 0;
                    ProjectNode.ischecked = false;
                    ProjectNode.Childrens = new List<NodeTreeItem>();
                    List<SubRegionModel> SubRegionList = efDbContext.SubRegion.Where(s => s.ProjectId == project.ID).ToList();
                    foreach (var subRegion in SubRegionList)
                    {
                        NodeTreeItem SubRegionNode = new NodeTreeItem();
                        SubRegionNode.id = subRegion.ID;
                        SubRegionNode.pid = project.ID;
                        SubRegionNode.ptypeid = PTypeIdEnum.SubRegion;
                        SubRegionNode.title = subRegion.Name;
                        SubRegionNode.index = 0;
                        SubRegionNode.ischecked = false;
                        SubRegionNode.Childrens = new List<NodeTreeItem>();
                        List<HouseModel> HouseList = efDbContext.House.Where(h => h.SubRegionId == subRegion.ID).ToList();
                        foreach (var house in HouseList)
                        {
                            NodeTreeItem HouseNode = new NodeTreeItem();
                            HouseNode.id = house.ID;
                            HouseNode.pid = subRegion.ID;
                            HouseNode.ptypeid = PTypeIdEnum.House;
                            HouseNode.title = house.Name;
                            HouseNode.index = 0;
                            HouseNode.ischecked = false;
                            HouseNode.Childrens = new List<NodeTreeItem>();
                            List<HousePartModel> HousePartList = efDbContext.HousePart.Where(hp => hp.HouseId == house.ID).ToList();
                            foreach (var housePart in HousePartList)
                            {
                                NodeTreeItem HousePartNode = new NodeTreeItem();
                                HousePartNode.id = housePart.ID;
                                HousePartNode.pid = house.ID;
                                HousePartNode.ptypeid = PTypeIdEnum.HousePart;
                                HousePartNode.title = housePart.Name;
                                HousePartNode.index = 0;
                                HousePartNode.ischecked = false;
                                HousePartNode.Childrens = new List<NodeTreeItem>();
                                List<AccessControlEquipmentModel> AccessControlEquipmentList = efDbContext.AccessControlEquipment.Where(e => e.HousePartId == housePart.ID).ToList();
                                foreach (var accessControlEquipment in AccessControlEquipmentList)
                                {
                                    NodeTreeItem EquipmentItem = new NodeTreeItem();
                                    EquipmentItem.id = accessControlEquipment.ID;
                                    EquipmentItem.pid = housePart.ID;
                                    EquipmentItem.ptypeid = PTypeIdEnum.AccessControlEquipment;
                                    EquipmentItem.title = accessControlEquipment.Name;
                                    EquipmentItem.index = 0;
                                    EquipmentItem.ischecked = false;
                                    EquipmentItem.Childrens = new List<NodeTreeItem>();
                                    List<AccessControlEquipmentNodeModel> AccessControlEquipmentNodeList = efDbContext.AccessControlEquipmentNode.Where(en => en.AccessControlEquipmentId == accessControlEquipment.ID).ToList();
                                    foreach (var accessControlEquipmentNode in AccessControlEquipmentNodeList)
                                    {
                                        NodeTreeItem EquipmentNodeItem = new NodeTreeItem();
                                        EquipmentNodeItem.id = accessControlEquipmentNode.ID;
                                        EquipmentNodeItem.pid = accessControlEquipment.ID;
                                        EquipmentNodeItem.ptypeid = PTypeIdEnum.AccessControlEquipmentNode;
                                        EquipmentNodeItem.title = accessControlEquipmentNode.Name;
                                        EquipmentNodeItem.index = 0;
                                        EquipmentNodeItem.ischecked = false;
                                        EquipmentNodeItem.Childrens = null;
                                        EquipmentItem.Childrens.Add(EquipmentNodeItem);
                                    }
                                    HousePartNode.Childrens.Add(EquipmentItem);
                                }
                                HouseNode.Childrens.Add(HousePartNode);
                            }
                            SubRegionNode.Childrens.Add(HouseNode);
                        }
                        ProjectNode.Childrens.Add(SubRegionNode);
                    }
                    ProjectTree.Add(ProjectNode);
                }
                return Json(new ResponseInfo(1, "成功", ProjectTree));
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "查询过程发生异常", null));
            }
        }
    }
}