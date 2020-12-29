using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity;
using EasyOperate.Web.Models.AccessControl;
using EasyOperate.Common.Enums;

namespace EasyOperate.Web.Models
{
    
    public class NodeTreeItem
    {
        /// <summary>
        /// 显示的文本
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 显示文本对应的值
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public int pid { get; set; }
        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool ischecked { get; set; }
        /// <summary>
        /// 结合表单的辅助属性,View上使用(注意每个层级的索引必需从0开始并且是连续的)
        /// </summary>
        public int index { get; set; }
            /// <summary>
            /// 代表层次关系 项目表、分区表、楼表、单元表、设备表、节点表
            /// </summary>
        public PTypeIdEnum ptypeid { get; set; }

        /// <summary>
        /// 终节点
        /// </summary>
        public IList<TerminalNode> TerminalNodes { get; set; }
        /// <summary>
        /// 子菜单集合
        /// </summary>
        public IList<NodeTreeItem> Childrens { get; set; }
        
    }
    public class TerminalNode
    {
        /// <summary>
        /// 显示的文本
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 显示文本对应的值
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public int pid { get; set; }
        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool ischecked { get; set; }
        /// <summary>
        /// 结合表单的辅助属性,View上使用(注意每个层级的索引必需从0开始并且是连续的)
        /// </summary>
        public int index { get; set; }
        /// <summary>
        /// 代表层次关系 项目表、分区表、楼表、单元表、设备表、节点表
        /// </summary>
        public PTypeIdEnum ptypeid { get; set; }
    }
}