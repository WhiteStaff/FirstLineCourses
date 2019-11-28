using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ThreatsParser.Actions.Interfaces
{
    public static class MenuActionExtension
    {
        public static MenuItem[] ToMenuItems(this IMenuAction[] actions)
        {
            var items = actions.GroupBy(a => a.Category).Select(x => CreateTopMenuItem(x.Key, x.ToList())).ToArray();
            return items;
        }

        public static MenuItem ToMenuItem(this IMenuAction action)
        {
            var menuItem = new MenuItem() {Header = action.Name};
            menuItem.Click += (s, e) => action.Perform();
            return menuItem;
        }

        public static MenuItem CreateTopMenuItem(string name, IList<IMenuAction> items)
        {
            var menuItems = items.Select(x => x.ToMenuItem()).ToArray();
            var resultMenuItem = new MenuItem {Header = name};
            foreach (var item in menuItems)
            {
                resultMenuItem.Items.Add(item);
            }

            return resultMenuItem;
        }
    }
}