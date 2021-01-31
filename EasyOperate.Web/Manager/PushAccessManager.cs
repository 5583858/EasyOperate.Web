using EasyOperate.Web.Models;
using EasyOperate.Web.Models.AccessControl;
using System;
using System.Linq;

namespace EasyOperate.Web.Manager
{
    public class PushAccessManager
    {
        EfDbContext efDbContext = new EfDbContext();

        public void Save(string deviceCode, int personCode)
        {
            AccessControlEquipmentModel equipment = efDbContext.AccessControlEquipment.Where(o => o.Serialno == deviceCode).FirstOrDefault();

            BaseUserModel user = efDbContext.BaseUser.Where(o => o.ID == personCode).FirstOrDefault();

            if (equipment != null && user != null)
            {
                AccessControlRecordModel accessControlRecord = new AccessControlRecordModel();

                accessControlRecord.RealName = user.RealName;
                accessControlRecord.Gender = user.Gender;
                //ProjectName
                //SubRegionName
                accessControlRecord.HouseNumber = equipment.HouseName;
                accessControlRecord.HousePartName = equipment.HousePartName;
                //FloorName
                //RoomNumber
                accessControlRecord.AccessControlEquipmentName = equipment.Name;
                //NodeName
                //CardTypeName
                accessControlRecord.Direction = equipment.Direction;
                //DoorOpeningTypeName
                //OperationUserName
                accessControlRecord.CreateTime = DateTime.Now;
                accessControlRecord.UserId = user.ID.ToString();
                //ProjectId
                accessControlRecord.SubRegionId = equipment.SubRegionId;
                accessControlRecord.HouseId = equipment.HouseId;
                accessControlRecord.HousePartId = equipment.HousePartId;
                //FloorId
                //RoomId
                accessControlRecord.AccessControlEquipmentId = equipment.ID.ToString();
                //NodeId
                //CardType
                //DoorOpeningType
                //OperationUserId

                efDbContext.AccessControlRecord.Add(accessControlRecord);
                efDbContext.SaveChanges();
            }
        }

        //public void Save(PushAccessControlRecordModel pushAccessControlRecordModel)
        //{
        //    AccessControlEquipmentModel equipment = efDbContext.AccessControlEquipment.Where(o => o.Serialno == pushAccessControlRecordModel.DeviceCode).FirstOrDefault();

        //    pushAccessControlRecordModel.LibMatInfoList.ForEach(info => {
        //        if (string.IsNullOrEmpty(info.MatchPersonInfo.PersonCode))
        //        {
        //            return;
        //        }

        //        BaseUserModel user = efDbContext.BaseUser.Where(o => o.ID == Convert.ToInt32(info.MatchPersonInfo.PersonCode)).FirstOrDefault();

        //        AccessControlRecordModel accessControlRecord = new AccessControlRecordModel();

        //        accessControlRecord.RealName = user.RealName;
        //        accessControlRecord.Gender = user.Gender;
        //        //ProjectName
        //        //SubRegionName
        //        accessControlRecord.HouseNumber = equipment.HouseName;
        //        accessControlRecord.HousePartName = equipment.HousePartName;
        //        //FloorName
        //        //RoomNumber
        //        accessControlRecord.AccessControlEquipmentName = equipment.Name;
        //        //NodeName
        //        //CardTypeName
        //        accessControlRecord.Direction = equipment.Direction;
        //        //DoorOpeningTypeName
        //        //OperationUserName
        //        accessControlRecord.CreateTime = DateTime.Now;
        //        accessControlRecord.UserId = user.ID.ToString();
        //        //ProjectId
        //        accessControlRecord.SubRegionId = equipment.SubRegionId;
        //        accessControlRecord.HouseId = equipment.HouseId;
        //        accessControlRecord.HousePartId = equipment.HousePartId;
        //        //FloorId
        //        //RoomId
        //        accessControlRecord.AccessControlEquipmentId = equipment.ID.ToString();
        //        //NodeId
        //        //CardType
        //        //DoorOpeningType
        //        //OperationUserId

        //        efDbContext.AccessControlRecord.Add(accessControlRecord);
        //    });

        //    efDbContext.SaveChanges();
        //}
    }
}