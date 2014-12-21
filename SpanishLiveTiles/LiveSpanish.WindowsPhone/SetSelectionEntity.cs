using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSpanish.WindowsPhone.DataAccess.Entities;

namespace LiveSpanish.WindowsPhone
{
    public class SetSelectionEntity
    {
        public bool IsSelected { get; set; }
        public VocabularySetEnum SetEnum { get; set; }


        public override string ToString()
        {
            return SetEnum.ToString();
        }
    }
}
