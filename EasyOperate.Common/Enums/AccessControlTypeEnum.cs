using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyOperate.Common.Enums
{
    /// <summary>
    /// 开门类型
    /// </summary>
    public enum DoorOpeningTypeEnum
    {
        Face=1,
        Card=2,
        QRCode=3,
        Manual=4
    }
    /// <summary>
    /// 设备方向
    /// </summary>
    public enum DirectionEnum
    {
        In=0,
        Out=1,
    }
    public enum RunningStateEnum
    {
        Enable=1,
        Disable=2,
        Busy =3
    }
    public enum CardTypeEnum
    {
        /// <summary>
        /// 客户
        /// </summary>
        User=1,
        /// <summary>
        /// 访客
        /// </summary>
        Visitor=2,
        /// <summary>
        /// 员工
        /// </summary>
        Staff=3,
        /// <summary>
        /// 合作方
        /// </summary>
        CooperationUnit=4
    }
    public enum PTypeIdEnum
    {
        Root=0,
        Project=1,
        SubRegion=2,
        House=3,
        HousePart=4,
        AccessControlEquipment =5,
        AccessControlEquipmentNode =6,
        Floor=7,
        Room=8
    }
}
