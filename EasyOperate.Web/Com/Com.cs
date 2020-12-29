using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyOperate.Web.Models;

namespace EasyOperate.Web
{
    public class Com
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public static List<NodeTreeItem> GetProjectTree(IQueryable<ProjectModel> efProjectTree)
        //{
            
        //    foreach (var efp in efProjectTree)
        //    {
        //        NodeTreeItem NodeItme = new NodeTreeItem();
        //        NodeItme.Id = efp.ID;
        //        NodeItme.PId = 0;
        //        NodeItme.Title = efp.ProjectName;
        //        NodeItme.Index = 0;
        //        NodeItme.Checked = false;
        //        NodeItme.Childrens = GetProjectTree();
        //    }           
        //}
    }
}