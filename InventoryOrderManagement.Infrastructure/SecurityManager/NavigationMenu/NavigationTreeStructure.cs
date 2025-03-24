using InventoryOrderManagement.Infrastructure.SecurityManager;
using System.Text.Json;

namespace InventoryOrderManagement.Infrastructure.SecurityManager.NavigationMenu;

public class JsonStructureItem
{
    public string? URL { get; set; }
    public string? Name { get; set; }
    public bool IsModule { get; set; }
    public List<JsonStructureItem> Children { get; set; } = new List<JsonStructureItem>();
}

public static class NavigationTreeStructure
{
    public static readonly string JsonStructure = """
                                                  [
                                                  {
                                                        "URL": "#",
                                                        "Name": "Dashboards",
                                                        "IsModule": true,
                                                          "Children": [
                                                              {
                                                          "URL": "/Dashboards/DefaultDashboard",
                                                          "Name": "Default",
                                                          "IsModule": false
                                                              }
                                                                      ]
                                                  },
                                                  {
                                                      "URL": "#",
                                                      "Name": "Membership",
                                                      "IsModule": true,
                                                      "Children": [
                                                          {
                                                              "URL": "/Users/UserList",
                                                              "Name": "Users",
                                                              "IsModule": false
                                                          },
                                                          {
                                                              "URL": "/Roles/RoleList",
                                                              "Name": "Roles",
                                                              "IsModule": false
                                                          }
                                                      ]
                                                  },
                                                  {
                                                      "URL": "#",
                                                      "Name": "Profiles",
                                                      "IsModule": true,
                                                      "Children": [
                                                          {
                                                              "URL": "/Profiles/MyProfile",
                                                              "Name": "My Profile",
                                                              "IsModule": false
                                                          }
                                                      ]
                                                  },
                                                  {
                                                      "URL": "#",
                                                      "Name": "Settings",
                                                      "IsModule": true,
                                                      "Children": [
                                                          {
                                                              "URL": "/Companies/MyCompany",
                                                              "Name": "My Company",
                                                              "IsModule": false
                                                          }
                                                      ]
                                                  }
                                                  ]
                                                  """;
    
    public static List<MenuNavigationTreeNodeDto> GetCompleteMenuNavigationTreeNode()
    {
        var json = JsonStructure;

        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true
        };
        
        var menus = JsonSerializer.Deserialize<List<JsonStructureItem>>(json, options);

        List<MenuNavigationTreeNodeDto> nodes = new List<MenuNavigationTreeNodeDto>();

        var index = 1;
        void AddNodes(List<JsonStructureItem> menuItems, string? parentId = null)
        {
            foreach (var item in menuItems)
            {
                var nodeId = index.ToString();
                if (item.IsModule)
                {
                    nodes.Add(new MenuNavigationTreeNodeDto(nodeId, item.Name ?? "", param_hasChild: true, param_expanded: false));
                }
                else
                {
                    nodes.Add(new MenuNavigationTreeNodeDto(nodeId, item.Name ?? "", parentId, item.URL));
                }

                index++;

                if (item.Children != null && item.Children.Count > 0)
                {
                    AddNodes(item.Children, nodeId);
                }
            }
        }

        if (menus != null) AddNodes(menus);

        return nodes;
    }
    
    public static List<string> GetCompleteFirstMenuNavigationSegment()
    {
        var json = JsonStructure;
        
        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true
        };
        
        var menus = JsonSerializer.Deserialize<List<JsonStructureItem>>(json, options);
        var result = new List<string>();

        if (menus != null)
        {
            foreach (var item in menus)
            {
                ProcessMenuItem(item, result);
            }
        }

        return result;
    }
    
    private static void ProcessMenuItem(JsonStructureItem item, List<string> result)
    {
        if (item.URL != "#" && !string.IsNullOrEmpty(item.URL))
        {
            var segments = item.URL.Split('/');
            if (segments.Length > 1)
            {
                result.Add(segments[1]);
            }
        }

        if (item.Children != null && item.Children.Count > 0)
        {
            foreach (var child in item.Children)
            {
                ProcessMenuItem(child, result);
            }
        }
    }
}