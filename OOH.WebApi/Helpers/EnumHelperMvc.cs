using Microsoft.AspNetCore.Mvc.Rendering;
using OOH.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOH.WebApi.Helpers
{
    public static class EnumHelperMvc
    {
        public static List<SelectListItem> GetSelectListItemFromEnum<T>(Enum selected = null, bool selectMessageInclude = true, bool selectMessageDisabled = true, string defaultTextMessage = "") where T : struct, IConvertible
        {
            List<SelectListItem> items = new List<SelectListItem>();


            if (selectMessageInclude)
            {
                items.Add(new SelectListItem()
                {
                    Text = string.IsNullOrEmpty(defaultTextMessage) ? "Seleccionar" : defaultTextMessage,
                    Selected = true,
                    Disabled = selectMessageDisabled
                });
            }

            List<T> enumItems = EnumHelper.GetItems<T>();

            foreach (T itemEnum in enumItems)
            {
                SelectListItem item = new SelectListItem()
                {
                    Value = itemEnum.ToString(),
                    Text = (itemEnum as Enum).GetEnumDescription(),
                    Selected = selected != null && selected == itemEnum as Enum
                };
                items.Add(item);
            }

            return items;
        }
    }
}
