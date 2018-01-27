using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiaryProject.Model
{
    public class DiaryModel
    {
        public string itemTitle
        {
            get; set;
        }
        public DateTime  writeDate
        {
            get; set;
        }
        /// <summary>
        /// 日记内容 html
        /// </summary>
        public string diaryContent { get; set; }

        /// <summary>
        /// 日记类型
        /// </summary>
        public int diaryType{ get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string UserName { get; set; }

    }
}
