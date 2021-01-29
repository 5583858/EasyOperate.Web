using EasyOperate.Web.Models;
using EasyOperate.Web.Models.AccessControl;
using System;
using System.Linq;
using static EasyOperate.Web.Models.AccessControlModel.PushAccessControlRecord;

namespace EasyOperate.Web.Manager
{
    public class PushAccessManager
    {
        EfDbContext efDbContext = new EfDbContext();

        public void Save(PushAccessControlRecordModel pushAccessControlRecordModel)
        {
            AccessControlEquipmentModel equipment = efDbContext.AccessControlEquipment.Where(o => o.Serialno == pushAccessControlRecordModel.DeviceCode).FirstOrDefault();

            pushAccessControlRecordModel.LibMatInfoList.ForEach(info => {
                BaseUserModel user = efDbContext.BaseUser.Where(o => o.ID == Convert.ToInt32(info.MatchPersonID)).FirstOrDefault();

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
            });

            efDbContext.SaveChanges();
        }
    }
}