using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyOperate.Common.Enums
{
    public enum EquipmentTypeEnum
    {
        /// <summary>
        /// 分布式门禁控制器
        /// </summary>
        SplitAccessControl=1,
        /// <summary>
        /// 一体式门禁控制器
        /// </summary>
        IntegratedAccessControl = 2,
        /// <summary>
        /// 一体式电梯楼层控制器
        /// </summary>
        IntegratedElevatorAccessControl = 3,
        /// <summary>
        /// 分布式电梯楼层控制器
        /// </summary>
        SplitElevatorAccessControl = 4
    }
}
