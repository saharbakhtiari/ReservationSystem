using System.Collections.Generic;

namespace Application_Frontend.Menus;
public class MenuItem
{
    public string Name { get; set; }
    public string Label { get; set; }
    public string Permission { get; set; }
    public string Role { get; set; }
    public List<MenuItem> Items { get; set; }
}
