using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_W2_L20
{

    public class MenuService
    {
        private List<Menu> menuService { get; set; } = new List<Menu>();

        public void AddNewMenu(int id, string name, string group)
        {
            Menu menu = new Menu() { Id = id, Name = name, Group = group };
            menuService.Add(menu);
        }


        public List<Menu> GetAllItems()
        {
            return menuService;
        }



    }

}
