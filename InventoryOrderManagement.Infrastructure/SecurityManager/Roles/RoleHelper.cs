using InventoryOrderManagement.Infrastructure.SecurityManager.NavigationMenu;

namespace InventoryOrderManagement.Infrastructure.SecurityManager.Roles;

public class RoleHelper
{
    public static List<string> GetAdminRoles()
    {
        var roles = new List<string>();
        roles = NavigationTreeStructure.GetCompleteFirstMenuNavigationSegment();
        return roles;
    }

    public static string GetProfileRole()
    {
        return "Profiles";
    }
}