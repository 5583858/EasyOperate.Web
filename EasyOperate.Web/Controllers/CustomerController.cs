using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;
using System.Threading.Tasks;
using EasyOperate.Web.Models;
using EasyOperate.Web.Models.CustomerModel;
using EasyOperate.Common;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Web;
using System.IO;
using EasyOperate.Web.Models.AccessControl;
using EasyOperate.Common.Enums;
using EasyOperate.Web.Controllers.AccessControlApi;
using EasyOperate.Web.Models.AccessControlRequest;
using EasyOperate.Web.Models.AccessControlModel;
using EasyOperate.Web.Manager;

namespace EasyOperate.Web.Controllers
{
    [AllowAnonymous]
    public class CustomerController : EasyOperateBaseController
    {
        // GET: Customer
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<BaseUserModel> BaseUserList = db.BaseUser;
            return View(BaseUserList);
        }
        [HttpPost]
        public JsonResult GetUserList()
        {
            try
            {
                List<BaseUserModel> BaseUserList = db.BaseUser.ToList();
                List<BaseUserViewModel> baseUserViewModelList = new List<BaseUserViewModel>();  
                foreach (BaseUserModel bu in BaseUserList)
                {
                    BaseUserViewModel newModel = new BaseUserViewModel();
                    newModel.SubRegionId = bu.SubRegionId;
                    newModel.HouseId = bu.HouseId;
                    newModel.HousePartId = bu.HousePartId;
                    newModel.FloorId = bu.FloorId;
                    newModel.RoomId = bu.RoomId;
                    newModel.Address = bu.Address;
                    newModel.Email = bu.Email;
                    newModel.Gender = bu.Gender;
                    newModel.Note = bu.Note;
                    newModel.PhoneNumber = bu.PhoneNumber;
                    newModel.RealName = bu.RealName;
                    newModel.IdentityUserId = bu.IdentityUserId;
                    newModel.IdentityCardNumber = bu.IdentityCardNumber;
                    newModel.SubRegion = db.SubRegion.Find(bu.SubRegionId)==null?"": db.SubRegion.Find(bu.SubRegionId).Name;
                    newModel.House = db.House.Find(bu.HouseId)==null?"": db.House.Find(bu.HouseId).Name;
                    newModel.HousePart = db.HousePart.Find(bu.HousePartId)==null?"": db.HousePart.Find(bu.HousePartId).Name;
                    newModel.Floor = db.Floor.Find(bu.FloorId)==null?"": db.Floor.Find(bu.FloorId).Name;
                    newModel.Room = db.Room.Find(bu.RoomId)==null?"": db.Room.Find(bu.RoomId).Name;
                    baseUserViewModelList.Add(newModel);
                }

                return Json(new ResponseInfo(0, "查询成功", baseUserViewModelList));
            }
            catch(Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "操作过程发生异常", null));
            }
            
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UpImage(int UserId, HttpPostedFileBase Image)
        {
            try
            {
                if (Image == null || Image.ContentLength <= 0)
                {
                    return Json(new ResponseInfo(0, "请按要求上传图片!", null));
                }
                BaseUserModel userModel= db.BaseUser.Find(UserId);
                if(userModel == null)
                {
                    return Json(new ResponseInfo(0, "查找的客户不存在!", null));
                }
                string imgPath = CommonParameters.ServerImagePath + Path.DirectorySeparatorChar;
                if (!Directory.Exists(Server.MapPath(imgPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(imgPath));
                }
                string fileName = userModel.ID + ".jpg";
                string filePath = Path.Combine(HttpContext.Server.MapPath(imgPath), fileName);
                byte[] imageData = new byte[Image.ContentLength];
                Image.InputStream.Read(imageData, 0, Image.ContentLength);
                Image.SaveAs(filePath);
                UserPhotoModel userPhotoModel = new UserPhotoModel();
                userPhotoModel.ImgType = Image.ContentType;
                userPhotoModel.BaseUserId = userModel.ID;
                userPhotoModel.Path = Server.MapPath(imgPath) + "\"" + filePath;
                db.UserPhoto.Add(userPhotoModel);
                db.SaveChanges();
                return Json(new ResponseInfo(1, "操作成功", fileName));
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "操作过程发生异常", null));
            }
        }
        [HttpPost]
        public JsonResult AddCard(AccessControlCardModel model)
        {
            try
            {
                DateTime dateTimeNow =DateTime.Now;
                BaseUserModel userModel = db.BaseUser.Find(model.BaseUserId);
                if (userModel == null)
                {
                    return Json(new ResponseInfo(0, "未找到用户信息!", null));
                }
                //建卡
                //AccessControlCardModel cardModel = new AccessControlCardModel();
                //model.AccessControlStartTime = model.StartTime == null ? dateTimeNow : model.StartTime.Value;
                //model.AccessControlEndTime = model.EndTime == null ? dateTimeNow : model.EndTime.Value;
                //model.AccessControlUpTime = model.EndTime == null ? dateTimeNow : model.EndTime.Value;
                //model.CardTypeID = 1;
                if (string.IsNullOrEmpty(model.CardNo))
                {
                    model.CardNo = "00000000";
                }
                DateTime time2000 = new DateTime(2000, 1, 1);
                model.HeadImagePath = "userPhotoModel.Path";
                model.AccessControlStartTime = model.AccessControlStartTime< time2000 ? dateTimeNow: model.AccessControlStartTime;
                model.AccessControlEndTime= model.AccessControlEndTime <time2000? dateTimeNow : model.AccessControlEndTime;

                db.AccessControlCard.Add(model);
                db.SaveChanges();
                return Json(new ResponseInfo(1, "操作成功", null));
            }
            
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "操作过程发生异常", null));
            }
        }
        [HttpPost]
        public JsonResult DeleteCard(int CardId)
        {
            try
            {
                AccessControlCardModel cardModel = db.AccessControlCard.Find(CardId);
                if (cardModel == null)
                {
                    return Json(new ResponseInfo(0, "未找到卡信息!", null));
                }
                //建卡
                //AccessControlCardModel cardModel = new AccessControlCardModel();
                //model.AccessControlStartTime = model.StartTime == null ? dateTimeNow : model.StartTime.Value;
                //model.AccessControlEndTime = model.EndTime == null ? dateTimeNow : model.EndTime.Value;
                //model.AccessControlUpTime = model.EndTime == null ? dateTimeNow : model.EndTime.Value;
                //model.CardTypeID = 1;
                db.Entry(cardModel).State=EntityState.Deleted;
                db.SaveChanges();
                return Json(new ResponseInfo(1, "操作成功", null));
            }

            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "操作过程发生异常", null));
            }
        }
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]//[Bind(Include = "RoomId,Email,RealName,Gender,IdentityCardNumber,PhoneNumber,Address,Note")]
        public async Task<JsonResult> Create(BaseUserViewModel model)
        {
            try
            {
                //if (ModelState.IsValid)
                {
                    DateTime dateTimeNow = DateTime.Now;
                    if (string.IsNullOrEmpty(model.RealName) || string.IsNullOrEmpty(model.PhoneNumber))
                    {
                        return Json(new ResponseInfo(0, "请添写用户名和电话号", null));
                    }
                    RoomModel roomModel = db.Room.Find(model.RoomId);
                    //if(roomModel==null)
                    //{
                    //    return Json(new ResponseInfo(0, "未找到用户的房号", null));
                    //}
                    var UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                    BaseModel baseDataUser = db.BaseUser.Where(bu => bu.RoomId == model.RoomId && bu.RealName == model.RealName).FirstOrDefault();
                    BaseModel baseDataUserPhonNumber = db.BaseUser.Where(bu => bu.PhoneNumber == model.PhoneNumber).FirstOrDefault();
                    if (baseDataUser != null)
                    {
                        return Json(new ResponseInfo(0, "用户已存在", null));
                    }
                    if (baseDataUserPhonNumber != null)
                    {
                        return Json(new ResponseInfo(0, "电话号码已注册", null));
                    }
                    if (!string.IsNullOrEmpty(model.Email))
                    {
                        BaseModel baseDataUserEmail = db.BaseUser.Where(bu => bu.Email == model.Email).FirstOrDefault();
                        if (baseDataUserPhonNumber != null)
                        {
                            return Json(new ResponseInfo(0, "电子邮件已注册", null));
                        }
                    }
                   
                    var appUser = new ApplicationUser
                    {
                        UserName = model.PhoneNumber,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        CreateTime = dateTimeNow,
                        UpdateTime = dateTimeNow
                    };
                    var result = await UserManager.CreateAsync(appUser, "Hy.123456");
                    if (result.Succeeded)
                    {
                        //string temp=null;
                        var r = await UserManager.AddToRolesAsync(appUser.Id, RoleType.CUSTOMER);
                        BaseUserModel newUserModel = new BaseUserModel();
                        newUserModel.SubRegionId = model.SubRegionId;
                        newUserModel.HouseId = model.HouseId;
                        newUserModel.HousePartId = model.HousePartId;
                        newUserModel.FloorId = model.FloorId;
                        newUserModel.RoomId = model.RoomId;
                        newUserModel.Address = model.Address;
                        newUserModel.Email = model.Email;
                        newUserModel.Gender = model.Gender;
                        newUserModel.Note = model.Note;
                        newUserModel.PhoneNumber = model.PhoneNumber;
                        newUserModel.RealName = model.RealName;
                        newUserModel.IdentityUserId = model.IdentityUserId;
                        newUserModel.IdentityCardNumber = model.IdentityCardNumber;
                        newUserModel.CreateTime = dateTimeNow;
                        newUserModel.UpdateTime = dateTimeNow;
                        newUserModel.IdentityUserId = appUser.Id;
                        
                        db.BaseUser.Add(newUserModel);
                        db.SaveChanges();

                        

                        

                        ////如果客户选择自动授权
                        if (model.AutoAuthorizeds)
                        {
                            var equipments = (from equipment in db.AccessControlEquipment
                                              join equipmentNode in db.AccessControlEquipmentNode on equipment.ID equals equipmentNode.ID
                                              where equipment.HouseId == newUserModel.HousePartId
                                              select equipmentNode);
                            //var equipmentList= db.AccessControlEquipment.Where(ace=>ace.HousePartId==newUserModel.HousePartId);

                            var EquipmentNodeGroupBys = db.AccessControlEquipmentNode.Where(acen => acen.HousePartId == newUserModel.HousePartId).GroupBy(p => p.AccessControlEquipmentId);
                            var floor = db.Floor.Find(newUserModel.FloorId);

                            foreach (var equipmentNode in EquipmentNodeGroupBys)
                            {
                                Com.log.Debug("设备授权!");
                                AccessControlCardAuthorizedModel cardAuthorizedModel = new AccessControlCardAuthorizedModel();
                                cardAuthorizedModel.EquipmentId = equipmentNode.Key;
                                //cardAuthorizedModel.CardId = cardModel.ID;
                                ulong node = 0;
                                foreach (var equNode in equipmentNode)
                                {
                                    if (equNode.equipmentTypeEnum == EquipmentTypeEnum.SplitElevatorAccessControl)
                                    {
                                        if (equNode.NodeNumber == floor.FloorNumber)
                                        {
                                            cardAuthorizedModel.Nodes = equNode.Node;
                                        }
                                    }
                                    else if (equNode.equipmentTypeEnum == EquipmentTypeEnum.IntegratedAccessControl)
                                    {
                                        cardAuthorizedModel.Nodes = equNode.Node;
                                    }
                                    else if (equNode.equipmentTypeEnum == EquipmentTypeEnum.SplitAccessControl)
                                    {
                                        node |= equNode.Node;
                                    }
                                    Com.log.Debug("子设备授权!");
                                }
                                cardAuthorizedModel.CreateTime = dateTimeNow;
                                cardAuthorizedModel.UpTime = dateTimeNow;
                                //向设备添加客户
                                BaseRequestController.Get(BasicRequestUrl.KeepAliveInfoUrl);
                                //添加成功后保存
                                db.AccessControlCardAuthorized.Add(cardAuthorizedModel);
                                db.SaveChanges();
                            }

                            //PersonRequestModel personRequestModel = new PersonRequestModel();
                            //string host = "http://192.168.1.103";
                            //temp= BaseRequestController.Get(host+BasicRequestUrl.DeviceBasicInfoUrl);

                            //TODO
                            //查询授权相机集合
                            List<AccessControlEquipmentModel> equipmentList = null;

                            //TODO
                            //用户照片信息
                            UserPhotoModel userPhotoModel = null;

                            if (equipmentList != null)
                            {
                                equipmentList.ForEach(equipment => {
                                    PeopleManager peopleManager = new PeopleManager(equipment);
                                    peopleManager.AddPeopleInfo(newUserModel, userPhotoModel);
                                });
                            }

                        }
                        return Json(new ResponseInfo(1, "注册成功", null));
                    }
                }
                return Json(new ResponseInfo(0, "注册失败", null));
            }
            catch(Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "操作过程发生异常", null));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(BaseUserModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.RealName) || string.IsNullOrEmpty(model.PhoneNumber))
                {
                    return Json(new ResponseInfo(0, "请添写用户名和电话号", null));
                }
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                DateTime dateTimeNow = DateTime.Now;
                if (ModelState.IsValid)
                {
                    BaseModel baseDataUser = db.BaseUser.Where(bu => bu.RoomId == model.RoomId && bu.RealName == model.RealName && bu.ID != model.ID).FirstOrDefault();
                    if (baseDataUser != null)
                    {
                        return Json(new ResponseInfo(0, "该用户已存在", null));
                    }
                    BaseModel baseDataUserPhonNumber = db.BaseUser.Where(bu => bu.PhoneNumber == model.PhoneNumber && bu.ID != model.ID).FirstOrDefault();
                    if (baseDataUserPhonNumber != null)
                    {
                        return Json(new ResponseInfo(0, "电话号码已注册", null));
                    }
                    if (!string.IsNullOrEmpty(model.Email))
                    {
                        BaseModel baseDataUserEmail = db.BaseUser.Where(bu => bu.Email == model.Email && bu.ID != model.ID).FirstOrDefault();
                        if (baseDataUserPhonNumber != null)
                        {
                            return Json(new ResponseInfo(0, "电子邮件已注册", null));
                        }
                    }

                    ApplicationUser applicationUser = db.Users.Find(model.IdentityUserId);

                    if (applicationUser != null)
                    {
                        applicationUser.UserName = model.PhoneNumber;
                        applicationUser.PhoneNumber = model.PhoneNumber;
                        applicationUser.Email = model.Email;
                        applicationUser.UpdateTime = dateTimeNow;


                        DbEntityEntry entry = db.Entry(applicationUser);
                        entry.State = EntityState.Modified;
                        entry.Property(nameof(applicationUser.CreateTime)).IsModified = false;
                        db.SaveChanges();

                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new ResponseInfo(1, "修改成功", model));
                    }                    
                }
                return Json(new ResponseInfo(0, "操作失败", null));
            }
            catch (Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "操作过程发生异常", null));
            }
        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            DetailsViewModel detailsViewModel = new DetailsViewModel();
            BaseUserModel baseUserModel=db.BaseUser.Find(Id);
            if(baseUserModel!=null)
            {
                detailsViewModel.Address = baseUserModel.Address;
                detailsViewModel.Email = baseUserModel.Email;
                detailsViewModel.Gender = baseUserModel.Gender;
                detailsViewModel.IdentityCardNumber = baseUserModel.IdentityCardNumber;
                detailsViewModel.Note = baseUserModel.Note;
                detailsViewModel.PhoneNumber = baseUserModel.PhoneNumber;
                detailsViewModel.RealName = baseUserModel.RealName;
                detailsViewModel.RoomId = baseUserModel.RoomId;
                UserPhotoModel userPhotoModel = db.UserPhoto.Where(up=>up.BaseUserId==baseUserModel.ID).FirstOrDefault();
                if(userPhotoModel!=null)
                {
                    detailsViewModel.ImgUrl = userPhotoModel.Path;
                }                
            }
            return View(detailsViewModel);
        }
        [HttpPost]
        public JsonResult Details(DetailsViewModel detailsViewModel)
        {
            return Json(detailsViewModel);
        }
        [HttpPost]
        public JsonResult GetUser(int Id)
        {
            try
            {
                BaseUserModel userModel = db.BaseUser.Find(Id);
                if (userModel == null)
                {
                    return Json(new ResponseInfo(0, "查找的用户不存在", null));
                }
                ApplicationUser applicationUser = db.Users.Find(userModel.IdentityUserId);
                if (applicationUser == null)
                {
                    return Json(new ResponseInfo(0, "查找的可登录用户不存在", null));
                }
                return Json(new ResponseInfo(1, "查询成功", userModel));
            }
            catch(Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "操作过程发生异常", null));
            }
            
        }
        //public FileContentResult GetImage(int? id)
        //{
        //    if (id == null)
        //    {
        //        return null;
        //    }
        //    Venue venue = db.Venues.FirstOrDefault(o => o.Id == id);
        //    if (venue.ImageData != null && venue.ImageType != null)
        //    {
        //        return File(venue.ImageData, venue.ImageType);
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}
        [HttpPost]
        public JsonResult GetUser(string Id)
        {
            try
            {
                if (Id == null)
                {
                    return Json(new ResponseInfo(0, "Id不能空", null));
                }
                ApplicationUser applicationUser = db.Users.Find(Id);
                if (applicationUser == null)
                {
                    return Json(new ResponseInfo(0, "查找的可登录用户不存在", null));
                }
                BaseUserModel userModel = db.BaseUser.Where(bu => bu.IdentityUserId == applicationUser.Id).FirstOrDefault();
                if (userModel == null)
                {
                    return Json(new ResponseInfo(0, "查找的用户不存在", null));
                }
                return Json(new ResponseInfo(1, "查询成功", applicationUser));
            }
            catch(Exception ex)
            {
                Com.log.Error(ex);
                return Json(new ResponseInfo(0, "操作过程发生异常", null));
            }
        }
        public JsonResult EquipmentAddPeople(BaseUserModel baseUserModel)
        {
            PersonRequestModel personRequestModel = new PersonRequestModel();
            personRequestModel.Num = 1;
            //personRequestModel.PersonInfoList = new List<PersonInfo>();
            PersonInfo personInfo = new PersonInfo();
            personInfo.PersonID = (ulong)baseUserModel.ID;
            personInfo.LastChange = (ulong)CommonFunctions.ConvertDateTimeToInt10(baseUserModel.UpdateTime.Value);
            personInfo.PersonCode = baseUserModel.ID.ToString();
            personInfo.PersonName = baseUserModel.RealName;
            personInfo.Remarks = "";
            personInfo.TimeTemplateNum = 0;
            personInfo.TimeTemplateList = null;
            personInfo.IdentificationNum = 1;//////////////

            Identification identification = new Identification();
            identification.Type = 1;
            identification.Number = "asdfasdfas";

            personInfo.ImageNum = 1;
            FaceImage faceImage = new FaceImage();
            faceImage.FaceID =(ulong) baseUserModel.ID;
            //image.Name=baseUserModel.


            return Json(true);




        }
        
    }
}