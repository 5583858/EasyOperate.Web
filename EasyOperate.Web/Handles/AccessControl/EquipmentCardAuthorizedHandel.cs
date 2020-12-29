using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyOperate.Web.Models;
using EasyOperate.Web.Handle;
using EasyOperate.Web.Models.AccessControlModel;

namespace EasyOperate.Web.Handle.AccessControl
{
    public class EquipmentAddPeopleHandel :BaseHandel
    {
        public void EquipmentAddPeople(PersonRequestModel personRequestModel)
        {
            if(personRequestModel==null)
            {
                return;
            }
            var EquipmentList= db.AccessControlEquipment.ToList();

        }
    }
}